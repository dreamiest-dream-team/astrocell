using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public string name;
    public Field[] inputs;
    public Field[] outputs;

    [Space]

    public GameObject inputFieldPrefab;
    public GameObject outputFieldPrefab;
    public Transform fieldParent;

    [Space]

    public float fieldSpacing;
    public float fieldOffset;

    private bool mouseOver;
    private bool dragging;
    private Vector3 dragOffset;

    private void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = name;

        for (int i = 0; i < inputs.Length; i++)
        {
            GameObject go = Instantiate(inputFieldPrefab);
            go.transform.parent = fieldParent;
            go.transform.localPosition = new Vector3(0, fieldSpacing * i - fieldOffset, 0);
            string t = " (" + inputs[i].type + ")";
            if (t == " ()") t = "";
            go.GetComponentInChildren<TextMeshPro>().text = inputs[i].name + t;
            go.GetComponentInChildren<Port>().type = inputs[i].type;
        }

        for (int i = 0; i < outputs.Length; i++)
        {
            GameObject go = Instantiate(outputFieldPrefab);
            go.transform.parent = fieldParent;
            go.transform.localPosition = new Vector3(0, fieldSpacing * i - fieldOffset, 0);
            string t = "(" + outputs[i].type + ") ";
            if (t == "() ") t = "";
            go.GetComponentInChildren<TextMeshPro>().text = t + outputs[i].name;
            go.GetComponentInChildren<Port>().type = outputs[i].type;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && mouseOver)
        {
            if (dragging)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = transform.position.z;

                transform.position = pos + dragOffset;
            }
            else
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;

                dragging = true;
                dragOffset = transform.position - pos;
            }
        }
        else
        {
            dragging = false;
        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
