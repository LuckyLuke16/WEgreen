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
        // put input data into string array
        string[] plantData = new string[] {
            waterinPlantName.text,
            wateringPlantIntervall.text
        };

        // split the string elements with a separator and put them into a string and save the string by writing it into a text file
        string saveString = string.Join(SAVE_SEPARATOR, plantData);
        // load counter
        LoadWateringPlantCounter();
        int counter = int.Parse(wateringPlantCounter);
        if(counter < 4)
        {
            // if file exists then create a new file with a higher counter, otherwise create file with current counter
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
            {
                SaveWateringPlantCounter("increase");
                File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt", saveString);
            }
            else
            {
                SaveWateringPlantCounter("increase");
                File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt", saveString);
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
        int counter = int.Parse(wateringPlantCounter);
        string[] savedStrings;
        savedStrings = new string[int.Parse(wateringPlantCounter) + 1];
        string[] wateringPlantsData = new string[2];
        string wateringPlantsDataString = "\n";
        for (int i = 0; i < savedStrings.Length; i++)
        {
            savedStrings[i] = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/PLANT_DATA_TEXT_FILE_NAME" + i + ".txt");
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
            File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/wateringPlantCounter.txt", wateringPlantCounter);
        } 
        else if(update == "decrease")
        {
            counter--;
            wateringPlantCounter = counter.ToString();
            File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/wateringPlantCounter.txt", wateringPlantCounter);
        }
        else
        {
            Debug.Log("The is not a legal update mode.");
        }
    }

    // loads counter
    private void LoadWateringPlantCounter()
    {
        if(File.Exists(System.IO.Directory.GetCurrentDirectory() + "/wateringPlantCounter.txt"))
        {
            wateringPlantCounter = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/wateringPlantCounter.txt");
        }
        else
        {
            File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/wateringPlantCounter.txt", "-1");
            Debug.Log("The text file 'wateringPlantCounter.txt' does not exist. Therefore this file with the content '-1' has been created.");
        }
    }

    public void Delete()
    {
        LoadWateringPlantCounter();
        int counter = int.Parse(wateringPlantCounter);
        if (counter >= 0)
        {
            // if file exists then create a new file with a higher counter, otherwise create file with current counter
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt"))
            {
                File.Delete(System.IO.Directory.GetCurrentDirectory() + "/PLANT_DATA_TEXT_FILE_NAME" + wateringPlantCounter + ".txt");
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
