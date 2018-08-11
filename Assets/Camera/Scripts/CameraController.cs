using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float parallax;
	public float zoomSpeed;
	public float minZoom;
	public float maxZoom;

	public RectTransform background;
	public Transform nodes;

	private Camera cam;

	public static bool disableMenu = false;
	public Vector3 posCompare;

	private bool dragging;
	private Vector3 dragOffsetBackground;
	private Vector3 dragOffsetNodes;

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

			nodes.position = pos + dragOffsetNodes;
			background.position = (pos / parallax) * (cam.orthographicSize / parallax) + dragOffsetBackground;
		}

		if (Input.GetMouseButtonDown(1))
		{
			Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0;

			dragging = true;
			dragOffsetNodes = nodes.position - pos;
			dragOffsetBackground = background.position - (pos / parallax) * (cam.orthographicSize / parallax);

			posCompare = cam.ScreenToWorldPoint(Input.mousePosition);
			disableMenu = true;
		}

		if (Input.GetMouseButtonUp(1))
		{
			if (posCompare == cam.ScreenToWorldPoint(Input.mousePosition)) disableMenu = false;

			dragging = false;
		}
	}
}
