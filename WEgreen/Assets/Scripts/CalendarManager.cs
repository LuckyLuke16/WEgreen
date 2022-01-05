using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class CalendarManager : MonoBehaviour
{
    // variable declation
    // Test buttons
    //public Button buttonA;
    //public Button buttonB;
    public bool isButtonA;
    public bool isButtonB;
    private Button[] buttonList;
    private Button addWateringPlantButton;
    //public Button navLeft;
    public string time;
    private string currentPage;
    public int intTime;
    public int intYear;
    private int currentDay;
    private int currentMonth;
    private int currentYear;
    private int monthIndex;
    private int dayIndex;
    public Text monthText;
    public Text yearText;
    public Text waterIntervallValueText;
    public Text wateringPlantName;
    public Text wateringPlantIntervall; 
    public Text wateringPlantNameLoaded;
    public Text wateringPlantIntervallLoaded;
    private Text[] weekdaysText;
    private string[] monthsList;
    private string[] weekdaysAligned;
    public Sprite greenArrow;
    public Sprite redArrow;
    public Sprite grayArrow;
    public Image navRightImg;
    public Image navLeftImg;
    private Image currentDayImg;
    public GameObject addingWindow;
    public GameObject[] buttonGameobjectList = new GameObject[3];
    public GameObject[] greenWateringGameobjectList;
    public GameObject[] yellowWateringGameobjectList;
    public GameObject[] redWateringGameobjectList;
    public Slider wateringSlider;

    // Start is called before the first frame update
    void Start()
    {

        

        // initialize list of months
        monthsList = new string[] {"Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember"};
        monthIndex = currentMonth - 1;
        // creating array and put all the buttons(31 days) in it
        buttonList = new Button[31];
        //Debug.Log(buttonList.Length);
        for (int i = 1; i < 31; i++)
        {
            buttonList[i - 1] = GameObject.Find(i.ToString()).GetComponent<Button>();
            //Debug.Log(i);
        }

        FindCurrentYearMonthDay();

        // find current page the user is on
        dayIndex = currentDay - 1;
        monthIndex = currentMonth - 1;
        currentDayImg = buttonList[dayIndex].GetComponent<Image>();
        //Debug.Log("OKKKK: " + currentPage + "." + currentYear + "\n" + System.DateTime.UtcNow.ToLocalTime().ToString("MMMM.yyyy"));

        // initialize current month text, year text and casting year text to int
        monthText.text = monthsList[monthIndex];
        yearText.text = currentYear.ToString();
        intYear = Convert.ToInt32(yearText.text);

        // initialize current weekdays text list with the text objects from scene
        weekdaysText = new Text[7];
        for (int i = 0; i < weekdaysText.Length; i++)
        {
            weekdaysText[i] = GameObject.Find("D" + (i + 1)).GetComponent<Text>();
        }

        // check corectness routine
        CheckCurrentDay();
        AlignWeekdays();
        TotalDaysInMonth();

        // set max value of watering slider
        wateringSlider.maxValue = 4;

        // find watering game objects with tag and disable them in scene
        greenWateringGameobjectList = GameObject.FindGameObjectsWithTag("greenWatering");
        yellowWateringGameobjectList = GameObject.FindGameObjectsWithTag("yellowWatering");
        redWateringGameobjectList = GameObject.FindGameObjectsWithTag("redWatering");

        foreach (GameObject go in greenWateringGameobjectList)
        {
            //Debug.Log("HELLOOOOOOOOOOOOOOOO");
            go.SetActive(false);
        }
        foreach (GameObject go in yellowWateringGameobjectList)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in redWateringGameobjectList)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // DOES NOT WORK: color of current day not changing???
        //time = System.DateTime.UtcNow.ToLocalTime().ToString("dd");
        //Debug.Log(Convert.ToInt32(time) + 3);

        //time = System.DateTime.UtcNow.ToLocalTime().ToString("dd");
        //intTime = Convert.ToInt32(time) - 8;
        //Image currentDay = buttonList[intTime].GetComponent<Image>();
        //currentDay.color = Color.black;
        //Debug.Log(currentDay.name);

        // highlight watering days
        //if (isButtonA)
        //{
        //    for (int i = 1; i < 24; i += 2)
        //    {
        //        Image imgA = buttonList[i].GetComponent<Image>();
        //        imgA.color = Color.red;
        //    }
        //}
        //else if (!isButtonA)
        //{
        //    for (int i = 1; i < 24; i += 2)
        //    {
        //        //buttonList[i].GetComponent<Image>().color = Color.white;
        //        Image imgA = buttonList[i].GetComponent<Image>();
        //        imgA.color = Color.white;
        //    }
        //}
        //if (isButtonB)
        //{
        //    for (int i = 2; i < 24; i += 2)
        //    {
        //        //buttonList[i].GetComponent<Image>().color = Color.blue;
        //        Image imgB = buttonList[i].GetComponent<Image>();
        //        imgB.color = Color.blue;
        //    }
        //}
        //else if (!isButtonB)
        //{
        //    for (int i = 2; i < 24; i += 2)
        //    {
        //        //buttonList[i].GetComponent<Image>().color = Color.white;
        //        Image imgB = buttonList[i].GetComponent<Image>();
        //        imgB.color = Color.white;
        //    }
        //}

        waterIntervallValueText.text = wateringSlider.value.ToString();

        MarkWateringDays();

        //AddWateringPlant();
    }



    //public void ButtonIsClicked(string button)
    //{
    //    if (button == "true")
    //    {
    //        button = "false";
    //        Debug.Log("false");
    //    }
    //    else
    //    {
    //        button = "true";
    //        Debug.Log("true");
    //    }
    //}

    // boolean setter for button clicks
    public void ButtonAIsClicked()
    {
        if (isButtonA)
        {
            isButtonA = false;
            //Debug.Log("A: false");
        }
        else
        {
            isButtonA = true;
            //Debug.Log("A: true");
        }
    }

    public void ButtonBIsClicked()
    {
        if (isButtonB)
        {
            isButtonB = false;
            //Debug.Log("B: false");
        }
        else
        {
            isButtonB = true;
            //Debug.Log("B: true");
        }
    }

    private void FindCurrentYearMonthDay()
    {
        // find current day and cast from string to int
        currentDay = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("dd"));
        // find current month and cast from string to int
        currentMonth = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("MM"));
        // find current year and cast from string to int
        currentYear = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy"));
    }

    // navigating one month back
    public void NavigateLeft()
    {
        if(navLeftImg.sprite.name != redArrow.name)
        {
            navLeftImg.sprite = redArrow;
            navRightImg.sprite = greenArrow;

            //Debug.Log("changed sprite!!!");
        }
        if (monthIndex >= 0)
        {
            if (monthText.text == "Januar" && intYear > 2021)
            {
                intYear--;
                monthIndex = monthsList.Length;
            }
            if(monthIndex > 0)
            {
                monthText.text = monthsList[--monthIndex];
            }
            AlignWeekdays();
            CheckCurrentDay();
            TotalDaysInMonth();
            //Debug.Log("Month Index: " + monthIndex);
        }
        if(monthIndex == 0 && intYear == 2021)
        {
            //Debug.Log("Month Index: " + monthIndex);
            navLeftImg.sprite = grayArrow;
            //Debug.Log("end of year!!!");
        }
        //Debug.Log("left clicked: " + monthIndex);
    }

    // navigating one month forward
    public void NavigateRight()
    {
        if (navRightImg.sprite.name != redArrow.name)
        {
            navRightImg.sprite = redArrow;
            navLeftImg.sprite = greenArrow;
            //Debug.Log("changed sprite!!!");
        }
        if (monthIndex <= monthsList.Length - 1)
        {
            //Debug.Log("Month Index: " + monthIndex);
            if (monthText.text == "Dezember" && intYear < 2024)
            {
                //Debug.Log("IM INNNNN!!!");
                intYear++;
                monthIndex = -1;
            }
            if(monthIndex < monthsList.Length - 1)
            {
                monthText.text = monthsList[++monthIndex];
            }
            AlignWeekdays();
            CheckCurrentDay();
            TotalDaysInMonth();
        }
        if (monthIndex == monthsList.Length - 1 && intYear == 2024)
        {
            navRightImg.sprite = grayArrow;
            //Debug.Log("end of year!!!");
        }
        //Debug.Log("right clicked: " + monthIndex);
    }

    // highlight current day
    private void CheckCurrentDay()
    {
        // update current page
        currentPage = monthsList[monthIndex];
        yearText.text = intYear.ToString();

        if (System.DateTime.UtcNow.ToLocalTime().ToString("MMMM.yyyy") == currentPage + "." + yearText.text)
        {
            //Debug.Log("Current DAY!!!!!");
            currentDayImg.color = Color.red;
        }
        else
        {
            Debug.Log("NOTTTTTTT Current DAY!!!!!");
            //currentDayImg = buttonList[dayIndex].GetComponent<Image>();
            currentDayImg.color = Color.white;
        }
        //Debug.Log("END\nStart");
    }

    private void AlignWeekdays()
    {
        
        FindCurrentYearMonthDay();
        DateTime date = new DateTime(intYear, monthIndex + 1, 1);
        // for debugging
        //print(date);
        //print(date.ToString("dddd"));

        // identify the weekday of the first day of the month and align the weekdays in the correct order for this month
        if (date.ToString("dddd") == "Montag")
        {
            print("Montag baby!");
            weekdaysAligned = new string[] { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" };
        }
        else if (date.ToString("dddd") == "Dienstag")
        {
            print("Dienstag baby!");
            weekdaysAligned = new string[] { "Di", "Mi", "Do", "Fr", "Sa", "So", "Mo" };
        }
        else if (date.ToString("dddd") == "Mittwoch")
        {
            print("Mittwoch baby!");
            weekdaysAligned = new string[] { "Mi", "Do", "Fr", "Sa", "So", "Mo", "Di" };

        }
        else if (date.ToString("dddd") == "Donnerstag")
        {
            print("Donnerstag baby!");
            weekdaysAligned = new string[] { "Do", "Fr", "Sa", "So", "Mo", "Di", "Mi" };

        }
        else if (date.ToString("dddd") == "Freitag")
        {
            print("Freitag baby!");
            weekdaysAligned = new string[] { "Fr", "Sa", "So", "Mo", "Di", "Mi", "Do" };

        }
        else if (date.ToString("dddd") == "Samstag")
        {
            print("Samstag baby!");
            weekdaysAligned = new string[] { "Sa", "So", "Mo", "Di", "Mi", "Do", "Fr" };

        }
        else if (date.ToString("dddd") == "Sonntag")
        {
            print("Sonntag baby!");
            weekdaysAligned = new string[] { "So", "Mo", "Di", "Mi", "Do", "Fr", "Sa" };

        }

        // name the weekday texts in the correct way
        for (int i = 0; i < weekdaysAligned.Length; i++)
        {
            weekdaysText[i].text = weekdaysAligned[i];
        }
    }

    public void TotalDaysInMonth()
    {
        FindCurrentYearMonthDay();
        DateTime firstDayOfMonth = new DateTime(intYear, monthIndex + 1, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        // identify the last day of the month and adjust how many days are shown for this month
        if (lastDayOfMonth.ToString("dd") == "31")
        {
            print("31days baby!");
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
        }

        else if (lastDayOfMonth.ToString("dd") == "30")
        {
            print("30days baby!");
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
            buttonGameobjectList[2].SetActive(false);
        }
        else if (lastDayOfMonth.ToString("dd") == "29")
        {
            print("29days baby!");
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
            buttonGameobjectList[1].SetActive(false);
        }
        else if (lastDayOfMonth.ToString("dd") == "28")
        {
            print("28days baby!");
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
            buttonGameobjectList[0].SetActive(false);
        }
    }

    public void OpenAddingWindow()
    {
        addingWindow.SetActive(true);
        GameObject.Find("Add_Plant_To_Watering").GetComponent<Button>().image.color = Color.gray;
    }

    public void CloseAddingWindow()
    {
        addingWindow.SetActive(false);
        GameObject.Find("Add_Plant_To_Watering").GetComponent<Button>().image.color = Color.white;
    }

    // mark the watering days with correct intervalls
    public void MarkWateringDays()
    {
        switch (Convert.ToInt32(waterIntervallValueText.text))
        {
            case 1:
                //Debug.Log("Intervall 1");
                for(int i = 0; i < greenWateringGameobjectList.Length; i += 2)
                {
                    greenWateringGameobjectList[i].SetActive(true);
                }
                break;
            case 2:
                //Debug.Log("Intervall 2");
                for (int i = 0; i < yellowWateringGameobjectList.Length; i += 3)
                {
                    yellowWateringGameobjectList[i].SetActive(true);
                }
                break;
            case 3:
                //Debug.Log("Intervall 3");
                for (int i = 0; i < redWateringGameobjectList.Length; i += 5)
                {
                    redWateringGameobjectList[i].SetActive(true);
                }
                break;
            case 4:
                //Debug.Log("Intervall 4");
                break;
            default:
                //Debug.Log("mhhhhh?");
                break;
        }
    }

    public void AddWateringPlant()
    {
        //WateringPlantData wateringPlantKaktus = new WateringPlantData();
        //wateringPlantKaktus.wateringPlantName = wateringPlantName.text;
        //wateringPlantKaktus.wateringIntervall = wateringPlantIntervall.text;

        //string jsonWateringPlantKaktus = JsonUtility.ToJson(wateringPlantKaktus);
        //Debug.Log(jsonWateringPlantKaktus);

        //File.WriteAllText(Application.dataPath + "/wateringPlantKaktus.json", jsonWateringPlantKaktus);

        string jsonWateringPlantKaktus = File.ReadAllText(Application.dataPath + "/wateringPlantKaktus.json");

        WateringPlantData loadedWateringPlantKaktus = JsonUtility.FromJson<WateringPlantData>(jsonWateringPlantKaktus);

        wateringPlantNameLoaded.text = loadedWateringPlantKaktus.wateringPlantName;
        wateringPlantIntervallLoaded.text = loadedWateringPlantKaktus.wateringIntervall;
        Debug.Log("Name: " + loadedWateringPlantKaktus.wateringPlantName);
        Debug.Log("Intervall: " + loadedWateringPlantKaktus.wateringIntervall);
    }

    private class WateringPlantData
    {
        public string wateringPlantName;
        public string wateringIntervall;
    }
}
