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
    public Button navLeft;
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
    private string[] monthsList;
    public Sprite greenArrow;
    public Sprite redArrow;
    public Sprite grayArrow;
    public Image navRightImg;
    public Image navLeftImg;
    public Image currentDayImg;

    // Start is called before the first frame update
    void Start()
    {
        // initialize list of months
        monthsList = new string[] {"Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember"};
        monthIndex = currentMonth - 1;
        // creating array and put all the buttons(31 days) in it
        buttonList = new Button[30];
        Debug.Log(buttonList.Length);
        for (int i = 1; i < 26; i++)
        {
            buttonList[i] = GameObject.Find("Button (" + i + ")").GetComponent<Button>();
            //Debug.Log(i);
        }

        navLeft = GameObject.Find("Nav_Left").GetComponent<Button>();


        // find current day and cast from string to int
        currentDay = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("dd"));
        // find current month and cast from string to int
        currentMonth = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("MM"));
        // find current year and cast from string to int
        currentYear = Convert.ToInt32(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy"));
        // find current page the use is on
        dayIndex = currentDay - 1;
        monthIndex = currentMonth - 1;
        currentDayImg = buttonList[dayIndex].GetComponent<Image>();
        Debug.Log("OKKKK: " + currentPage + "." + currentYear + "\n" + System.DateTime.UtcNow.ToLocalTime().ToString("MMMM.yyyy"));

        CheckCurrentDay();

        // initialize current month text
        monthText.text = monthsList[monthIndex];
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
}
