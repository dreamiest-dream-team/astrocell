using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
	public ModeButton editButton;
	public ModeButton testButton;

	public Color selected;
	public Color unselected;

	public bool edit = true;

	private void Start()
	{
		UpdateButtons();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (editButton.mouseOver) edit = true;
			if (testButton.mouseOver) edit = false;

			UpdateButtons();
		}
	}

	private void UpdateButtons()
	{
		if (edit)
		{
			editButton.GetComponentInChildren<Image>().color = selected;
			testButton.GetComponentInChildren<Image>().color = unselected;
		}
		else
		{
			editButton.GetComponentInChildren<Image>().color = unselected;
			testButton.GetComponentInChildren<Image>().color = selected;
		}
	}
}
