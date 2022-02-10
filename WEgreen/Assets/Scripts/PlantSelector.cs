using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/**
 * @brief Manages the plants to select and its stage 
 * 
 */
public class PlantSelector : MonoBehaviour
{
    public static bool ShowStageSelect = false;
    public static bool ShowPlant1 = false;
    public static bool ShowPlant2 = false;
    public static bool ShowPlant3 = false;
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
    public GameObject slider;
    private int selectedStage = 1;
    public Sprite spriteWhenBtnPressed;
    public Sprite spriteBtnNotPressed;
    public Button aloaeButton;
    public Button tomatoButton;
    public Button appleButton;

    /**
     * @brief Handles the visibility of the plant selection ui elements.
     */
    public void PlantSelectVisible()
    {
        ShowStageSelect = !ShowStageSelect;
        PlantStageSelectOn.SetActive(ShowStageSelect);
        slider.SetActive(ShowStageSelect);
    }

    /**
     * @brief When the slider value is changed the stage of the plant model is switched accordingly.
     * 
     * @param float Value of the plant selection slider (can be 0, 1 or 2)
     */
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
    /**
     * @brief Selects the first plant (Aloe Vera) by setting ShowPlant1 to true and setting ShowPlant2 and ShowPlant3 to false to prevent showing multiple plants at once
     * @return void
     */
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
        Plant2Stage1.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage2.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant2Stage3.SetActive(false);
        Plant2Stage3.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant3Stage1.SetActive(false);
        Plant3Stage1.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant3Stage2.SetActive(false);
        Plant3Stage2.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant3Stage3.SetActive(false);
        onSliderValueChanged(selectedStage);
        Plant3Stage3.transform.Find("MeasurePrefab").gameObject.SetActive(false);
    }

    /**
     * @brief Selects the second plant (Tomato Plant) by setting ShowPlant2 to true and setting ShowPlant1 and ShowPlant3 to false to prevent showing multiple plants at once
     * @return void
     */
    public void SelectPlant2()
    {
        // Change buttons sprite when activated
        ShowPlant2 = true;
        ShowPlant1 = false;
        ShowPlant3 = false;
        tomatoButton.image.sprite = spriteWhenBtnPressed;
        aloaeButton.image.sprite = spriteBtnNotPressed;
        appleButton.image.sprite = spriteBtnNotPressed;


        // Set all other plants to inacitve 
        Plant1Stage1.SetActive(false);
        Plant1Stage1.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant1Stage2.SetActive(false);
        Plant1Stage2.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant1Stage3.SetActive(false);
        Plant1Stage3.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant3Stage1.SetActive(false);
        Plant3Stage1.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant3Stage2.SetActive(false);
        Plant3Stage2.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant3Stage3.SetActive(false);
        onSliderValueChanged(selectedStage);
        Plant3Stage3.transform.Find("MeasurePrefab").gameObject.SetActive(false);
    }

    /**
     * @brief Selects the third plant (Apple Tree) by setting ShowPlant3 to true and setting ShowPlant1 and ShowPlant2 to false to prevent showing multiple plants at once
     * @return void
     */
    public void SelectPlant3()
    {
        // Change buttons sprite when activated
        ShowPlant2 = false;
        ShowPlant1 = false;
        ShowPlant3 = true;
        tomatoButton.image.sprite = spriteBtnNotPressed;
        aloaeButton.image.sprite = spriteBtnNotPressed;
        appleButton.image.sprite = spriteWhenBtnPressed;

        // Set all other plants to inacitve 
        Plant1Stage1.SetActive(false);
        Plant1Stage1.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant1Stage2.SetActive(false);
        Plant1Stage2.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant1Stage3.SetActive(false);
        Plant1Stage3.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant2Stage1.SetActive(false);
        Plant2Stage1.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage2.transform.Find("MeasurePrefab").gameObject.SetActive(false);
        Plant2Stage3.SetActive(false);
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);
        onSliderValueChanged(selectedStage);
        Plant2Stage3.transform.Find("MeasurePrefab").gameObject.SetActive(false);
    }
    // Plant Select End

    // Plant1 Stage Select Start
    // Plant 1 (Aloe Vera) Stage 1
    /**
    * @brief Selects the first stage for the first plant (Aloe Vera) by setting Plant1Stage1 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P1Stage1()
    {
        Plant1Stage1.SetActive(true);
        Plant1Stage2.SetActive(false);
        Plant1Stage3.SetActive(false);
    }

    // Plant 1 (Aloe Vera) Stage 2
    /**
    * @brief Selects the second stage for the first plant (Aloe Vera) by setting Plant1Stage2 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P1Stage2()
    {
        Plant1Stage1.SetActive(false);
        Plant1Stage2.SetActive(true);
        Plant1Stage3.SetActive(false);
    }

    // Plant 1 (Aloe Vera) Stage 3
    /**
    * @brief Selects the third stage for the first plant (Aloe Vera) by setting Plant1Stage3 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P1Stage3()
    {
        Plant1Stage1.SetActive(false);
        Plant1Stage2.SetActive(false);
        Plant1Stage3.SetActive(true);
    }
    // Plant1 Stage Select End

    // Plant2 Stage Select Start
    // Plant 2 (Tomato Plant) Stage 1
    /**
    * @brief Selects the first stage for the second plant (Tomato Plant) by setting Plant2Stage1 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P2Stage1()
    {
        Plant2Stage1.SetActive(true);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(false);


    }

    // Plant 2 (Tomato Plant) Stage 2
    /**
    * @brief Selects the second stage for the second plant (Tomato Plant) by setting Plant2Stage2 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P2Stage2()
    {
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(true);
        Plant2Stage3.SetActive(false);
    }

    // Plant 2 (Tomato Plant) Stage 3
    /**
    * @brief Selects the third stage for the second plant (Tomato Plant) by setting Plant2Stage3 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P2Stage3()
    {
        Plant2Stage1.SetActive(false);
        Plant2Stage2.SetActive(false);
        Plant2Stage3.SetActive(true);
    }

    // Plant2 Stage Select End

    // Plant3 Stage Select Start
    // Plant 3 (Apple Tree) Stage 1
    /**
    * @brief Selects the first stage for the third plant (Apple Tree) by setting Plant3Stage1 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P3Stage1()
    {
        Plant3Stage1.SetActive(true);
        Plant3Stage2.SetActive(false);
        Plant3Stage3.SetActive(false);
    }

    // Plant 3 (Apple Tree) Stage 2
    /**
    * @brief Selects the second stage for the third plant (Apple Tree) by setting Plant3Stage2 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P3Stage2()
    {
        Plant3Stage1.SetActive(false);
        Plant3Stage2.SetActive(true);
        Plant3Stage3.SetActive(false);
    }

    // Plant 3 (Apple Tree) Stage 3
    /**
    * @brief Selects the third stage for the third plant (Apple Tree) by setting Plant3Stage3 to active and setting everything else to false / inactive
    * to prevent showing multiple plant stages at once
    * @return void
    */
    public void P3Stage3()
    {
        Plant3Stage1.SetActive(false);
        Plant3Stage2.SetActive(false);
        Plant3Stage3.SetActive(true);
    }

}


