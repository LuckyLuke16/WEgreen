using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeasurePlant : MonoBehaviour
{
    [SerializeField] private TextMeshPro xText, yText, zText;
    [SerializeField] private Vector3 offsetMeasurement;
    private Bounds bounds;
    //private MeshRenderer renderer;
    private Mesh mesh;
    private float xSize, ySize, zSize;
    private Vector3 xLabel, yLabel, zLabel;
    void Start()
    {
        combine();
    }

    void Update()
    {
        combine();
        measure();
    }

    public void combine()
    {
        MeshFilter[]  meshFilters = GetComponentsInChildren<MeshFilter>();
        List<MeshFilter> meshFilter = new List<MeshFilter>();
        
        
        for(int i = 1; i < meshFilters.Length; i++)
        {
            meshFilter.Add(meshFilters[i]);
        }
        meshFilter.Add(meshFilters[meshFilters.Length-1]);
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int np = 0;
        while(np < meshFilter.Count)
        {
            combine[np].mesh = meshFilter[np].sharedMesh;
            combine[np].transform = meshFilter[np].transform.localToWorldMatrix;
            np++;
        }
        mesh = transform.GetComponent<MeshFilter>().mesh;
        mesh = new Mesh();
        mesh.CombineMeshes(combine);
        bounds = mesh.bounds;
        
        setLabels();
    }

    public void setLabels()
    {
        xSize = bounds.size.x;
        //Debug.Log("xSize: " + xSize);
        ySize = bounds.size.y;
        //Debug.Log("ySize: " + ySize);
        zSize = bounds.size.z;
        //Debug.Log("zSize: " + zSize);

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

        xText.text = $"Breite (x): {xSize.ToString("F2")} m";
        yText.text = $"Höhe (y): {ySize.ToString("F2")} m";
        zText.text = $"Tiefe (z): {zSize.ToString("F2")} m";
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
