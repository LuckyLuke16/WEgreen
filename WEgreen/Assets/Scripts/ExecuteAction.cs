using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExecuteAction : MonoBehaviour
{
    public Text title;

    //scale button soll bei ber�hrung den slider f�rs skalieren aktivieren/deaktivieren
    public Slider scaleButton;
    private bool scaleSliderActive = false;

    public bool isAction = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActionButton(string titlename)
    {
        title.text = titlename;
        isAction = true;
        switch (titlename) {
            case "Scaling":
                scaleSliderActive = !scaleSliderActive;
                scaleButton.gameObject.SetActive(scaleSliderActive);
                break;
        }
             
    }

}
