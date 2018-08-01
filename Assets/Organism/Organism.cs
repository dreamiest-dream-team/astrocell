using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism
{
	private List<Organelle> organelles = new List<Organelle>();
	private int curId = 0;

	public void AddOrganelle(Organelle organelle)
	{
		organelle.id = curId;
		curId++;

		organelles.Add(organelle);
	}

	public void RemoveOrganelle(Organelle organelle)
	{
		organelles.Remove(organelle);
	}

	public List<Organelle> GetOrganelles()
	{
		return organelles;
	}
}
