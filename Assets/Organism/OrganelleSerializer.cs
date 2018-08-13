using System;
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
		Organism.editing.cost = FindObjectOfType<OrganDisplay>().cost;

		Node[] nodes = instantiatedNodesParent.GetComponentsInChildren<Node>();

		Organism.editing.nodes = new SerializedNode[nodes.Length];

		for (int i = 0; i < nodes.Length; i++)
		{
			SerializedNode n = new SerializedNode();
			n.type = nodes[i].UID;
			n.id = i;
			n.position = nodes[i].transform.position;
			Organism.editing.nodes[i] = n;

			LineRenderer[] rends = nodes[i].GetComponentsInChildren<LineRenderer>();

			for (int j = 0; j < rends.Length; j++)
			{
                if (!rends[j].enabled)
                    break;

				SerializedConnection connection = new SerializedConnection();
				connection.endID = Array.IndexOf(nodes, rends[j].transform.parent.GetComponentInChildren<Port>().connection.GetComponentInParent<Node>());
				connection.startPortName = rends[j].transform.parent.GetComponentInChildren<Port>().name;
				connection.endPortName = rends[j].transform.parent.GetComponentInChildren<Port>().connection.name;
				n.connections.Add(connection);
			}
		}
	}

	public void Deserialize()
	{
		Node[] nodes = nodesParent.GetComponentsInChildren<Node>();

		for (int i = 0; i < Organism.editing.nodes.Length; i++)
		{
			for (int j = 0; j < nodes.Length; j++)
			{
				if (nodes[j].UID == Organism.editing.nodes[i].type)
				{
					GameObject go = Instantiate(nodes[j].gameObject, Organism.editing.nodes[i].position, Quaternion.identity);
					go.transform.parent = instantiatedNodesParent;
				}
			}
		}

        Node[] instantiatedNodes = instantiatedNodesParent.GetComponentsInChildren<Node>();

		for (int i = 0; i < Organism.editing.nodes.Length; i++) {
            for (int j = 0; j < Organism.editing.nodes[i].connections.Count; j++)
            {
                SerializedConnection connection = Organism.editing.nodes[i].connections[j];

                Node inputNode = null;

                for (int k = 0; k < Organism.editing.nodes.Length; k++)
                {
                    if (k == connection.endID)
                    {
                        inputNode = instantiatedNodes[k];
                    }
                }

                Port output = null;
                Port input = null;

                for (int l = 0; l < instantiatedNodes[i].outputs.Length; l++)
                {
                    if (instantiatedNodes[i].outputs[l].name == connection.startPortName)
                    {
                        output = instantiatedNodes[i].GetComponentsInChildren<Port>()[l + instantiatedNodes[i].inputs.Length];
                    }
                }

                for (int l = 0; l < inputNode.inputs.Length; l++)
                {
                    if (inputNode.inputs[l].name == connection.endPortName)
                    {
                        input = inputNode.GetComponentsInChildren<Port>()[l];
                    }
                }

                Debug.Log(output);
                Debug.Log(input);

                if (output != null && input != null)
                {
                    output.connection = input;
                    input.connection = output;
                }
            }
        }
    }

	public void Back()
	{
		Serialize();
		SceneManager.LoadScene(1);
	}
}
