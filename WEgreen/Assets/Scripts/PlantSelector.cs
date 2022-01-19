using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject Plant1Stage1;
    public GameObject Plant1Stage2;
    public GameObject Plant1Stage3;
    public GameObject Plant2Stage1;
    public GameObject Plant2Stage2;
    public GameObject Plant2Stage3;
    public GameObject Plant3Stage1;
    public GameObject Plant3Stage2;
    public GameObject Plant3Stage3;

    //slider fuer stage der pflanzen
    public GameObject slider;
    private int selectedStage=1;
    //sprites fuer button aktiv und inaktiv
    public Sprite spriteWhenBtnPressed;
    public Sprite spriteBtnNotPressed;
    //pflanzen auswahl buttons
    public Button aloaeButton;
    public Button tomatoButton;
    public Button appleButton;

    public void PlantSelectVisible()
    {
        ShowStageSelect = !ShowStageSelect;
        PlantStageSelectOn.SetActive(ShowStageSelect);
        slider.SetActive(ShowStageSelect);
    }

    //when slider changedValue
    public void onSliderValueChanged(float value)
    {
        selectedStage = (int)value;
        if (ShowPlant1)
        {
            switch (selectedStage)
            {
                case (0):
                    P1Stage1();
                    break;
                case (1):
                    P1Stage2();
                    break;
                case (2):
                    P1Stage3();
                    break;
            }
        }
        if (ShowPlant2)
        {
            switch (selectedStage)
            {
                case (0):
                    P2Stage1();
                    break;
                case (1):
                    P2Stage2();
                    break;
                case (2):
                    P2Stage3();
                    break;
            }
        }
        if (ShowPlant3)
        {
            switch (selectedStage)
            {
                case (0):
                    P3Stage1();
                    break;
                case (1):
                    P3Stage2();
                    break;
                case (2):
                    P3Stage3();
                    break;
            }
        }
    }
    // Plant Select Start

    public void SelectPlant1()
    {
        //change buttons sprite when activated
        ShowPlant1 = true;
        ShowPlant2 = false;
        ShowPlant3 = false;
        tomatoButton.image.sprite = spriteBtnNotPressed;
        aloaeButton.image.sprite = spriteWhenBtnPressed;
        appleButton.image.sprite = spriteBtnNotPressed;

        // Set all other plants to inacitve 
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);
        Plant3Stage1.SetActive(false);
        Plant3Stage2.SetActive(false);
        Plant3Stage3.SetActive(false);
        onSliderValueChanged(selectedStage);
    }
    public void SelectPlant2()
    {
        //change buttons sprite when activated
        ShowPlant2 = true;
        ShowPlant1 = false;
        ShowPlant3 = false;
        tomatoButton.image.sprite = spriteWhenBtnPressed;
        aloaeButton.image.sprite = spriteBtnNotPressed;
        appleButton.image.sprite = spriteBtnNotPressed;


        // Set all other plants to inacitve 
        Plant1Stage1.SetActive(false);
        Plant1Stage2.SetActive(false);
        Plant1Stage3.SetActive(false);
        Plant3Stage1.SetActive(false);
        Plant3Stage2.SetActive(false);
        Plant3Stage3.SetActive(false);
        onSliderValueChanged(selectedStage);
    }

    public void SelectPlant3()
    {
        //change buttons sprite when activated
        ShowPlant2 = false;
        ShowPlant1 = false;
        ShowPlant3 = true;
        tomatoButton.image.sprite = spriteBtnNotPressed;
        aloaeButton.image.sprite = spriteBtnNotPressed;
        appleButton.image.sprite = spriteWhenBtnPressed;

        // Set all other plants to inacitve 
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);
        onSliderValueChanged(selectedStage);
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


