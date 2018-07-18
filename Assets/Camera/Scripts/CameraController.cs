using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

    public Transform nodes;

    private Camera cam;
    
    private bool dragging;
    private Vector3 dragOffset;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        //Handle zooming
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - zoomSpeed * Input.GetAxis("Mouse ScrollWheel"), minZoom, maxZoom);

        //Handle panning
        if (dragging)
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = nodes.position.z;

            nodes.position = pos + dragOffset;
        }

        if (Input.GetMouseButtonDown(2))
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            dragging = true;
            dragOffset = nodes.position - pos;
        }

        if (Input.GetMouseButtonUp(2))
        {
            dragging = false;
        }
    }
}
