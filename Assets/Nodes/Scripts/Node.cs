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

    private void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = name;

        for (int i = 0; i < inputs.Length; i++)
        {
            GameObject go = Instantiate(inputFieldPrefab);
            go.transform.parent = fieldParent;
            go.transform.localPosition = new Vector3(0, fieldSpacing * i, 0);
            string t = " (" + inputs[i].type + ")";
            if (t == " ()") t = "";
            go.GetComponentInChildren<TextMeshPro>().text = inputs[i].name + t;
            go.GetComponentInChildren<Port>().type = inputs[i].type;
        }

        for (int i = 0; i < outputs.Length; i++)
        {
            GameObject go = Instantiate(outputFieldPrefab);
            go.transform.parent = fieldParent;
            go.transform.localPosition = new Vector3(0, fieldSpacing * i, 0);
            go.GetComponentInChildren<TextMeshPro>().text = "(" + outputs[i].type + ") " + outputs[i].name;
            go.GetComponentInChildren<Port>().type = outputs[i].type;
        }
    }
}
