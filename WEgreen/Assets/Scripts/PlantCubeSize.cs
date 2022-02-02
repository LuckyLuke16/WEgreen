using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
* Used to create the visible cube element of the AR measuring function. The cube already exists within
* the MeasurePrefab of each plant model. The final mesh created in MeasurePlant is used to set the bounds
* of the cube and display it to the user. Due to variations in scaling, different plant models use
* different methods within the class.
*/
public class PlantCubeSize : MonoBehaviour
{
    /**
    * Reference to other script used for measuring
    */
    private MeasurePlant MeasurePlant;
    private GameObject plant;

    private float xScale, yScale, zScale;

    /**
    * @brief Prepares the instance variables on the first frame
    */
    void Start()
    {
        plant = transform.parent.parent.gameObject;
        MeasurePlant = plant.GetComponent<MeasurePlant>();
    }
    /**
    * @brief Calculates the scales for the current plant model each frame
    *
    * Scales and  positions are set to be in-line with the AR cursor.
    */
    void Update()
    {
        if(plant.tag == "aloe")
        {
            calculateScalesAloe();
            transform.localScale = new Vector3(xScale, yScale, zScale);
            transform.position = new Vector3(plant.transform.position.x, plant.transform.position.y + (MeasurePlant.ySize/2), plant.transform.position.z);
        }
        else
        {
            calculateScales();
            transform.localScale = new Vector3(xScale, yScale, zScale);
            transform.position = new Vector3(plant.transform.position.x, plant.transform.position.y + (MeasurePlant.ySize/2), plant.transform.position.z);
        }
    }


    //different scaling used by aloe models
    /**
    * Calculates the scale values of the current aloe model
    * This requires its own method, as the aloe methods differ in scaling compared to the other plant models.
    */
    public void calculateScalesAloe()
    {
        xScale = MeasurePlant.xSize + (plant.transform.localScale.x * 100);
        yScale = MeasurePlant.ySize + ((plant.transform.localScale.y * 100));
        zScale = MeasurePlant.zSize + (plant.transform.localScale.z * 100);
    }
    /**
    * @brief Calculates the scale values of the current plant model (excluding aloe models)
    */
    public void calculateScales()
    {
        xScale = MeasurePlant.xSize + (plant.transform.localScale.x) - 1;
        yScale = MeasurePlant.ySize + (plant.transform.localScale.y) - 1;
        zScale = MeasurePlant.zSize + (plant.transform.localScale.z) - 1;
    }
}
