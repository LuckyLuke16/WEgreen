using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // funktion dient dazu mittels der button die Szenen zu ändern
    public void loadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
