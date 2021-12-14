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
    public int amountOfPlants = 0;
    public GameObject movingPlantToPlace;

    //Dialog wenn maximales pflanzenlimit erreicht ist
    public GameObject maxPlantReachedDialouge;

    //list mit hits des raycasts mit einer plane
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public bool useCursor = true;
    
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);

        movingPlantToPlace.SetActive(true);
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

    public void addPlant()
    {
        if (hits.Count > 0 && amountOfPlants < 3)
        {
            GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
            amountOfPlants++;
        }
        else
        {
            //wenn 3 oder mehr pflanzen gesetzt sind wird ein dialog angezeigt, der darauf hinweist, dass die maximale anzahl erreicht ist
            if (amountOfPlants >= 3) {
                maxPlantReachedDialouge.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
