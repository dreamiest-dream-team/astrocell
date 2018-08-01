using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HubManager : MonoBehaviour
{
	[SerializeField]
	private GameObject organelleUIPrefab;

	[SerializeField]
	private float offset;
	[SerializeField]
	private float startY;

	private List<GameObject> organelleGOs = new List<GameObject>();

	private Organism organism;

	private void Start()
	{
		organism = new Organism();

		//Testing purposes
		organism.AddOrganelle(new Organelle());
		organism.AddOrganelle(new Organelle());

		Display();
	}

	private void Display()
	{
		for (int i = 0; i < organelleGOs.Count; i++)
		{
			Destroy(organelleGOs[i]);
		}

		organelleGOs.Clear();

		List<Organelle> organelles = organism.GetOrganelles();

		for (int i = 0; i < organelles.Count; i++)
		{
			GameObject go = Instantiate(organelleUIPrefab);

			go.transform.SetParent(transform);
			go.transform.localPosition = new Vector3(0, offset * i + startY, 0);

			go.GetComponentInChildren<TextMeshProUGUI>().text = "ID: " + organelles[i].id;
		}
	}
}
