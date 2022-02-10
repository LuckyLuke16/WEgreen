using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
/**
 * @brief The class is responsible for changing the scene title and for the changing of ui elements.
 */
public class ExecuteAction : MonoBehaviour
{
    public Text title;
    public Button visibility;
    public Sprite spriteWhenBtnPressed;
    public Sprite spriteBtnNotPressed;
    bool vis = false;
    public GameObject aloeSprites;
    public GameObject tomatoSprites;
    public GameObject appleSprites;
    public Button scaleButton;
    public Button measureButton;
    public Button selectPlantButon;
    public Slider scaleSlider;
    private bool scaleSliderActive = false;

    private bool measureActive = false;
    private bool selectPlant = false;
    private bool isAction = false;

    private float titleTime = 1.50f;
    private float deltaTime = 1.50f;

    [SerializeField] private AR_Cursor cursor;
    private GameObject measurePrefab;
    private Transform plantModels;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= deltaTime)
        {
            // The title fades after 2 seconds.
            title.CrossFadeAlpha(0,0.6f,false);
        }

        plantModels = cursor.objectToPlace.transform;

        foreach(Transform child in plantModels)
        {
            if(child.transform.gameObject.activeInHierarchy)
            {
               measurePrefab = child.transform.Find("MeasurePrefab").gameObject; 
            }
        }

    }
    /**
     * @brief The title of the current AR- function is set and the sprites of the buttons as well as the button color is switched when pressed.
     * 
     * The title stands for 2 seconds and is then disappearing.
     * 
     * @param Name of the function that is currently used
     */
    private void ActionButton(string titlename)
    {
        if (titlename!="Visibility")
        {
            title.text = titlename;
        }
        isAction = true;
        switch (titlename)
        {
            case "Skalieren":
                scaleSliderActive = !scaleSliderActive;
                scaleSlider.gameObject.SetActive(scaleSliderActive);
                if (scaleSliderActive)
                {
                    scaleButton.image.sprite = spriteWhenBtnPressed;
                    deltaTime = Time.time + titleTime;
                    title.CrossFadeAlpha(1, 0, false);
                }
                else
                {
                    scaleButton.image.sprite = spriteBtnNotPressed;
                }
                break;
            case "Visibility":
                vis = !vis;
                visibility.transform.GetChild(0).gameObject.SetActive(!vis);
                visibility.transform.GetChild(1).gameObject.SetActive(vis);
                break;
            case "Measuring":
                measureActive = !measureActive;
                measurePrefab.SetActive(measureActive);
                if (measureActive)
                {
                    measureButton.image.sprite = spriteWhenBtnPressed;
                    deltaTime = Time.time + titleTime;
                    title.CrossFadeAlpha(1, 0, false);
                }
                else
                {
                    measureButton.image.sprite = spriteBtnNotPressed;
                }
                break;
            case "Pflanzenauswahl":
                selectPlant = !selectPlant;
                if (selectPlant)
                {
                    selectPlantButon.image.sprite = spriteWhenBtnPressed;
                    deltaTime = Time.time + titleTime;
                    title.CrossFadeAlpha(1, 0, false);
                }
                else
                {
                    selectPlantButon.image.sprite = spriteBtnNotPressed;
                }
                break;
            case "Aloe Vera":
                aloeSprites.SetActive(true);
                tomatoSprites.SetActive(false);
                appleSprites.SetActive(false);
                break;
            case "Tomate":
                aloeSprites.SetActive(false);
                tomatoSprites.SetActive(true);
                appleSprites.SetActive(false);
                break;
            case "Apfelbaum":
                aloeSprites.SetActive(false);
                tomatoSprites.SetActive(false);
                appleSprites.SetActive(true);
                break;


        }
        if (title.text=="Tomate"|| title.text == "Aloe Vera" || title.text == "Apfelbaum")
        {
            deltaTime = Time.time + titleTime;
            title.CrossFadeAlpha(1, 0, false);
        }
        
             
    }

}
