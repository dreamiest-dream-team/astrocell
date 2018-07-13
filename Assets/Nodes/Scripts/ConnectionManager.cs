using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public Material lineMat;

    private bool creatingConnection;
    private Vector3 start;
    private Vector3 mousePos;
    private Port port;

    private void Update()
    {
        mousePos = Input.mousePosition;
    }

    public void Connect(Port _port)
    {
        if (creatingConnection)
        {
            if (_port.transform.parent.GetComponentInChildren<LineRenderer>() != port.transform.parent.GetComponentInChildren<LineRenderer>()
                && _port.transform.parent.parent != port.transform.parent.parent)
            {
                if (_port.connection != null) _port.connection.connection = null;
                if (port.connection != null) port.connection.connection = null;

                _port.connection = port;
                port.connection = _port;
            }

            creatingConnection = false;
            return;
        }

        port = _port;
        start = Camera.main.WorldToScreenPoint(_port.transform.position);
        creatingConnection = true;
    }

    private void OnPostRender()
    {
        if (creatingConnection)
        {
            lineMat.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
            GL.Vertex(new Vector3(start.x / Screen.width, start.y / Screen.height, 0));
            GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
            GL.End();
        }
    }
}