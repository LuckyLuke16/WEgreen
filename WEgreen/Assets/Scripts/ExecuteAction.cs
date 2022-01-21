using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class ExecuteAction : MonoBehaviour
{
    public Text title;
    //visibility button und bool fuer das aendern des images
    public Button visibility;
    public Sprite spriteWhenBtnPressed;
    public Sprite spriteBtnNotPressed;
    bool vis = false;
    //pflanzensprites an slider fuer pflanzenstages
    public GameObject aloeSprites;
    public GameObject tomatoSprites;
    public GameObject appleSprites;
    //Hauptfunktions-Buttons 
    public Button scaleButton;
    public Button measureButton;
    public Button selectPlantButon;

    //scale button soll bei beruehrung den slider fuers skalieren aktivieren/deaktivieren
    public Slider scaleSlider;
    private bool scaleSliderActive = false;

    //funktionen aktivität
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
        //der text verblasst nach einer gewissen zeit
        if (Time.time >= deltaTime)
        {
            title.CrossFadeAlpha(0,0.6f,false);
        }

        //measurePrefab = cursor.objectToPlace.transform.Find("MeasurePrefab").gameObject;
        plantModels = cursor.objectToPlace.transform;
        //measurePrefab = new  GameObject();

        foreach(Transform child in plantModels)
        {
            if(child.transform.gameObject.activeInHierarchy)
            {
               measurePrefab = child.transform.Find("MeasurePrefab").gameObject; 
            }
        }

    }

    public void ActionButton(string titlename)
    {
        //die visibilityfunktion soll keinen titel bekommen
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
                    //titel verschwindet nach gewisser zeit und löst sich langsam auf ebenso bei den anderen funktionen
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
        //wenn pflanzenart ausgewählt wird soll der name ebenfalls oben erscheinen
        if (title.text=="Tomate"|| title.text == "Aloe Vera" || title.text == "Apfelbaum")
        {
            deltaTime = Time.time + titleTime;
            title.CrossFadeAlpha(1, 0, false);
        }
        
             
    }

}
