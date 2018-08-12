using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HubManager : MonoBehaviour
{
	[SerializeField]
	private GameObject organelleUIPrefab;

	[SerializeField]
	private float offset;
	[SerializeField]
	private float startY;

	private float scrollOffset = 0f;

	private List<GameObject> organelleGOs = new List<GameObject>();

	private void Start()
	{
		Display();
	}

	private void Display()
	{
		for (int i = 0; i < organelleGOs.Count; i++)
		{
			Destroy(organelleGOs[i]);
		}

		organelleGOs.Clear();

		List<Organelle> organelles = Organism.GetOrganelles();

		for (int i = 0; i < organelles.Count; i++)
		{
			GameObject go = Instantiate(organelleUIPrefab);

			go.transform.SetParent(transform);
			go.transform.localPosition = new Vector3(0, offset * i + scrollOffset + startY, 0);

			organelleGOs.Add(go);

			TextMeshProUGUI[] texts = go.GetComponentsInChildren<TextMeshProUGUI>();

			texts[0].text = string.Format("${0:n0}", organelles[i].cost);
			texts[1].text = organelles[i].name;
		}
	}

	public void Scroll(float amount)
	{
		scrollOffset += amount;

		if (scrollOffset < 0)
			scrollOffset = 0;

		if (scrollOffset > organelleGOs.ToArray().Length * Mathf.Abs(amount))
			scrollOffset = organelleGOs.ToArray().Length * Mathf.Abs(amount);

		Display();
	}

	public void NewOrganelle()
	{
		if (organelleGOs.ToArray().Length + 1 > 19) return;

		Organelle o = new Organelle();
		o.nodes = new SerializedNode[] {};

		Organism.AddOrganelle(o);
		Display();
	}

	public void RemoveOrganelle(GameObject organelle)
	{
		int i = organelleGOs.FindIndex(o => o == organelle);
		Organism.RemoveOrganelle(i);

		Display();
	}

	public void Edit(GameObject organelle)
	{
		int i = organelleGOs.FindIndex(o => o == organelle);

		Organism.editing = Organism.GetOrganelles()[i];

		SceneManager.LoadScene(1); // 1 = editor
	}

	public void EditName(string name, GameObject organelle)
	{
		int i = organelleGOs.FindIndex(o => o == organelle);

		Organism.GetOrganelles()[i].name = name;
	}
}
