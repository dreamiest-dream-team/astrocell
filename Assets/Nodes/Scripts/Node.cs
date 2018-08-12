using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
	public string UID;

	[Space]
	
	[SerializeField]
	private string name;

	public Field[] inputs;
	public Field[] outputs;

	[Space]

	[SerializeField]
	private GameObject inputFieldPrefab;
	[SerializeField]
	private GameObject outputFieldPrefab;
	[SerializeField]
	private Transform fieldParent;

	[Space]

	[SerializeField]
	private float fieldSpacing;
	[SerializeField]
	private float fieldOffset;

	private bool mouseOver;
	private bool dragging;
	private Vector3 dragOffset;
	
	private ModeController mode;

	private void Awake()
	{
		mode = FindObjectOfType<ModeController>();
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
			go.GetComponentInChildren<Port>().name = inputs[i].name;
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
			go.GetComponentInChildren<Port>().name = outputs[i].name;
		}
	}

	private void Update()
	{
		if (dragging)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = transform.position.z;
			transform.position = pos + dragOffset;
		}

		if (Input.GetMouseButtonDown(0) && mouseOver)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0;
			dragging = true;
			dragOffset = transform.position - pos;
		}
		
		if (Input.GetMouseButtonUp(0) || !mode.edit)
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
