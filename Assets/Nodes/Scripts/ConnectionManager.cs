using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
	[SerializeField]
	private Material lineMat;

	private bool creatingConnection;
	private Port startPort;

	private ModeController mode;

	private void Start()
	{
		mode = FindObjectOfType<ModeController>();
	}

	private void Update()
	{
		if (creatingConnection && (!mode.edit || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1)))
		{
			creatingConnection = false;
		}
	}

	public void Connect(Port port)
	{
		if (creatingConnection)
		{
			if (port.transform.parent.GetComponentInChildren<LineRenderer>() != startPort.transform.parent.GetComponentInChildren<LineRenderer>()
				&& port.transform.parent.parent != startPort.transform.parent.parent && port.type == startPort.type)
			{
				if (port.connection != null) port.connection.connection = null;
				if (startPort.connection != null) startPort.connection.connection = null;

				port.connection = startPort;
				startPort.connection = port;
			}

			creatingConnection = false;
			return;
		}

		startPort = port;
		creatingConnection = true;
	}

	private void OnPostRender()
	{
		if (creatingConnection && mode.edit)
		{
			Vector3 start = Camera.main.WorldToScreenPoint(startPort.transform.position);
			lineMat.SetPass(0);
			GL.LoadOrtho();
			GL.Begin(GL.LINES);
			GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
			GL.Vertex(new Vector3(start.x / Screen.width, start.y / Screen.height, 0));
			GL.Vertex(new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0));
			GL.End();
		}
	}
}