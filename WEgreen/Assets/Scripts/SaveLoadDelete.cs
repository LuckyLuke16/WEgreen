using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

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
    
    [SerializeField]
    private Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // saves plant data
    public void Save()
    {
        debugText.text = "0DATA PATH:\n" + Application.persistentDataPath;
        LoadWateringPlantCounter();
        Debug.Log("SAFE-Anfang2!!!");
        // put input data into string array
        string[] plantData = new string[] {
            waterinPlantName.text,
            wateringPlantIntervall.text
        };

        // split the string elements with a separator and put them into a string and save the string by writing it into a text file
        string saveString = string.Join(SAVE_SEPARATOR, plantData);
        // load counter
        int counter = int.Parse(wateringPlantCounter);
        if(counter < 4)
        {
            Debug.Log("VORHER-VORHER!!!");
            // if file exists then create a new file with a higher counter, otherwise create file with current counter
            if (File.Exists(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
            {
                SaveWateringPlantCounter("increase");
                File.WriteAllText(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt", saveString);
            }
            else
            {
                debugText.text = "1DATA PATH: " + Application.persistentDataPath;
                Debug.Log("VORHER!!!");
                SaveWateringPlantCounter("increase");
                File.WriteAllText(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt", saveString);
                Debug.Log("NACHHER!!!");
            }
            Debug.Log("NACHHER-NACHHER!!!");
            debugText.text = "2DATA PATH: " + Application.persistentDataPath;

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
        string[] savedStrings = new string[int.Parse(wateringPlantCounter) + 1];
        string[] wateringPlantsData;
        wateringPlantsData = new string[2];
        string wateringPlantsDataString = "\n";
        for (int i = 0; i < savedStrings.Length; i++)
        {
            savedStrings[i] = File.ReadAllText(Application.persistentDataPath + "/PLANT_DATA_TEXT_FILE_NAME" + i + ".txt");
            wateringPlantsData = savedStrings[i].Split(new[] { SAVE_SEPARATOR }, System.StringSplitOptions.None);
            wateringPlantsDataString += "Pflanze " + (i + 1) + ": " + wateringPlantsData[0] + "\n";
            wateringPlantsDataString += "Intervall: " + wateringPlantsData[1] + "\n\n";
        }
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
        else if(update == "decrease")
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
        if(File.Exists(Application.persistentDataPath + "/wateringPlantCounter.txt"))
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
}
