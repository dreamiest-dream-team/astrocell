using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OrganelleDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private GameObject background_selected;

	private bool selected = false;
	private bool mouseOver;

	private HubManager manager;
	private TMP_InputField field;

	private void Start()
	{
		manager = GetComponentInParent<HubManager>();
		field = GetComponentInChildren<TMP_InputField>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)))
		{
			selected = mouseOver;
		}

		if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)))
		{
			if (mouseOver)
				selected = !selected;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			selected = false;
		}

		background_selected.SetActive(selected);

		if (selected)
		{
			if (Input.GetKeyDown(KeyCode.Delete))
			{
				manager.RemoveOrganelle(gameObject);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseOver = false;
	}

	public void Edit()
	{
		manager.Edit(gameObject);
	}

	public void EditName()
	{
		string name = field.text.Trim();

		if (name.Length == 0)
		{
			name = "MyOrganelle";
		}

		field.text = name;

		manager.EditName(name, gameObject);
	}
}
