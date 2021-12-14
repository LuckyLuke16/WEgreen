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

    public bool useCursor = true;
    
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);

        objectToPlace.SetActive(true);
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
        


    }
    /*
     * updated die position und rotation des cursors wenn hit mit plane aufgetreten ist
     * Modell-Pflanze ist immer auf cursor
    */
    void updateCursorAndPlant()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            
            movingPlantToPlace.transform.position = hits[0].pose.position;
            movingPlantToPlace.transform.rotation = hits[0].pose.rotation;
        }
    }

    //skalierfunktion, die beim benutzen des sliders verwendet wird
    public void changeScale(float scaleValue)
    {
        movingPlantToPlace.transform.localScale = Vector3.one * scaleValue;

    }
}
