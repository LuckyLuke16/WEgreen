using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

/**
* @brief Manages all the saving, loading and deleting activities for the watering plants.
*/
public class SaveLoadDelete : MonoBehaviour
{
    // declaring and initializing constant variables
    private const string SAVE_SEPARATOR = " -#SAVE_SEPARATOR#- ";
    //private const string PLANT_DATA_TEXT_FILE_NAME = "wateringPlantData";

    // declaring and partly initializing variables
    [SerializeField]
    private Text waterinPlantName;
    [SerializeField]
    private Text wateringPlantIntervall;
    [SerializeField]
    private Text wateringPlantsOverviewText;

    private string wateringPlantCounter = "";
    private string wateringPlantsDataString = "";

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

    private GameObject[] monthDays;
    private GameObject[] monthDaysImage;
    private GameObject[] monthDaysName;

    private int intervall = 1;
    private int offsetPrev;
    private int offsetNext;
    private int offsetRef;

    private int currentMonthDay;
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
        // loads all existing data that has been saved
        Load();

        // initialize all needed gameobjects for the calculations regarding the correctness of the calendar e.g. marking the days that need to be watered
        monthDays = GameObject.FindGameObjectsWithTag("wateringMark");
        monthDaysImage = GameObject.FindGameObjectsWithTag("month_days");
        monthDaysName = new GameObject[31];
        for (int i = 1; i < 32; i++)
        {
            monthDaysName[i - 1] = GameObject.Find(i.ToString());
        }

        MarkWateringDays();
    }

    /**
     * @brief Saves plant data(name, interval) into a text file
     * @return void
     */
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
     * @brief Loads all the watering plant data that has already been saved.
     * @return void
     */
    public void Load()
    {
        LoadWateringPlantCounter();
        CollectWateringPlantData();
        // display it
        wateringPlantsOverviewText.text = wateringPlantsDataString;
    }

    /**
     * @brief Saves / updates watering plant counter.
     * @param update(string): expects either "increase" or "decrease" as argument, to adjust 
     * and save the watering plant counter (needed for adding limit and naming text files with the different watering plant data.
     * @return void
     */
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
     * @brief Loads the watering plant counter so the app doesn't always start from nothing even thought watering plant data already exsists.
     * @return void
     */
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
     * @brief Deletes the last added watering plant data.
     * @return void
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
     * @brief Collets all the watering plant that exists(saved) and put them into a string in a formated way, so it can be used to display on the UI.
     * @return void
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
     * @brief Responsible for all the calculations so that the calendar marks all the days that need to be watered depending on the intervall of the added(most recent) watering plant.
     * @return void
     */
    public void MarkWateringDays()
    {
        // get counter
        string wateringPlantCounter = File.ReadAllText(Application.persistentDataPath + "/wateringPlantCounter.txt");
        // get last day of month which gives us the amount of days a month has
        firstDayOfMonth = new DateTime(int.Parse(displayedYearText.text), int.Parse(monthIndexText.text), 1);
        lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        
        // adjust the arguments of Datetime(firstDayOfPrevMonth) so there won't be any conflicts between index and the actual date that is needed 
        // -> then we can find the first day of the previous month correctly
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
        if (File.Exists(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
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
                currentMonthDay = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd"));
                lastDayOfMonthInt = int.Parse(lastDayOfMonth.ToString("dd"));
                lastDayOfPrevMonthInt = int.Parse(lastDayOfPrevMonth.ToString("dd"));
            }

            // reset month values
            for (int i = 0; i < monthDays.Length; i++)
            {
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
                            monthDaysName[j].GetComponent<Image>().color = Color.green;
                        }
                    }
                }
                // if (displayed month is bigger than current month AND displayed year is equal to current year) OR displayed year is bigger than current year
                else if (((int.Parse(monthIndexText.text) > int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("MM"))) && (int.Parse(displayedYearText.text) == int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))) 
                        || int.Parse(displayedYearText.text) > int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("yyyy")))
                {
                    // depending on whether you want to navigate forward or backward, a different offset is needed for marking the correct watering days
                    if (isNavLeft)
                    {
                        offsetRef = offsetPrev;
                        if (i == offsetRef)
                        {
                            for (int j = i; j < monthDays.Length; j += intervall)
                            {
                                monthDaysName[j - 1].GetComponent<Image>().color = Color.green;
                            }
                        }
                    }
                    else if (isNavRight)
                    {
                        offsetRef = offsetNext;
                        if (i == offsetRef)
                        {
                            for (int j = i - 1; j < monthDays.Length; j += intervall)
                            {
                                monthDaysName[j].GetComponent<Image>().color = Color.green;
                            }
                        }
                    }

                }
            }
            // calulate offset based on which month you are currently displaying and which month you are going to display
            if (displayedMonthText.text == System.DateTime.UtcNow.ToLocalTime().ToString("MMMM"))
            {
                offsetNext = intervall - ((lastDayOfMonthInt - currentMonthDay) % intervall);
                offsetRef = offsetNext;
            }
            else
            {
                offsetNext = intervall - ((lastDayOfMonthInt - offsetRef) % intervall);

                offsetPrev = (lastDayOfPrevMonthInt - (intervall - offsetRef)) % intervall;
                if (offsetPrev == 0)
                {
                    offsetPrev = intervall;
                }
            }
        }
    }

    /**
     * @brief This is used fir when you navigate to the previous month. It will set the booleans isNavLeft to true and isNavRight to false, 
     *  so that the correct offset is used for the calculations of marking the watering days
     * @return void
     */
    public void IsNavLeft()
    {
        isNavLeft = true;
        isNavRight = false;
    }

    /**
     * @brief This is used fir when you navigate to the previous month. It will set the booleans isNavRight to true and isNavLeft to false, 
     *  so that the correct offset is used for the calculations of marking the watering days
     * @return void
     */
    public void IsNavRight()
    {
        isNavRight = true;
        isNavLeft = false;
    }

    /**
     * @brief Activates / Opens up the error message window when saving an watering plant with an invalid intervall
     * @return void
     */
    public void OpenErrorMessageWindow()
    {
        errorMessageWindow.SetActive(true);
    }

    /**
     * @brief Deactivates / Closes the error message window 
     * @return void
     */
    public void CloseErrorMessageWindow()
    {
        errorMessageWindow.SetActive(false);
    }
}
