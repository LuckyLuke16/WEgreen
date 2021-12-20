using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCubeSize : MonoBehaviour
{
    [SerializeField] private MeasurePlant MeasurePlant;
    private GameObject plant;
    private float xScale, yScale, zScale;

    void Start()
    {
        plant = transform.parent.gameObject;
        //transform.localScale = transform.localScale + plant.transform.localScale;
    }
    void Update()
    {
        //transform.GetComponent<MeshFilter>().mesh.bounds = MeasurePlant.bounds;
        calculateScales();
        transform.localScale = new Vector3(xScale, yScale, zScale);
    }

    public void calculateScales()
    {
        xScale = MeasurePlant.xSize + (plant.transform.localScale.x * 100);
        yScale = MeasurePlant.ySize + (plant.transform.localScale.y * 100);
        zScale = MeasurePlant.zSize + (plant.transform.localScale.z * 100);
    }
}
