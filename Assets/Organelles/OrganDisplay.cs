using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrganDisplay : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI costText;

	private float cost;

	private void Start()
	{
		cost = 1000;
		UpdateCostText();
	}

	public void UpdateCost(float costModifier)
	{
		cost += costModifier;
		UpdateCostText();
	}

	private void UpdateCostText()
	{
		string str = string.Format("{0:n0}", cost);

		costText.text = "Organelle Cost: $" + str;
	}
}
