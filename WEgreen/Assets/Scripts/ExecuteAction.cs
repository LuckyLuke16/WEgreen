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

    private float titleTime = 2.0f;

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
        //measurePrefab = cursor.objectToPlace.transform.Find("MeasurePrefab").gameObject;
        plantModels = cursor.objectToPlace.transform;
        //measurePrefab = new  GameObject();

        foreach(Transform child in plantModels)
        {
            if(child.transform.gameObject.active)
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
        switch (titlename) {
            case "Skalieren":
                scaleSliderActive = !scaleSliderActive;
                scaleSlider.gameObject.SetActive(scaleSliderActive);
                if (scaleSliderActive)
                {
                    scaleButton.image.sprite = spriteWhenBtnPressed;
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
                }
                else
                {
                    selectPlantButon.image.sprite = spriteBtnNotPressed;
                }
                break;

        }
             
    }

}
