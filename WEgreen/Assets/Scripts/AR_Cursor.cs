using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
/**
 * @brief Generates the plant models on the cursor through AR Raycasting from AR Foundation.
 */
public class AR_Cursor : MonoBehaviour
{
   
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public GameObject actions;
    public ARRaycastManager raycastManager;
    public ARPlaneManager aRPlaneManager;
    public GameObject movingPlantToPlace;
    private int amountOfPlants = 0;
    private int maxAmountOfPlants = 3;

    public GameObject maxPlantReachedDialouge;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool useCursor = true;
    private bool visibility = true;

    GameObject[] placedPlants;
    
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(true);

        movingPlantToPlace.SetActive(true);
        placedPlants = new GameObject[maxAmountOfPlants];
    }

    /**
     * bei touch auf bildschirm wird eine pflanze auf die Stelle des hits des raycasts mit einer generierten plane gesetzt
     * max. 3 pflanzen moeglich zu generieren (performance) 
    */
    void Update()
    {
      
        if (useCursor)
        {
            updateCursorAndPlant();
        }
    }
    /**
     * @brief Updates the position of the cursor, if a hit with a plane occured.
     * 
     * The cursor position is set to the middle of the screen.The cursor as well as the active
     * plant gameobject are rendered to the position of the last hit of the raycastmanager
     * with the detected plane in the middle of the screen.
     * 
    */
    private void updateCursorAndPlant()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            objectToPlace.SetActive(true);

            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            
            movingPlantToPlace.transform.position = hits[0].pose.position;
            movingPlantToPlace.transform.rotation = hits[0].pose.rotation;
        }
    }
    /**
     * @brief Scale of plant model is changed through adjustment of scale slider value.
     * @param scaleValue Value of slider for scaling
     */
    public void changeScale(float scaleValue)
    {
        movingPlantToPlace.transform.localScale = Vector3.one * scaleValue;

    }

    /**
     * @brief Selected plant models is set on current cursor position.
     * 
     * A maximum of three plants can be placed. The plant gameobjects are added to the placedPlants array.
     * If the amount of placed plants exceeds three a dialoug is set active, that notifies the user, that he has
     * to delete the plants in order to place new ones.
     */
    public void addPlant()
    {
        if (hits.Count > 0 && amountOfPlants < maxAmountOfPlants && visibility)
        {
            placedPlants[amountOfPlants] = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
            for(int i = 0; i < placedPlants[amountOfPlants].transform.childCount; i++)
            {
                if(placedPlants[amountOfPlants].transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    placedPlants[amountOfPlants].transform.GetChild(i).Find("MeasurePrefab").gameObject.SetActive(false);
                }
            }
            amountOfPlants++;
        }
        else
        {
            if (amountOfPlants >= 3) {
                maxPlantReachedDialouge.SetActive(true);
                useCursor = false;
            }
        }
    }
    /**
     * @brief Closes the dialouge when the maximum amount of placed plants is reached and the according ok- button is pressed.
     * 
     */
    public void pressedOkWhenMaxPlants()
    {
        maxPlantReachedDialouge.SetActive(false);
        
    }

    /**
     * @brief Switches the visibility of the cursor and the plant model when the visibility- button is pressed.
     */
    public void setVisibility()
    {
        visibility = !visibility;
        cursorChildObject.SetActive(visibility);
        movingPlantToPlace.SetActive(visibility);
        useCursor = visibility;
    }

    /**
     * @brief Deletes all placed plant models when the delete- button is pressed.
     * 
     * The gameobjects in the placedPlants array are set inactive and the cursor is set active.
     */
    public void deletePlacedPlants()
    {
        for(int i = 0; i < maxAmountOfPlants; i++)
        {
            placedPlants[i].SetActive(false);
        }
        amountOfPlants = 0;
        if (visibility)
        {
            useCursor = true;
        }
        
    }
}
