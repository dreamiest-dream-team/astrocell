using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedNode 
{
	public string type;
	public int id;
	public Vector3 position;
	public List<SerializedConnection> connections = new List<SerializedConnection>();
}
