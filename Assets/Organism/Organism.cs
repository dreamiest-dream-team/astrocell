using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Organism
{
	private static List<Organelle> organelles = new List<Organelle>();
	private static int curId = 0;

	public static Organelle editing;

	public static void AddOrganelle(Organelle organelle)
	{
		organelle.id = curId;
		curId++;

		organelles.Add(organelle);
	}

	public static void RemoveOrganelle(int index)
	{
		organelles.RemoveAt(index);
	}

	public static List<Organelle> GetOrganelles()
	{
		return organelles;
	}
}
