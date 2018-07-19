using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField]
    private Material lineMat;

    private bool creatingConnection;
    private Transform startNode;
    private Vector3 mousePos;
    private Port port;

    private ModeController mode;

    private void Start()
    {
        mode = FindObjectOfType<ModeController>();
    }

    private void Update()
    {
        mousePos = Input.mousePosition;

        if (!mode.edit)
        {
            creatingConnection = false;
        }
    }

    public void Connect(Port _port)
    {
        if (creatingConnection)
        {
            if (_port.transform.parent.GetComponentInChildren<LineRenderer>() != port.transform.parent.GetComponentInChildren<LineRenderer>()
                && _port.transform.parent.parent != port.transform.parent.parent && _port.type == port.type)
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
        startNode = _port.transform;
        creatingConnection = true;
    }

    private void OnPostRender()
    {
        if (creatingConnection && mode.edit)
        {
            Vector3 start = Camera.main.WorldToScreenPoint(startNode.position);
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