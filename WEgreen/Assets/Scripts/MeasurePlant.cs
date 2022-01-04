using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeasurePlant : MonoBehaviour
{
    private TextMeshPro xText, yText, zText;
    [SerializeField] private Vector3 offsetMeasurement;
    private Bounds bounds;
    //private MeshRenderer renderer;
    //private Mesh mesh;
    public float xSize, ySize, zSize;
    private Vector3 xLabel, yLabel, zLabel;
    private GameObject measurePrefab;
    private bool scaling = false;
    private Vector3 plantScale;
    private MeshFilter[]  meshFilters;
    private List<MeshFilter> meshFilter;
    void Start()
    {
        measurePrefab = transform.Find("MeasurePrefab").gameObject;
        xText = measurePrefab.transform.Find("xText").GetComponent<TextMeshPro>();
        yText = measurePrefab.transform.Find("yText").GetComponent<TextMeshPro>();
        zText = measurePrefab.transform.Find("zText").GetComponent<TextMeshPro>();
        plantScale = transform.localScale;
        //combine();
        meshFilters = GetComponentsInChildren<MeshFilter>();
        meshFilter = new List<MeshFilter>();
        //Debug.Log(meshFilters.Length);
        for(int i = 1; i < meshFilters.Length; i++)
        {
            meshFilter.Add(meshFilters[i]);
            //Debug.Log(meshFilters[i].gameObject.name);
        }
        meshFilter.Add(meshFilters[meshFilters.Length-1]);
    }

    void Update()
    {
        //if(isScaling())
            combine();
        measure();
    }

    public void combine()
    {
        //MeshFilter[]  meshFilters = GetComponentsInChildren<MeshFilter>();
        //List<MeshFilter> meshFilter = new List<MeshFilter>();
        
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int np = 0;
        while(np < meshFilter.Count)
        {
            combine[np].mesh = meshFilter[np].sharedMesh;
            combine[np].transform = meshFilter[np].transform.localToWorldMatrix;
            //Destroy(meshFilters[np]);
            np++;
        }
        //mesh = transform.GetComponent<MeshFilter>().mesh;
        //mesh = new Mesh();
        //transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.GetComponent<MeshRenderer>().enabled = false;
        bounds = transform.GetComponent<MeshFilter>().mesh.bounds;
        //Debug.Log(bounds);
        
        setLabels();
    }

    public void setLabels()
    {
        xSize = bounds.size.x;
        ySize = bounds.size.y;
        zSize = bounds.size.z;

        xLabel = new Vector3(xSize, 0, 0);
        yLabel = new Vector3(0, ySize, 0);
        zLabel = new Vector3(0, 0, zSize);
    }

    public void measure()
    {
        xText.transform.position = transform.position + -zLabel + offsetMeasurement;
        //xText.transform.LookAt(Camera.main.transform);
        yText.transform.position = transform.position + yLabel + offsetMeasurement;
        //yText.transform.LookAt(Camera.main.transform);
        zText.transform.position = transform.position + xLabel + offsetMeasurement;
        //zText.transform.LookAt(Camera.main.transform);

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

    public bool isScaling()
    {
        if(plantScale != transform.localScale)
        {
            scaling = true;
            plantScale = transform.localScale;
        }
        else
        {
            scaling = false;
        }

        return scaling;
    }
}
