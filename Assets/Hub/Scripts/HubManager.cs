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

			organelleGOs.Add(go);

			go.GetComponentInChildren<TextMeshProUGUI>().text = "ID: " + organelles[i].id;
		}
	}

	public void NewOrganelle()
	{
		organism.AddOrganelle(new Organelle());
		Display();
	}

	public void RemoveOrganelle(GameObject organelle)
	{
		int i = organelleGOs.FindIndex(o => o == organelle);
		organism.RemoveOrganelle(i);

		Display();
	}
}
