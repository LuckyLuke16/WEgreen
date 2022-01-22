using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class SaveLoadDelete : MonoBehaviour
{
    private const string SAVE_SEPARATOR = " -#SAVE_SEPARATOR#- ";
    private const string PLANT_DATA_TEXT_FILE_NAME = "wateringPlantData";

    [SerializeField]
    private Text waterinPlantName;
    [SerializeField]
    private Text wateringPlantIntervall;
    [SerializeField]
    private Text wateringPlantsOverviewText;

    private string wateringPlantCounter = "";

    string wateringPlantsDataString = "";

    [SerializeField]
    private Text debugText;
    [SerializeField]
    private Text waterIntervallValueText;
    [SerializeField]
    private Text displayedMonthText;
    [SerializeField]
    private Text monthIndexText;
    [SerializeField]
    private Text displayedYearText;
    [SerializeField]
    private Text debugg1;
    [SerializeField]
    private Text debugg2;
    [SerializeField]
    private Text debugg3;
    [SerializeField]
    private Text debugg4;


    private GameObject[] monthDays;
    private GameObject[] monthDaysImage;
    private GameObject[] monthDaysName;
    private GameObject monthDaysNameElement;
    //private Text[] monthDaysText;

    private int intervall = 1;
    private int offsetPrev;
    private int offsetNext;
    private int offsetRef;

    private int currentMonathDay;
    private int lastDayOfMonthInt;

    private bool isNavLeft;
    private bool isNavRight = true;


    // Start is called before the first frame update
    void Start()
    {
        Load();


        monthDays = GameObject.FindGameObjectsWithTag("wateringMark");
        monthDaysImage = GameObject.FindGameObjectsWithTag("month_days");
        monthDaysName = new GameObject[31];
        for (int i = 1; i < 32; i++)
        {
            //monthDaysNameElement = GameObject.Find(i.ToString());
            //monthDaysName[i] = monthDaysNameElement;
            monthDaysName[i - 1] = GameObject.Find(i.ToString());
            Debug.Log("monthdayname" + monthDaysName[i - 1].name);
        }
        debugg3.text = "montsdays length1: " + monthDays.Length;

        //monthDaysText = new Text[monthDays.Length];

        //for (int i = 0; i < monthDays.Length; i++)
        //{
        //    monthDaysText[i] = monthDays[i].GetComponent<Text>();
        //}

        MarkWateringDays();

    }

    // Update is called once per frame
    void Update()
    {

    }

    // saves plant data
    public void Save()
    {
        LoadWateringPlantCounter();
        // put input data into string array
        string[] plantData = new string[] {
            waterinPlantName.text,
            wateringPlantIntervall.text
        };

        // split the string elements with a separator and put them into a string and save the string by writing it into a text file
        string saveString = string.Join(SAVE_SEPARATOR, plantData);
        // load counter
        int counter = int.Parse(wateringPlantCounter);
        if (counter < 4)
        {
            // if file exists then create a new file with a higher counter, otherwise create file with current counter
            if (File.Exists(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
            {
                SaveWateringPlantCounter("increase");
                File.WriteAllText(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt", saveString);
                Debug.Log("1LAST WRTIE TIME: " + File.GetLastWriteTimeUtc(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"));
                int month = int.Parse(File.GetLastWriteTimeUtc(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt").ToString("MM")) + 1;
                Debug.Log("1.1LAST WRTIE TIME: " + month);

            }
            else
            {
                debugText.text = "1DATA PATH: " + Application.persistentDataPath;
                SaveWateringPlantCounter("increase");
                File.WriteAllText(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt", saveString);
                Debug.Log("2LAST WRTIE TIME: " + File.GetLastWriteTimeUtc(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"));
            }

            Load();
            Debug.Log("FILE " + wateringPlantCounter + " SAVED.");
        }
        else
        {
            Debug.Log("You have reached the maximum amount of plants already. Please remove a plant in order to add a new one.");
        }
    }

    // loads plant data
    public void Load()
    {
        LoadWateringPlantCounter();
        CollectWateringPlantData();
        // display it
        wateringPlantsOverviewText.text = wateringPlantsDataString;
    }

    // saves / updates counter
    private void SaveWateringPlantCounter(string update)
    {
        LoadWateringPlantCounter();
        int counter = int.Parse(wateringPlantCounter);
        if (update == "increase")
        {
            counter++;
            wateringPlantCounter = counter.ToString();
            File.WriteAllText(Application.persistentDataPath + "/wateringPlantCounter.txt", wateringPlantCounter);
        }
        else if (update == "decrease")
        {
            counter--;
            wateringPlantCounter = counter.ToString();
            File.WriteAllText(Application.persistentDataPath + "/wateringPlantCounter.txt", wateringPlantCounter);
        }
        else
        {
            Debug.Log("The is not a legal update mode.");
        }
    }

    // loads counter
    private void LoadWateringPlantCounter()
    {
        if (File.Exists(Application.persistentDataPath + "/wateringPlantCounter.txt"))
        {
            wateringPlantCounter = File.ReadAllText(Application.persistentDataPath + "/wateringPlantCounter.txt");
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/wateringPlantCounter.txt", "-1");
            Debug.Log("The text file 'wateringPlantCounter.txt' does not exist.");
        }
    }

    public void Delete()
    {
        LoadWateringPlantCounter();
        int counter = int.Parse(wateringPlantCounter);
        if (counter >= 0)
        {
            // if file exists then create a new file with a higher counter, otherwise create file with current counter
            if (File.Exists(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
            {
                File.Delete(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt");
                Debug.Log("FILE " + wateringPlantCounter + " DELETED.");
                SaveWateringPlantCounter("decrease");
                Load();
            }
            else
            {
                Debug.Log("This text file does not exist.");

            }
        }
        else
        {
            Debug.Log("There are no plants to remove. Please add some plants first.");
        }
    }

    private void CollectWateringPlantData()
    {
        string[] savedStrings = new string[int.Parse(wateringPlantCounter) + 1];
        string[] wateringPlantsData;
        wateringPlantsData = new string[2];
        wateringPlantsDataString = "\n";
        for (int i = 0; i < savedStrings.Length; i++)
        {
            savedStrings[i] = File.ReadAllText(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + i + ".txt");
            wateringPlantsData = savedStrings[i].Split(new[] { SAVE_SEPARATOR }, System.StringSplitOptions.None);
            wateringPlantsDataString += "Pflanze " + (i + 1) + ": " + wateringPlantsData[0] + "\n";
            wateringPlantsDataString += "Intervall: " + wateringPlantsData[1] + "\n\n";
        }
    }

    public void MarkWateringDays()
    {
        // get counter
        string wateringPlantCounter = File.ReadAllText(Application.persistentDataPath + "/wateringPlantCounter.txt");
        // get last day of month which gives us the amount of days a month has
        DateTime firstDayOfMonth = new DateTime(int.Parse(displayedYearText.text), int.Parse(monthIndexText.text), 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        // if file exists then access the data to mark the calendar
        if (/*waterIntervallValueText.text != null || */File.Exists(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
        {
            // prepare the needed data e.g. intervall to calculate the watering markers
            string[] savedStrings = new string[int.Parse(wateringPlantCounter) + 1];
            string[] wateringPlantsData;
            wateringPlantsData = new string[2];
            wateringPlantsDataString = "\n";
            // go through all the existing watering plant data files and get the intervalls to mark the days accordingly
            for (int i = 0; i < savedStrings.Length; i++)
            {
                savedStrings[i] = File.ReadAllText(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + i + ".txt");
                wateringPlantsData = savedStrings[i].Split(new[] { SAVE_SEPARATOR }, System.StringSplitOptions.None);

                // get the intervall of all the existing plants one by one
                intervall = int.Parse(wateringPlantsData[1]);
                // calculate for the offset for the other months, so the intervall is still remains correct
                currentMonathDay = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd"));
                lastDayOfMonthInt = int.Parse(lastDayOfMonth.ToString("dd"));
                //offset = intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall);


                //wateringPlantsDataString += "Pflanze " + (i + 1) + ": " + wateringPlantsData[0] + "\n";
                //wateringPlantsDataString += "Intervall: " + wateringPlantsData[1] + "\n\n";
            }

            // reset month values
            for (int i = 0; i < monthDays.Length; i++)
            {
                monthDays[i].GetComponent<Text>().text = "";
                //monthDaysImage[i].GetComponent<Image>().color = Color.white;
                monthDaysName[i].GetComponent<Image>().color = Color.white;

            }

            // mark the calendar(each month) correcetly
            for (int i = 0; i < monthDays.Length; i++)
            {
                // if displayed month is equal to current month AND displayed year is equal to current year
                // AND i is equal to current day of month (basically: start marking the days from today on)
                if (displayedMonthText.text == System.DateTime.UtcNow.ToLocalTime().ToString("MMMM") 
                    && int.Parse(displayedYearText.text) == int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))
                {
                    if (i == int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd")))
                    {
                        for (int j = i - 1; j < monthDays.Length; j += intervall)
                        {
                            monthDays[j].GetComponent<Text>().text = "1";
                            //monthDaysImage[j].GetComponent<Image>().color = Color.black;
                            monthDaysName[j].GetComponent<Image>().color = Color.black;
                            debugg1.text = "DEBUG1: i" + i + ", j: " + j;
                            //string increaseMarkerString = monthDays[j].GetComponent<Text>().text.ToString();
                            //int increaseMarker = int.Parse(increaseMarkerString) + 1;
                            //monthDays[j].GetComponent<Text>().text = increaseMarker.ToString();
                        }
                    }
                }
                // if displayed month is smaller than current month OR displayed year is smaller than current year
                else if (int.Parse(monthIndexText.text) < int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("MM")) || int.Parse(displayedYearText.text) < int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))
                {
                    monthDays[i].GetComponent<Text>().text = "";
                }
                // if displayed month is bigger than current month OR displayed year is bigger than current year
                else if (int.Parse(monthIndexText.text) > int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("MM")) || int.Parse(displayedYearText.text) > int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))
                {
                    if (i == offsetRef)
                    {
                        Debug.Log("OFFSETREF: " + offsetRef);
                        for (int j = i - 1; j < monthDays.Length; j += intervall)
                        {
                            monthDays[j].GetComponent<Text>().text = "1";
                            //monthDaysImage[j].GetComponent<Image>().color = Color.blue;
                            monthDaysName[j].GetComponent<Image>().color = Color.blue;
                            debugg4.text = "montsdays length2: " + monthDays.Length;
                            debugg2.text = "DEBUG2: i" + i + ", j: " + j;

                            //string increaseMarkerString = monthDays[j].GetComponent<Text>().text.ToString();
                            //int increaseMarker = int.Parse(increaseMarkerString) + 1;
                            //monthDays[j].GetComponent<Text>().text = increaseMarker.ToString();
                        }
                    }
                }
                //else
                //{
                //    monthDays[i].GetComponent<Text>().text = "0";
                //}
            }
            // calulate offset based on which month you are currently displaying and which month you are going to display
            if (displayedMonthText.text == System.DateTime.UtcNow.ToLocalTime().ToString("MMMM"))
            {
                Debug.Log("intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall) = erg: \n" + intervall + " - ((" + lastDayOfMonthInt + " - " + currentMonathDay + ") % " + intervall + ") = " + (intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall)));
                offsetNext = intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall);
                offsetRef = offsetNext;
                //debugg3.text = "DEBUG3: " + "intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall) = erg: \n" + intervall + " - ((" + lastDayOfMonthInt + " - " + currentMonathDay + ") % " + intervall + ") = " + (intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall));
            }
            //else if (isNavLeft)
            //{
            //    Debug.Log("intervall - offsetPrev = erg: \n" + intervall + " - " + offsetPrev + " = " + (intervall - offsetPrev));
            //    offsetPrev = intervall - offsetRef;
            //    offsetRef = offsetPrev;
            //}
            else if (isNavRight)
            {
                Debug.Log("intervall - ((lastDayOfMonthInt - offsetRef) % intervall) = erg: \n" + intervall + " - ((" + lastDayOfMonthInt + " - " + offsetRef + ") % " + intervall + ") = " + (intervall - ((lastDayOfMonthInt - offsetRef) % intervall)));
                offsetNext = intervall - ((lastDayOfMonthInt - offsetRef) % intervall);
                offsetRef = offsetNext;
                //debugg4.text = "DEBUG4: " + "intervall - ((lastDayOfMonthInt - offsetRef) % intervall) = erg: \n" + intervall + " - ((" + lastDayOfMonthInt + " - " + offsetRef + ") % " + intervall + ") = " + (intervall - ((lastDayOfMonthInt - offsetRef) % intervall));
            }
            Debug.Log("!!!OFFSETREF: " + offsetRef);

            //// calulate offset based on which month you are currently displaying and which month you are going to display
            //if (displayedMonthText.text == System.DateTime.UtcNow.ToLocalTime().ToString("MMMM"))
            //{
            //    Debug.Log("intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall) = erg: \n" + intervall + " - ((" + lastDayOfMonthInt + " - " + currentMonathDay + ") % " + intervall + ") = " + (intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall)));
            //    offsetNext = intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall);
            //    offsetRef = offsetNext;
            //}
            //else if (isNavLeft)
            //{
            //    Debug.Log("intervall - offsetPrev = erg: \n" + intervall + " - " + offsetPrev + " = " + (intervall - offsetPrev));
            //    offsetPrev = intervall - offsetRef;
            //    offsetRef = offsetPrev;
            //}
            //else if (isNavRight)
            //{
            //    Debug.Log("intervall - ((lastDayOfMonthInt - offsetRef) % intervall) = erg: \n" + intervall + " - ((" + lastDayOfMonthInt + " - " + offsetRef + ") % " + intervall + ") = " + (intervall - ((lastDayOfMonthInt - offsetRef) % intervall)));
            //    offsetNext = intervall - ((lastDayOfMonthInt - offsetRef) % intervall);
            //    offsetRef = offsetNext;
            //}
            //Debug.Log("!!!OFFSETREF: " + offsetRef);
        }
    }

    public void IsNavLeft()
    {
        isNavLeft = true;
        isNavRight = false;
        Debug.Log("navleft: true");
    }
    public void IsNavRight()
    {
        isNavRight = true;
        isNavLeft = false;
        Debug.Log("navright: true");

    }
}
