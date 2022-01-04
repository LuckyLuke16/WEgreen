 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;


public class MeasurementController : MonoBehaviour
{
    [SerializeField] private GameObject measurementPointPrefab;
    [SerializeField] private Vector3 offsetMeasurement;
    [SerializeField] private TextMeshPro distanceText;
    [SerializeField] private ARCameraManager arCameraManager;
    //[SerializeField] private MeshRenderer renderer;
    private LineRenderer measureLine;
    private ARRaycastManager arRaycastManager;
    //private GameObject p1, p2, p3, p4, p5, p6, p7, p8;
    private GameObject startPoint, endPoint;
    //private Vector2 touchPosition = default;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

        //startPoint = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);
        //endPoint = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity);

        measureLine = GetComponent<LineRenderer>();

        //startPoint.SetActive(false);
        //endPoint.SetActive(false);
    }

    void Update()
    {
        /*
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;

                if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    startPoint.SetActive(true);

                    Pose hitPose = hits[0].pose;
                    startPoint.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }

            if(touch.phase == TouchPhase.Moved)
            {
                touchPosition = touch.position;

                if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    measureLine.gameObject.SetActive(true);
                    endPoint.SetActive(true);

                    Pose hitPose = hits[0].pose;
                    endPoint.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }
        */
    /*
        if(startPoint.activeSelf && endPoint.activeSelf)
        {
            distanceText.transform.position = endPoint.transform.position + offsetMeasurement;
            distanceText.transform.rotation = endPoint.transform.rotation;
            measureLine.SetPosition(0, startPoint.transform.position);
            measureLine.SetPosition(1, endPoint.transform.position);

            distanceText.text = $"Breite (x): {(Vector3.Distance(startPoint.transform.position, endPoint.transform.position)).ToString("F2")} m";
            distanceText.text = $"Höhe (y): {(Vector3.Distance(startPoint.transform.position, endPoint.transform.position)).ToString("F2")} m";
            distanceText.text = $"Tiefe (z): {(Vector3.Distance(startPoint.transform.position, endPoint.transform.position)).ToString("F2")} m";
        }
        */
    }
}
