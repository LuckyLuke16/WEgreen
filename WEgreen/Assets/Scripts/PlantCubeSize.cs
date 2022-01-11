using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCubeSize : MonoBehaviour
{
    private MeasurePlant MeasurePlant;
    private GameObject plant;

    private float xScale, yScale, zScale;

    void Start()
    {
        plant = transform.parent.parent.gameObject;
        MeasurePlant = plant.GetComponent<MeasurePlant>();
    }
    void Update()
    {
        //transform.GetComponent<MeshFilter>().mesh.bounds = MeasurePlant.bounds;
        if(plant.tag == "aloe")
        {
            calculateScalesAloe();
            transform.localScale = new Vector3(xScale, yScale, zScale);
            //transform.position = new Vector3(plant.transform.position.x, plant.transform.position.y + 0.08f, plant.transform.position.z);
            transform.position = new Vector3(plant.transform.position.x, plant.transform.position.y + (MeasurePlant.ySize/2), plant.transform.position.z);
        }
        else
        {
            calculateScales();
            transform.localScale = new Vector3(xScale, yScale, zScale);
            transform.position = new Vector3(plant.transform.position.x, plant.transform.position.y + (MeasurePlant.ySize/2), plant.transform.position.z);
        }
    }



    public void calculateScalesAloe()
    {
        xScale = MeasurePlant.xSize + (plant.transform.localScale.x * 100);
        yScale = MeasurePlant.ySize + ((plant.transform.localScale.y * 100));
        zScale = MeasurePlant.zSize + (plant.transform.localScale.z * 100);
    }

    public void calculateScales()
    {
        xScale = MeasurePlant.xSize + (plant.transform.localScale.x) - 1;
        yScale = MeasurePlant.ySize + (plant.transform.localScale.y) - 1;
        zScale = MeasurePlant.zSize + (plant.transform.localScale.z) - 1;
    }
}
