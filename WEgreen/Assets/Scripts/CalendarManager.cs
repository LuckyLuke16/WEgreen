using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CalendarManager : MonoBehaviour
{
    // variable declation
    // Test buttons
    //public Button buttonA;
    //public Button buttonB;
    public bool isButtonA;
    public bool isButtonB;
    public Button[] buttonList;
    //public Button navLeft;
    public string time;
    private string currentPage;
    public int intTime;
    private int currentDay;
    private int currentMonth;
    private int currentYear;
    private int monthIndex;
    private int dayIndex;
    public Text monthText;
    public Text yearText;
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

    // Start is called before the first frame update
    void Start()
    {
        // initialize list of months
        monthsList = new string[] {"Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember"};
        monthIndex = currentMonth - 1;
        // creating array and put all the buttons(31 days) in it
        buttonList = new Button[30];
        Debug.Log(buttonList.Length);
        for (int i = 1; i < 30; i++)
        {
            buttonList[i] = GameObject.Find("Button (" + i + ")").GetComponent<Button>();
            //Debug.Log(i);
        }

        //navLeft = GameObject.Find("Nav_Left").GetComponent<Button>();


        FindCurrentYearMonthDay();
        // find current page the use is on
        dayIndex = currentDay - 1;
        monthIndex = currentMonth - 1;
        currentDayImg = buttonList[dayIndex].GetComponent<Image>();
        Debug.Log("OKKKK: " + currentPage + "." + currentYear + "\n" + System.DateTime.UtcNow.ToLocalTime().ToString("MMMM.yyyy"));

        CheckCurrentDay();

        // initialize current month text
        monthText.text = monthsList[monthIndex];

        // initialize current weekdays text
        weekdaysText = new Text[7];
        for (int i = 0; i < weekdaysText.Length; i++)
        {
            weekdaysText[i] = GameObject.Find("D" + (i + 1)).GetComponent<Text>();
        }

        AlignWeekdays();
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
        if (isButtonA)
        {
            for (int i = 1; i < 24; i += 2)
            {
                Image imgA = buttonList[i].GetComponent<Image>();
                imgA.color = Color.red;
            }
        }
        else if (!isButtonA)
        {
            for (int i = 1; i < 24; i += 2)
            {
                //buttonList[i].GetComponent<Image>().color = Color.white;
                Image imgA = buttonList[i].GetComponent<Image>();
                imgA.color = Color.white;
            }
        }
        if (isButtonB)
        {
            for (int i = 2; i < 24; i += 2)
            {
                //buttonList[i].GetComponent<Image>().color = Color.blue;
                Image imgB = buttonList[i].GetComponent<Image>();
                imgB.color = Color.blue;
            }
        }
        else if (!isButtonB)
        {
            for (int i = 2; i < 24; i += 2)
            {
                //buttonList[i].GetComponent<Image>().color = Color.white;
                Image imgB = buttonList[i].GetComponent<Image>();
                imgB.color = Color.white;
            }
        }
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
            Debug.Log("A: false");
        }
        else
        {
            isButtonA = true;
            Debug.Log("A: true");
        }
    }

    public void ButtonBIsClicked()
    {
        if (isButtonB)
        {
            isButtonB = false;
            Debug.Log("B: false");
        }
        else
        {
            isButtonB = true;
            Debug.Log("B: true");
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

            Debug.Log("changed sprite!!!");
        }
        if (monthIndex > 0)
        {
            monthText.text = monthsList[--monthIndex];
            AlignWeekdays();
            CheckCurrentDay();
        }
        if(monthIndex == 0)
        {
            navLeftImg.sprite = grayArrow;
            Debug.Log("end of year!!!");
        }
        Debug.Log("left clicked: " + monthIndex);
    }

    // navigating one month forward
    public void NavigateRight()
    {
        if (navRightImg.sprite.name != redArrow.name)
        {
            navRightImg.sprite = redArrow;
            navLeftImg.sprite = greenArrow;
            Debug.Log("changed sprite!!!");
        }
        if (monthIndex < monthsList.Length - 1)
        {
            monthText.text = monthsList[++monthIndex];
            AlignWeekdays();
            CheckCurrentDay();
        }
        if (monthIndex == monthsList.Length - 1)
        {
            navRightImg.sprite = grayArrow;
            Debug.Log("end of year!!!");
        }
        Debug.Log("right clicked: " + monthIndex);
    }

    // highlight current day
    private void CheckCurrentDay()
    {
        // update current page
        currentPage = monthsList[monthIndex];

        if (System.DateTime.UtcNow.ToLocalTime().ToString("MMMM.yyyy") == currentPage + "." + currentYear)
        {
            Debug.Log("Current DAY!!!!!");
            currentDayImg.color = Color.red;
        }
        else
        {
            Debug.Log("NOTTTTTTT Current DAY!!!!!");
            //currentDayImg = buttonList[dayIndex].GetComponent<Image>();
            currentDayImg.color = Color.white;
        }
        Debug.Log("END\nStart");
    }

    private void AlignWeekdays()
    {
        FindCurrentYearMonthDay();
        DateTime date = new DateTime(currentYear, monthIndex + 1, 1);
        // for debugging
        //print(date);
        //print(date.ToString("dddd"));

        // identify the weekday of the first day of the month and align the weekdays in the correct order for this month
        if(date.ToString("dddd") == "Montag")
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

    public void OpenAddingWindow()
    {
        addingWindow.SetActive(true);
    }

    public void CloseAddingWindow()
    {
        addingWindow.SetActive(false);
    }
}
