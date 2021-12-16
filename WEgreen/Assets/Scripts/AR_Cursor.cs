using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AR_Cursor : MonoBehaviour
{
   
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public GameObject actions;
    public ARRaycastManager raycastManager;
    public ARPlaneManager aRPlaneManager;
    public GameObject movingPlantToPlace;
    
    //maximale anzahl an platzierbaren pflanzen und counter für pflanzenanzahl
    public int amountOfPlants = 0;
    private int maxAmountOfPlants = 3;

    //Dialog wenn maximales pflanzenlimit erreicht ist
    public GameObject maxPlantReachedDialouge;

    //list mit hits des raycasts mit einer plane
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public bool useCursor = true;
    public bool visibility = true;

    GameObject[] placedPlants;
    
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(true);

        movingPlantToPlace.SetActive(true);
        placedPlants = new GameObject[maxAmountOfPlants];
    }

    // Update is called once per frame
    /*
     * bei touch auf bildschirm wird eine pflanze auf die Stelle des hits des raycasts mit einer generierten plane gesetzt
     * max. 3 pflanzen moeglich zu generieren (performance) 
    */
    void Update()
    {
      
        if (useCursor)
        {
            updateCursorAndPlant();
        }

        //instanziieren eines modellpflanze an der momentanen position des cursors
        /*
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && amountOfPlants < 3)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
            amountOfPlants++;
           
        }
        */


    }
    /*
     * updated die position und rotation des cursors wenn hit mit plane aufgetreten ist
     * Modell-Pflanze ist immer auf cursor
    */
    void updateCursorAndPlant()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            //cursor soll erst gerendert werden, wenn raycast eine plane getroffen hat
            objectToPlace.SetActive(true);

            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            
            movingPlantToPlace.transform.position = hits[0].pose.position;
            movingPlantToPlace.transform.rotation = hits[0].pose.rotation;
        }
    }

    //skalierfunktion, die beim benutzen des sliders verwendet wird
    //scaleValue wird vom slider übergeben
    public void changeScale(float scaleValue)
    {
        movingPlantToPlace.transform.localScale = Vector3.one * scaleValue;

    }

    //pflanze wird auf die derzeitige position des cursors platziert, wenn mehr als 3 pflanzen hinzugefügt sind wird ein dialog angezeigt
    public void addPlant()
    {
        if (hits.Count > 0 && amountOfPlants < maxAmountOfPlants && visibility)
        {
            //GameObject placedPlant = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
            placedPlants[amountOfPlants] = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
            amountOfPlants++;
        }
        else
        {
            //wenn 3 oder mehr pflanzen gesetzt sind wird ein dialog angezeigt, der darauf hinweist, dass die maximale anzahl erreicht ist
            if (amountOfPlants >= 3) {
                maxPlantReachedDialouge.SetActive(true);
                useCursor = false;
            }
        }
    }
    //ok taste bei max. pflanzen-erreicht-dialog schließt diesen
    public void pressedOkWhenMaxPlants()
    {
        maxPlantReachedDialouge.SetActive(false);
        useCursor = true;
        
    }
    //sichtbarkeit der pflanze + cursor wird mit taste aktiviert/deaktiviert
    public void setVisibility()
    {
        visibility = !visibility;
        cursorChildObject.SetActive(visibility);
        movingPlantToPlace.SetActive(visibility);
        useCursor = visibility;
    }

    //die mülleimer-taste löscht alle gesetzten pflanzen
    public void deletePlacedPlants()
    {
        for(int i = 0; i < maxAmountOfPlants; i++)
        {
            placedPlants[i].SetActive(false);
        }
        amountOfPlants = 0;
    }
}
