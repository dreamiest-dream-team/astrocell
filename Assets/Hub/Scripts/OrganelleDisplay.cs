using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrganelleDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private GameObject background_selected;

	private bool selected = false;
	private bool mouseOver;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			selected = mouseOver;
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
				HubManager manager = GetComponentInParent<HubManager>();

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
		HubManager manager = GetComponentInParent<HubManager>();

		manager.Edit(gameObject);
	}
}
