using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class NodeAction : MonoBehaviour
{
	[SerializeField]
	private string type;
	[SerializeField]
	private TextMeshPro progressText;

	private List<Item> items;
	bool inProgress = false;

	private ModeController mode;

	private void Start()
	{
		mode = FindObjectOfType<ModeController>();
		items = new List<Item>();
		progressText.text = "Waiting...";
	}

	private void Update()
	{
		if (!mode.edit)
		{
			CheckReady();
		}
	}

	public void RecieveItem(Item item)
	{
		items.Add(item);
	}

	private void CheckReady()
	{
		switch (type)
		{
			case "inhaler":
				if (!inProgress && HasItem("") >= 10) 
				{
					RemoveItems("", 10);
					StartCoroutine(StartExecutionWait());
				}
				break;
			case "import_energy":
				if (!inProgress)
				{
					StartCoroutine(StartExecutionWait());
				}
				break;
		}
	}

	private int HasItem(string type)
	{
		int n = 0;

		foreach (Item item in items)
		{
			if (item.type == type) n++;
		}

		return n;
	}

	private void RemoveItems(string type, int num)
	{
		int n = 0;

		Item[] remove = new Item[num];

		foreach (Item item in items)
		{
			if (item.type == type && n < num)
			{
				remove[n] = item;
				n++;
			}
		}

		foreach (Item i in remove)
		{
			items.Remove(i);
		}
	}

	IEnumerator StartExecutionWait()
	{
		inProgress = true;

		float elapsed = 0;
		float maxElapsed = 0;

		switch (type)
		{
			case "inhaler":
				maxElapsed = 5;
				break;
			case "import_energy":
				maxElapsed = 5;
				break;
		}

		while (elapsed < maxElapsed)
		{
			if (mode.edit)
				elapsed = maxElapsed;

			elapsed += Time.deltaTime;
			progressText.text = Mathf.Round((elapsed / maxElapsed) * 100) + "%";
			yield return null;
		}

		progressText.text = "Waiting...";

		if (!mode.edit)
			ExecuteTask();

		yield return new WaitForSeconds(1);

		inProgress = false;
	}

	private void ExecuteTask()
	{
		List<Item> sending = new List<Item>();

		switch (type)
		{
			case "inhaler":
				string[] atmosphere = { "oxygen", "oxygen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "carbon dioxide", "water", "argon" };
				for (int i = 0; i < 4; i++)
				{
					Item item = new Item();
					item.type = "GAS";
					item.name = atmosphere[Random.Range(0, atmosphere.Length)];
					sending.Add(item);
				}
				break;
			case "import_energy":
				for (int i = 0; i < 3; i++)
				{
					Item item = new Item();
					item.type = "";
					item.name = "energy";
					sending.Add(item);
				}
				break;
		}

		Port[] ports = GetComponentsInChildren<Port>();
		foreach (Port p in ports)
		{
			if (p.transform.parent.GetComponentInChildren<LineRenderer>() != null)
			{
				StartCoroutine(Send(sending.ToArray(), p));
			}
		}
	}

	IEnumerator Send(Item[] sending, Port port)
	{
		if (port != null && port.connection != null && !mode.edit)
		{
			StartCoroutine(SendPulseAnimation(port));

			yield return new WaitForSeconds(2);

			for (int i = 0; i < sending.Length; i++)
			{
				Port p = port.connection;
					
				if (p != null) {
					p.GetComponentInParent<NodeAction>().RecieveItem(sending[i]);
				} 
			}
		}
	}

	IEnumerator SendPulseAnimation(Port port)
	{
		LineRenderer lr = port.transform.parent.GetComponentInChildren<LineRenderer>();

		Vector3[] positions = { lr.GetPosition(0), lr.GetPosition(1), lr.GetPosition(2), lr.GetPosition(3) };

		float lengthOfConnection = 0;

		lengthOfConnection += Vector3.Distance(positions[0], positions[1]);
		lengthOfConnection += Vector3.Distance(positions[1], positions[2]);
		lengthOfConnection += Vector3.Distance(positions[2], positions[3]);

		float ups = lengthOfConnection / 2; //units per second
		
		Transform pulse = lr.transform.GetChild(0);

		pulse.position = positions[0];
		pulse.gameObject.SetActive(true);

		Port origConnection = port.connection;

		int point = 1;
		float elapsed = 0;

		while (point < 4)
		{
			Vector3 _offset = positions[point] - lr.GetPosition(point);

			pulse.position = Vector3.MoveTowards(pulse.position, positions[point] - _offset, ups * Time.deltaTime);
			if (pulse.position == positions[point] - _offset) point++;

			if (origConnection != port.connection)
			{
				point = 4;
			}

			for (int i = 0; i < 4; i++)
			{
				if (positions[i] - _offset != lr.GetPosition(i))
				{
					point = 4;
				}
			}

			if (mode.edit)
				point = 4;

			elapsed += Time.deltaTime;
			yield return null;
		}

		pulse.gameObject.SetActive(false);
	}
}

[System.Serializable]
public class Item
{
	public string type;
	public string name; 
}