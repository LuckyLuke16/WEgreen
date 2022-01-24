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
    private GameObject[] buttonList;
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
    public Text wateringPlantCollectionOverviewText;
    public Text monthIndexText;
    private Text[] weekdaysText;
    //private Text[] monthDaysText;
    private string[] monthsList;
    private string[] weekdaysAligned;
    public Sprite greenArrow;
    public Sprite redArrow;
    public Sprite grayArrow;
    public Image navRightImg;
    public Image navLeftImg;
    private GameObject currentDayImg;
    public GameObject addingWindow;
    public GameObject wateringPlantOverview;
    public GameObject[] buttonGameobjectList = new GameObject[3];
    public GameObject[] greenWateringGameobjectList;
    public GameObject[] yellowWateringGameobjectList;
    public GameObject[] redWateringGameobjectList;
    int k;


    // Start is called before the first frame update
    void Start()
    {
        // initialize list of months
        monthsList = new string[] { "Januar", "Februar", "M‰rz", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember" };
        monthIndex = currentMonth - 1;
        // creating array and put all the buttons(31 days) in it
        buttonList = new GameObject[31];
        //Debug.Log(buttonList.Length);
        for (int i = 1; i < 31; i++)
        {
            buttonList[i - 1] = GameObject.Find(i.ToString());
            //Debug.Log(i);
        }

        FindCurrentYearMonthDay();

        // find current page the user is on
        dayIndex = currentDay - 1;
        monthIndex = currentMonth - 1;
        currentDayImg = buttonList[dayIndex];
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

        //for(int i = 0; i < 31; i++)
        //{
        //    monthDaysText[i] = GameObject.Find("MarkText" + (i + 1)).GetComponent<Text>();
        //}

        // check corectness routine
        CheckCurrentDay();
        AlignWeekdays();
        TotalDaysInMonth();

        // find watering game objects with tag and disable them in scene
        greenWateringGameobjectList = GameObject.FindGameObjectsWithTag("greenWatering");
        yellowWateringGameobjectList = GameObject.FindGameObjectsWithTag("yellowWatering");
        redWateringGameobjectList = GameObject.FindGameObjectsWithTag("redWatering");

        foreach (GameObject go in greenWateringGameobjectList)
        {
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
        if (navLeftImg.sprite.name != redArrow.name)
        {
            navLeftImg.sprite = redArrow;
            navRightImg.sprite = greenArrow;
        }
        if (monthIndex >= 0)
        {
            if (monthText.text == "Januar" && intYear > 2021)
            {
                intYear--;
                monthIndex = monthsList.Length;
            }
            if (monthIndex > 0)
            {
                monthText.text = monthsList[--monthIndex];
            }
            AlignWeekdays();
            CheckCurrentDay();
            TotalDaysInMonth();
        }
        if (monthIndex == 0 && intYear == 2021)
        {
            navLeftImg.sprite = grayArrow;
        }
    }

    // navigating one month forward
    public void NavigateRight()
    {
        if (navRightImg.sprite.name != redArrow.name)
        {
            navRightImg.sprite = redArrow;
            navLeftImg.sprite = greenArrow;
        }
        if (monthIndex <= monthsList.Length - 1)
        {
            if (monthText.text == "Dezember" && intYear < 2024)
            {
                intYear++;
                monthIndex = -1;
            }
            if (monthIndex < monthsList.Length - 1)
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
        }
    }

    // highlight current day
    private void CheckCurrentDay()
    {
        // update current page
        currentPage = monthsList[monthIndex];
        monthIndexText.text = (monthIndex + 1).ToString();
        yearText.text = intYear.ToString();

        if (System.DateTime.UtcNow.ToLocalTime().ToString("MMMM.yyyy") == currentPage + "." + yearText.text)
        {
            currentDayImg.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            Debug.Log("NOTTTTTTT Current DAY!!!!!");
            currentDayImg.GetComponentInChildren<Text>().color = Color.black;
            //currentDayImg.color = Color.white;
        }
    }

    private void AlignWeekdays()
    {

        FindCurrentYearMonthDay();
        DateTime date = new DateTime(intYear, monthIndex + 1, 1);

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
    //public void MarkWateringDays()
    //{
    //    string wateringPlantCounter = File.ReadAllText(Application.persistentDataPath + "/wateringPlantCounter.txt");
    //    if (waterIntervallValueText.text != null || File.Exists(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
    //    {
    //        k = Convert.ToInt32(waterIntervallValueText.text);
    //        for (int i = 0; i < monthDaysText.Length; i += k)
    //        {
    //            if (currentPage == System.DateTime.UtcNow.ToLocalTime().ToString("MMMM") && i >= currentDay)
    //            {
    //                monthDaysText[i].text = "1";
    //            }
    //            else if (monthIndex > Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("MM")))
    //            {
    //                monthDaysText[i].text = "1";

    //            }
    //        }
    //    }
    //    k = Convert.ToInt32(waterIntervallValueText.text);
    //    //Debug.Log("k: " + k);
    //    for (int i = 0; i < monthDaysText.Length; i += k)
    //    {
    //        //Debug.Log(yellowWateringGameobjectList.Length + "_" + i + "k" + k + "heute" + currentDay + currentMonth.ToString() + currentPage);
    //        if (i >= currentDay && System.DateTime.UtcNow.ToLocalTime().ToString("MMMM") == currentPage)
    //        {
    //            yellowWateringGameobjectList[i].SetActive(true);
    //            //Debug.Log("TEST FUNKTIONIERT 1");
    //        }
    //        /* else if (currentPage ist kleiner als aktueller Monat ODER currentPage ist gleich groﬂ wie aktueller Monat UND Tag ist kleiner als aktueller Tag)
    //         *      if(markierung ist gleich true (also gegossen)
    //         *          markiere diese Tage gruen
    //         *      else
    //         *          marikiere die anderen Tag rot
    //         * 
    //         * 
    //         * */
    //    }
    //}

    public void OpenWateringPlantCollectionOverview()
    {
        wateringPlantOverview.SetActive(true);
        GameObject.Find("OpenWateringPlantOverview_Bg").GetComponent<Button>().image.color = Color.gray;
    }

    public void CloseWateringPlantCollectionOverview()
    {
        wateringPlantOverview.SetActive(false);
        GameObject.Find("OpenWateringPlantOverview_Bg").GetComponent<Button>().image.color = Color.white;
    }
}
