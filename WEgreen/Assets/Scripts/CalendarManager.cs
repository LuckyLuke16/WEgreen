using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

/**
 * @brief Manages all the fundamentals of a calendar e.g. calculating and displaying the correct date when is navigating throught the calendar.
 * Therefore this class contains all the needed methods like NavigateLeft() and CheckCurrentDay() to make this calendar-function work.
 */
public class CalendarManager : MonoBehaviour
{
    // declaring variables
    private GameObject[] buttonList;
    private string currentPage;
    private int intTime;
    private int intYear;
    private int currentDay;
    private int currentMonth;
    private int currentYear;
    private int monthIndex;
    private int dayIndex;
    [SerializeField] 
    private Text monthText;
    [SerializeField]
    private Text yearText;
    [SerializeField]
    private Text monthIndexText;
    private Text[] weekdaysText;
    private string[] monthsList;
    private string[] weekdaysAligned;
    [SerializeField]
    private Sprite greenArrow;
    [SerializeField]
    private Sprite redArrow;
    [SerializeField]
    private Sprite grayArrow;
    [SerializeField]
    private Image navRightImg;
    [SerializeField]
    private Image navLeftImg;
    private GameObject currentDayImg;
    [SerializeField]
    private GameObject addingWindow;
    [SerializeField]
    private GameObject wateringPlantOverview;
    [SerializeField]
    private GameObject[] buttonGameobjectList = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        // initializing variables
        // initialize list of months
        monthsList = new string[] { "Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember" };
        monthIndex = currentMonth - 1;
        // creating array and put all the buttons(31 days) in it
        buttonList = new GameObject[31];
        for (int i = 1; i < 31; i++)
        {
            buttonList[i - 1] = GameObject.Find(i.ToString());
        }
        FindCurrentYearMonthDay();

        // find current page the user is on
        dayIndex = currentDay - 1;
        monthIndex = currentMonth - 1;
        currentDayImg = buttonList[dayIndex];
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

        // check correctness routine
        CheckCurrentDay();
        AlignWeekdays();
        TotalDaysInMonth();
    }

    /**
     * @brief Updates the variables currentDay, currentMonth and currentYear to current date of system.
     * @return void
     */
    private void FindCurrentYearMonthDay()
    {
        // find current day and cast from string to int
        currentDay = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("dd"));
        // find current month and cast from string to int
        currentMonth = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("MM"));
        // find current year and cast from string to int
        currentYear = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy"));
    }

    /**
     * @brief Navigate to previous month with the correct date information, means all the displayed text on the UI are adjusted.
     * @return void
     */
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

    /**
     * @brief Navigate to next month with the correct date information, means all the displayed text on the UI are adjusted.
     * @return void
     */
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

    /**
     * @brief Highlights the UI text of the current day red. 
     * @return void
     */
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
            currentDayImg.GetComponentInChildren<Text>().color = Color.black;
        }
    }

    /**
     * @brief Adjusts the order of the weekdays(monday,...,sunday) in the UI, so the displayed info is correct. 
     * @return void
     */
    private void AlignWeekdays()
    {
        FindCurrentYearMonthDay();
        DateTime date = new DateTime(intYear, monthIndex + 1, 1);   // get first day(week day) of the displayed month

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

    /**
     * @brief Find out how many total days the current displayed month has(28, 29, 30 or 31). Display the correct amount of days on the UI.
     * @return void
     */
    private void TotalDaysInMonth()
    {
        FindCurrentYearMonthDay();
        DateTime firstDayOfMonth = new DateTime(intYear, monthIndex + 1, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        // identify the last day of the month and adjust how many days are shown for this month
        if (lastDayOfMonth.ToString("dd") == "31")
        {
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
        }

        else if (lastDayOfMonth.ToString("dd") == "30")
        {
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
            buttonGameobjectList[2].SetActive(false);
        }
        else if (lastDayOfMonth.ToString("dd") == "29")
        {
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
            buttonGameobjectList[1].SetActive(false);
        }
        else if (lastDayOfMonth.ToString("dd") == "28")
        {
            buttonGameobjectList[0].SetActive(true);
            buttonGameobjectList[1].SetActive(true);
            buttonGameobjectList[2].SetActive(true);
            buttonGameobjectList[0].SetActive(false);
        }
    }

    /**
     * @brief Opens up the adding window for watering plants and darkens the add button in the background.
     * @return void
     */
    public void OpenAddingWindow()
    {
        addingWindow.SetActive(true);
        GameObject.Find("Add_Plant_To_Watering").GetComponent<Button>().image.color = Color.gray;
    }

    /**
     * @brief Closes the adding window for watering plants and resets the brightness of the add button.
     * @return void
     */
    public void CloseAddingWindow()
    {
        addingWindow.SetActive(false);
        GameObject.Find("Add_Plant_To_Watering").GetComponent<Button>().image.color = Color.white;
    }

    /**
     * @brief Opens up the overview window with all the added watering plants data(name, interval) and darkens the overview button in the background.
     * @return void
     */
    public void OpenWateringPlantCollectionOverview()
    {
        wateringPlantOverview.SetActive(true);
        GameObject.Find("OpenWateringPlantOverview_Bg").GetComponent<Button>().image.color = Color.gray;
    }

    /**
     * @brief Closes the overview window with all the added watering plants data(name, interval) and resets the brightness of the overview button.
     * @return void
     */
    public void CloseWateringPlantCollectionOverview()
    {
        wateringPlantOverview.SetActive(false);
        GameObject.Find("OpenWateringPlantOverview_Bg").GetComponent<Button>().image.color = Color.white;
    }
}
