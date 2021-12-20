using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // funktion dient dazu mittels der button die Szenen zu ändern
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("button pressed: " + SceneName);
    }

    public void BackToHomeButton()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("You are already in the main menu.");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
