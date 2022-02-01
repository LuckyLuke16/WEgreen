using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
* @brief
* @param
* @return
*/
public class ChangeScenes : MonoBehaviour
{
    /**
     * @brief
     * @param
     * @return
     */
    // funktion dient dazu mittels der button die Szenen zu ändern
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("button pressed: " + SceneName);
    }

    /**
     * @brief
     * @param
     * @return
     */
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

    /**
     * @brief
     * @param
     * @return
     */
    public void ExitButton()
    {
        Application.Quit();
    }
}
