using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/**
* Used solely for the AR measuring function and serves to combine all the meshes within a given plant model.
* Each frame, it combines these meshes and uses their boundaries to create visble text objects for the user to see in AR.
*/
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeasurePlant : MonoBehaviour
{
    private TextMeshPro xText, yText, zText;
    [SerializeField] private Vector3 offsetMeasurement;
    /**
    * Boundaries of combined mesh
    */
    private Bounds bounds;
    public float xSize, ySize, zSize;
    private Vector3 xLabel, yLabel, zLabel;
    /**
    * Prefab that executes code
    */
    private GameObject measurePrefab;
    /**
    * Scale of plant model
    */
    private Vector3 plantScale;
    private MeshFilter[]  meshFilters;
    private List<MeshFilter> meshFilter;
    /**
    * Required components and gameobjects needed for methods are found and set.
    * Additionally, an array of all the mesh filters in the plant model is created via a loop.
    */
    void Start()
    {
        //required components and gameobjects are found and set
        measurePrefab = transform.Find("MeasurePrefab").gameObject;
        xText = measurePrefab.transform.Find("xText").GetComponent<TextMeshPro>();
        yText = measurePrefab.transform.Find("yText").GetComponent<TextMeshPro>();
        zText = measurePrefab.transform.Find("zText").GetComponent<TextMeshPro>();
        plantScale = transform.localScale;
        meshFilters = GetComponentsInChildren<MeshFilter>();
        meshFilter = new List<MeshFilter>();

        for(int i = 1; i < meshFilters.Length; i++)
        {
            meshFilter.Add(meshFilters[i]);
        }
        meshFilter.Add(meshFilters[meshFilters.Length-1]);
    }
    /**
    * Runs the combine and measure methods each frame.
    */
    void Update()
    {
        combine();
        measure();
    }
    /**
    * @brief Individual meshes of the plant model are combined to create a new unified mesh.
    *
    * An array of CombineInstance objects is created using the length of all meshFilters within the plant model.
    * This array is then filled by looping through all the mesh filters and retrieving the individual meshes and transform values.
    * The mesh filter of the complete model is then created with the CombineMeshes() method and teh mesh renderer is disabled so only the compiled mesh is visible.
    * The bounds variable is then set to the boundaries of this final mesh and the bounds/labels are assigned with setLabels().
    */
    public void combine()
    { 
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int np = 0;
        while(np < meshFilter.Count)
        {
            combine[np].mesh = meshFilter[np].sharedMesh;
            combine[np].transform = meshFilter[np].transform.localToWorldMatrix;
            np++;
        }

        transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.GetComponent<MeshRenderer>().enabled = false;
        bounds = transform.GetComponent<MeshFilter>().mesh.bounds;
        
        setLabels();
    }
    /**
    * @brief Text labels of x, y and z values are set using the bounds of the combined mesh.
    *
    * The sizes of each axis are applied to their respective variables. These variables are then used to create the labels as new Vector3 objects.
    */
    public void setLabels()
    {
        xSize = bounds.size.x;
        ySize = bounds.size.y;
        zSize = bounds.size.z;

        xLabel = new Vector3(xSize, 0, 0);
        yLabel = new Vector3(0, ySize, 0);
        zLabel = new Vector3(0, 0, zSize);
    }
    /**
    * @brief Positions of text are set and constantly updated using the combined mesh bounds
    *
    * The positions of the text objects are determined using the position of the plant, the label objects and a predetermined offset variable.
    * Due to variations in the scaling of the different plant models, the aloe models are scaled differently than the rest. Thus, there is a tag check for aloe to apply specific scaling to the text.
    * The text is then set using the respective size variables and set to display 2 decimal places.
    */
    public void measure()
    {
        xText.transform.position = transform.position + -zLabel + offsetMeasurement;
        yText.transform.position = transform.position + yLabel + offsetMeasurement;
        zText.transform.position = transform.position + xLabel + offsetMeasurement;

        if(gameObject.tag == "aloe")
        {
            xText.rectTransform.localScale = new Vector3(28, 28, 28);
            yText.rectTransform.localScale = new Vector3(28, 28, 28);
            zText.rectTransform.localScale = new Vector3(28, 28, 28);
        }
        else
        {
            xText.rectTransform.localScale = new Vector3(3, 3, 3);
            yText.rectTransform.localScale = new Vector3(3, 3, 3);
            zText.rectTransform.localScale = new Vector3(3, 3, 3);
        }
        

        xText.text = $"x: {xSize.ToString("F2")} m";
        yText.text = $"y: {ySize.ToString("F2")} m";
        zText.text = $"z: {zSize.ToString("F2")} m";
    }
}
