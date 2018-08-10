using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrganelleSerializer : MonoBehaviour
{
	[SerializeField]
	private Transform instantiatedNodesParent;
	[SerializeField]
	private Transform nodesParent;

	private void Start()
	{
		Deserialize();
	}

	public void Serialize()
	{
		Node[] nodes = instantiatedNodesParent.GetComponentsInChildren<Node>();

		Organism.editing.nodes = new SerializedNode[nodes.Length];

		for (int i = 0; i < nodes.Length; i++)
		{
			SerializedNode n = new SerializedNode();
			n.id = nodes[i].UID;
			n.position = nodes[i].transform.position;
			Organism.editing.nodes[i] = n;
		}
	}

	public void Deserialize()
	{
		Node[] nodes = nodesParent.GetComponentsInChildren<Node>();

		for (int i = 0; i < Organism.editing.nodes.Length; i++)
		{
			for (int j = 0; j < nodes.Length; j++)
			{
				if (nodes[j].UID == Organism.editing.nodes[i].id)
				{
					GameObject go = Instantiate(nodes[j].gameObject, Organism.editing.nodes[i].position, Quaternion.identity);
					go.transform.parent = instantiatedNodesParent;
				}
			}
		}
	}

	public void Back()
	{
		Serialize();
		SceneManager.LoadScene(0); //hub scene
	}
}
