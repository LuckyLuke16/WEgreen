using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using TMPro;
public class SaveNotes : MonoBehaviour
{
    public TMP_InputField notesInputField;
    //public Text parentText;
    public TMP_Text inputFieldText;
    private string notesString;
    private static string path;
    private bool loadTextFromFile = false;    

    // Start is called before the first frame update
    // text aus Notes.txt soll beim start geladen werden und im Input Feld angezeigt werden
    void Start()
    {
        //pfad fuer lokale textdatei, die die notizen beinhaltet
        path = Application.persistentDataPath + "/Notes.txt";

        //text aus datei in notizen anzeigen beim start der notiz-funktion
        StreamReader reader = new StreamReader(path);
        notesString = reader.ReadToEnd();
        notesInputField.text = notesString;
        reader.Close();
    }

    // abspeichern der geschriebenen Notizen, beim verändern des textes im input feld 
    public void save()
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
