using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private GameObject toolTipHost;
	[SerializeField]
	private bool description;

	private bool mouseOver = false;

	void Update ()
	{
		if (mouseOver)
			toolTipHost.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);

		toolTipHost.SetActive(mouseOver);
	}
}
