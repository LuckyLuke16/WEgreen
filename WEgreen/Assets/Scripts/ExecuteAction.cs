using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExecuteAction : MonoBehaviour
{
    public Text title;
    //visibility button und bool für das ändern des images
    public Button visibility;
    bool vis = false;
    //scale button soll bei berührung den slider fürs skalieren aktivieren/deaktivieren
    public Slider scaleSlider;
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
                scaleSlider.gameObject.SetActive(scaleSliderActive);
                break;
            case "Visibility":
                vis = !vis;
                visibility.transform.GetChild(0).gameObject.SetActive(!vis);
                visibility.transform.GetChild(1).gameObject.SetActive(vis);
                break;
        }
             
    }

}
