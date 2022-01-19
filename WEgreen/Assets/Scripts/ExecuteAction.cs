using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class ExecuteAction : MonoBehaviour
{
    public Text title;
    //visibility button und bool f�r das �ndern des images
    public Button visibility;
    bool vis = false;
    //scale button soll bei ber�hrung den slider f�rs skalieren aktivieren/deaktivieren
    public Slider scaleSlider;
    private bool scaleSliderActive = false;
    private bool measureActive = false;
    public bool isAction = false;
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
            if(child.transform.gameObject.activeInHierarchy)
            {
               measurePrefab = child.transform.Find("MeasurePrefab").gameObject; 
            }
        }
    }

    public void ActionButton(string titlename)
    {
        title.text = titlename;
        isAction = true;
        switch (titlename) {
            case "Scaling":
                scaleSliderActive = !scaleSliderActive;
                scaleSlider.gameObject.SetActive(scaleSliderActive);
                break;
            case "Visibility":
                vis = !vis;
                visibility.transform.GetChild(0).gameObject.SetActive(!vis);
                visibility.transform.GetChild(1).gameObject.SetActive(vis);
                break;
            case "Measuring":
                measureActive = !measureActive;
                measurePrefab.SetActive(measureActive);
                break;

        }
             
    }

}
