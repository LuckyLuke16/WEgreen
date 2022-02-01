using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

/**
* @brief
* @param
* @return
*/
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
    //[SerializeField]
    //private Text debugg1;
    //[SerializeField]
    //private Text debugg2;
    //[SerializeField]
    //private Text debugg3;
    //[SerializeField]
    //private Text debugg4;


    private GameObject[] monthDays;
    private GameObject[] monthDaysImage;
    private GameObject[] monthDaysName;
    //private Text[] monthDaysText;

    private int intervall = 1;
    private int offsetPrev;
    private int offsetNext;
    private int offsetRef;

    private int currentMonathDay;
    private int lastDayOfMonthInt;
    private int lastDayOfPrevMonthInt;

    private bool isNavLeft;
    private bool isNavRight = true;

    private DateTime firstDayOfMonth;
    private DateTime lastDayOfMonth;
    private DateTime firstDayOfPrevMonth;
    private DateTime lastDayOfPrevMonth;

    public GameObject errorMessageWindow;

    // Start is called before the first frame update
    void Start()
    {
        Load();


        monthDays = GameObject.FindGameObjectsWithTag("wateringMark");
        monthDaysImage = GameObject.FindGameObjectsWithTag("month_days");
        monthDaysName = new GameObject[31];
        for (int i = 1; i < 32; i++)
        {
            monthDaysName[i - 1] = GameObject.Find(i.ToString());
        }

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

    /**
     * @brief
     * @param
     * @return
     */
    // saves plant data
    public void Save()
    {
        // interval between 0 and 999 only allowed, otherwise error message
        if (int.Parse(wateringPlantIntervall.text) > 0 && int.Parse(wateringPlantIntervall.text) < 999)
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
        else
        {
            OpenErrorMessageWindow();
        }
    }

    /**
     * @brief
     * @param
     * @return
     */
    // loads plant data
    public void Load()
    {
        LoadWateringPlantCounter();
        CollectWateringPlantData();
        // display it
        wateringPlantsOverviewText.text = wateringPlantsDataString;
    }

    /**
     * @brief
     * @param
     * @return
     */
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

    /**
     * @brief
     * @param
     * @return
     */
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

    /**
     * @brief
     * @param
     * @return
     */
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

    /**
     * @brief
     * @param
     * @return
     */
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

    /**
     * @brief
     * @param
     * @return
     */
    public void MarkWateringDays()
    {
        // get counter
        string wateringPlantCounter = File.ReadAllText(Application.persistentDataPath + "/wateringPlantCounter.txt");
        // get last day of month which gives us the amount of days a month has
        firstDayOfMonth = new DateTime(int.Parse(displayedYearText.text), int.Parse(monthIndexText.text), 1);
        lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        
        
        if (int.Parse(monthIndexText.text) == 1)
        {
            firstDayOfPrevMonth = new DateTime(int.Parse(displayedYearText.text) - 1, 12, 1);
        }
        else if (int.Parse(monthIndexText.text) > 1)
        {
            firstDayOfPrevMonth = new DateTime(int.Parse(displayedYearText.text), (int.Parse(monthIndexText.text) - 1), 1);
        }
        lastDayOfPrevMonth = firstDayOfPrevMonth.AddMonths(1).AddDays(-1);

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
                lastDayOfPrevMonthInt = int.Parse(lastDayOfPrevMonth.ToString("dd"));
                //offset = intervall - ((lastDayOfMonthInt - currentMonathDay) % intervall);


                //wateringPlantsDataString += "Pflanze " + (i + 1) + ": " + wateringPlantsData[0] + "\n";
                //wateringPlantsDataString += "Intervall: " + wateringPlantsData[1] + "\n\n";
            }

            // reset month values
            for (int i = 0; i < monthDays.Length; i++)
            {
                //monthDays[i].GetComponent<Text>().text = "";
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
                            //monthDays[j].GetComponent<Text>().text = "1";
                            monthDaysName[j].GetComponent<Image>().color = Color.green;
                            //string increaseMarkerString = monthDays[j].GetComponent<Text>().text.ToString();
                            //int increaseMarker = int.Parse(increaseMarkerString) + 1;
                            //monthDays[j].GetComponent<Text>().text = increaseMarker.ToString();
                        }
                    }
                }
                // if displayed month is smaller than current month OR displayed year is smaller than current year
                //else if (int.Parse(monthIndexText.text) < int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("MM")) || int.Parse(displayedYearText.text) < int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))
                //{
                //    monthDays[i].GetComponent<Text>().text = "";
                //}
                // if (displayed month is bigger than current month AND displayed year is equal to current year) OR displayed year is bigger than current year
                else if (((int.Parse(monthIndexText.text) > int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("MM"))) && (int.Parse(displayedYearText.text) == int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))) 
                        || int.Parse(displayedYearText.text) > int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))
                {
                    if (isNavLeft)
                    {
                        offsetRef = offsetPrev;
                        if (i == offsetRef)
                        {
                            Debug.Log("OFFSETREF222: " + offsetRef);
                            for (int j = i; j < monthDays.Length; j += intervall)
                            {
                                //monthDays[j - 1].GetComponent<Text>().text = "1";
                                monthDaysName[j - 1].GetComponent<Image>().color = Color.green;

                                //string increaseMarkerString = monthDays[j].GetComponent<Text>().text.ToString();
                                //int increaseMarker = int.Parse(increaseMarkerString) + 1;
                                //monthDays[j].GetComponent<Text>().text = increaseMarker.ToString();
                            }
                        }
                    }
                    else if (isNavRight)
                    {
                        offsetRef = offsetNext;
                        if (i == offsetRef)
                        {
                            Debug.Log("OFFSETREF333: " + offsetRef);
                            for (int j = i - 1; j < monthDays.Length; j += intervall)
                            {
                                //monthDays[j].GetComponent<Text>().text = "1";
                                monthDaysName[j].GetComponent<Image>().color = Color.green;

                                //string increaseMarkerString = monthDays[j].GetComponent<Text>().text.ToString();
                                //int increaseMarker = int.Parse(increaseMarkerString) + 1;
                                //monthDays[j].GetComponent<Text>().text = increaseMarker.ToString();
                            }
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
            else
            {
                Debug.Log("offsetNext: intervall - ((lastDayOfMonthInt - offsetRef) % intervall) = erg: \n" + intervall + " - ((" + lastDayOfMonthInt + " - " + offsetRef + ") % " + intervall + ") = " + (intervall - ((lastDayOfMonthInt - offsetRef) % intervall)));
                offsetNext = intervall - ((lastDayOfMonthInt - offsetRef) % intervall);
                Debug.Log("intervall - offsetRef = erg: \n" + intervall + " - " + offsetRef + " = " + (intervall - offsetRef));

                offsetPrev = (lastDayOfPrevMonthInt - (intervall - offsetRef)) % intervall;
                if (offsetPrev == 0)
                {
                    offsetPrev = intervall;
                }
                Debug.Log("(" + lastDayOfPrevMonthInt + " - " + (intervall - offsetRef) + ")" + " % " + intervall);
                Debug.Log("offsetPrev: " + offsetPrev);

            }
            Debug.Log("!!!OFFSETREF: " + offsetRef);
        }
    }

    /**
     * @brief
     * @param
     * @return
     */
    public void IsNavLeft()
    {
        isNavLeft = true;
        isNavRight = false;
        Debug.Log("navleft: true");
    }

    /**
     * @brief
     * @param
     * @return
     */
    public void IsNavRight()
    {
        isNavRight = true;
        isNavLeft = false;
        Debug.Log("navright: true");

    }

    /**
     * @brief
     * @param
     * @return
     */
    public void OpenErrorMessageWindow()
    {
        errorMessageWindow.SetActive(true);
    }

    /**
     * @brief
     * @param
     * @return
     */
    public void CloseErrorMessageWindow()
    {
        errorMessageWindow.SetActive(false);
    }
}
