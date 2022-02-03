using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
* @brief This class manages the scene changes between the different menus.
*/
public class ChangeScenes : MonoBehaviour
{
    /**
     * @brief Loads the correct scene according to argument.
     * @param SceneName(string): Expects the name of the scene that wants to be loaded.
     * @return void
     */
    // funktion dient dazu mittels der button die Szenen zu ändern
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    /**
     * @brief Loads the "Home"(main menu) scene.
     * @return void
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
     * @brief Optional(not used) function which quits the application.
     * @return void
     */
    public void ExitButton()
    {
        Application.Quit();
    }
}
