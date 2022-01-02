using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlantSelector : MonoBehaviour
{
    public static bool ShowStageSelect = false;
    public static bool ShowPlant1 = false;
    public static bool ShowPlant2 = false;
    public static bool ShowPlant3 = false;
    public static bool ShowPlantStage1 = false;
    public static bool ShowPlantStage2 = false;
    public static bool ShowPlantStage3 = false;
    public GameObject PlantStageSelectOn;
    public GameObject PlantSelect1;
    public GameObject PlantSelect2;
    public GameObject PlantSelect3;
    public GameObject Plant1Stage1;
    public GameObject Plant1Stage2;
    public GameObject Plant1Stage3;
    public GameObject Plant2Stage1;
    public GameObject Plant2Stage2;
    public GameObject Plant2Stage3;
    public GameObject Plant3Stage1;
    public GameObject Plant3Stage2;
    public GameObject Plant3Stage3;

    public void PlantSelectVisible()
    {
        ShowStageSelect = !ShowStageSelect;
        PlantStageSelectOn.SetActive(ShowStageSelect);
    }

    // Plant Select Start

    public void SelectPlant1()
    {
        ShowPlant1 = !ShowPlant1;
        PlantSelect1.SetActive(ShowPlant1);

        ShowPlant2 = false;
        PlantSelect2.SetActive(false);

        ShowPlant3 = false;
        PlantSelect3.SetActive(false);

        // Set all other plants to inacitve 
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);
        Plant3Stage1.SetActive(false);
        Plant3Stage2.SetActive(false);
        Plant3Stage3.SetActive(false);
    }
    public void SelectPlant2()
    {
        ShowPlant1 = false;
        PlantSelect1.SetActive(false);

        ShowPlant2 = !ShowPlant2;
        PlantSelect2.SetActive(ShowPlant2);

        ShowPlant3 = false;
        PlantSelect3.SetActive(false);

        // Set all other plants to inacitve 
        Plant1Stage1.SetActive(false);
        Plant1Stage2.SetActive(false);
        Plant1Stage3.SetActive(false);
        Plant3Stage1.SetActive(false);
        Plant3Stage2.SetActive(false);
        Plant3Stage3.SetActive(false);
    }

    public void SelectPlant3()
    {
        ShowPlant1 = false;
        PlantSelect1.SetActive(false);

        ShowPlant2 = false;
        PlantSelect2.SetActive(false);

        ShowPlant3 = !ShowPlant3;
        PlantSelect3.SetActive(ShowPlant3);

        // Set all other plants to inacitve 
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);
    }
    // Plant Select End

    // Plant1 Stage Select Start

    public void P1Stage1()
    {
        ShowPlantStage1 = !ShowPlantStage1;
        Plant1Stage1.SetActive(ShowPlantStage1);

        ShowPlantStage2 = false;
        Plant1Stage2.SetActive(false);

        ShowPlantStage3 = false;
        Plant1Stage3.SetActive(false);
    }

    public void P1Stage2()
    {
        ShowPlantStage1 = false;
        Plant1Stage1.SetActive(false);

        ShowPlantStage2 = !ShowPlantStage2;
        Plant1Stage2.SetActive(ShowPlantStage2);

        ShowPlantStage3 = false;
        Plant1Stage3.SetActive(false);
    }

    public void P1Stage3()
    {
        ShowPlantStage1 = false;
        Plant1Stage1.SetActive(false);

        ShowPlantStage2 = false;
        Plant1Stage2.SetActive(false);

        ShowPlantStage3 = !ShowPlantStage3;
        Plant1Stage3.SetActive(ShowPlantStage3);
    }
    // Plant1 Stage Select End

    // Plant2 Stage Select Start

    public void P2Stage1()
    {
        ShowPlantStage1 = !ShowPlantStage1;
        Plant2Stage1.SetActive(ShowPlantStage1);

        ShowPlantStage2 = false;
        Plant2Stage2.SetActive(false);

        ShowPlantStage3 = false;
        Plant2Stage3.SetActive(false);


    }

    public void P2Stage2()
    {
        ShowPlantStage1 = false;
        Plant2Stage1.SetActive(false);

        ShowPlantStage2 = !ShowPlantStage2;
        Plant2Stage2.SetActive(ShowPlantStage2);

        ShowPlantStage3 = false;
        Plant2Stage3.SetActive(false);
    }

    public void P2Stage3()
    {
        ShowPlantStage1 = false;
        Plant2Stage1.SetActive(false);

        ShowPlantStage2 = false;
        Plant2Stage2.SetActive(false);

        ShowPlantStage3 = !ShowPlantStage3;
        Plant2Stage3.SetActive(ShowPlantStage3);
    }
    // Plant2 Stage Select End

    // Plant3 Stage Select Start

    public void P3Stage1()
    {
        ShowPlantStage1 = !ShowPlantStage1;
        Plant3Stage1.SetActive(ShowPlantStage1);

        ShowPlantStage2 = false;
        Plant3Stage2.SetActive(false);

        ShowPlantStage3 = false;
        Plant3Stage3.SetActive(false);
    }

    public void P3Stage2()
    {
        ShowPlantStage1 = false;
        Plant3Stage1.SetActive(false);

        ShowPlantStage2 = !ShowPlantStage2;
        Plant3Stage2.SetActive(ShowPlantStage2);

        ShowPlantStage3 = false;
        Plant3Stage3.SetActive(false);
    }

    public void P3Stage3()
    {
        ShowPlantStage1 = false;
        Plant3Stage1.SetActive(false);

        ShowPlantStage2 = false;
        Plant3Stage2.SetActive(false);

        ShowPlantStage3 = !ShowPlantStage3;
        Plant3Stage3.SetActive(ShowPlantStage3);
    }
    // Plant3 Stage Select End

}


