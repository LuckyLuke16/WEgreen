using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using TMPro;
/**
 *@brief The class handles the loading, saving and deleting of the notes.
 *
 */
public class SaveNotes : MonoBehaviour
{
    public TMP_InputField notesInputField;
    public TMP_Text inputFieldText;
    private string notesString;
    private static string path;
    private bool loadTextFromFile = false;

    // Start is called before the first frame update
    /**
     * @brief At the start of the scene the text from the locally saved "Notes.txt" file is written in the TMPro Input Field.
     */
    void Start()
    {
        path = Application.persistentDataPath + "/Notes.txt";
        StreamReader reader = new StreamReader(path);
        notesString = reader.ReadToEnd();
        notesInputField.text = notesString;
        reader.Close();
    }
    /**
     * @brief Saves the input of the TMPro Input Field when the text in it was changed.
     * 
     * The file input is overwritten everytime the notes have to be changed.
     */
    private void save()
    {
        if (loadTextFromFile)
        {
            path = Application.persistentDataPath + "/Notes.txt";
            //vor dem speichern löschen des gesamten textes damit text "überschrieben" werden kann
            File.WriteAllText(path, String.Empty);
            this.notesString = inputFieldText.text;
            StreamWriter writer = new StreamWriter(path, true);
            writer.Write(notesString);
            writer.Close();
            
        }
        loadTextFromFile = true;
            
    }

}
