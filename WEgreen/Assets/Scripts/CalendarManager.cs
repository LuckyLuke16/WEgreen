using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{

    //public Button buttonA;
    //public Button buttonB;
    public bool isButtonA;
    public bool isButtonB;
    public Button[] buttonList;

    // Start is called before the first frame update
    void Start()
    {
        buttonList = new Button[30];
        Debug.Log(buttonList.Length);
        for (int i = 1; i < 26; i++)
        {
            buttonList[i] = GameObject.Find("Button (" + i + ")").GetComponent<Button>();
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
}
