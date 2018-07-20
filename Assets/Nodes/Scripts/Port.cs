using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [HideInInspector]
    public Port connection;
    [HideInInspector]
    public string type;

    private LineRenderer line;
    private ConnectionManager manager;
    private SpriteRenderer rend;

    private void Start()
    {
        line = transform.parent.GetComponentInChildren<LineRenderer>();
        manager = FindObjectOfType<ConnectionManager>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            manager.Connect(this);
        }
    }

    private void Update()
    {
        if (connection == null)
        {
            rend.color = new Color(0.4431027f, 0.4316038f, 0.5f, 0.2039216f);
        }
        else
        {
            rend.color = new Color(0.4431027f, 0.4316038f, 0.5f, 1);
        }

        if (line == null)
            return;

        if (connection == null)
        {
            line.enabled = false;
            return;
        }
        else
        {
            line.enabled = true;
        }

        Vector3 zOffset = new Vector3(0, 0, 0.1f);
        Vector3 initialOffset = new Vector3(1, 0, 0);

        if (connection.transform.position.x < transform.position.x) initialOffset *= -1;

        line.SetPosition(0, transform.position + zOffset);
        line.SetPosition(1, transform.position + initialOffset + zOffset);
        line.SetPosition(2, connection.transform.position - initialOffset + zOffset);
        line.SetPosition(3, connection.transform.position + zOffset);
    }
}
