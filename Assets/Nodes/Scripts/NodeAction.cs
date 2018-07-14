using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class NodeAction : MonoBehaviour
{
    public List<Item> items;
    public string type;
    public TextMeshPro progressText;

    private bool inProgress = false;

    private void Start()
    {
        items = new List<Item>();
        progressText.text = "Waiting...";
    }

    private void Update()
    {
        CheckReady();
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
                if (!inProgress) // && HasItem("") >= 10
                {
                    //RemoveItems("", 10);
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

        foreach (Item item in items)
        {
            if (item.type == type && n < num)
            {
                n++;
                items.Remove(item);
            }
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
        }

        while (elapsed < maxElapsed)
        {
            elapsed += Time.deltaTime;
            progressText.text = Mathf.Round((elapsed / maxElapsed) * 100) + "%";
            yield return null;
        }

        progressText.text = "Waiting...";
        ExecuteTask();

        inProgress = false;
    }

    private void ExecuteTask()
    {
        switch (type)
        {
            case "inhaler":
                string[] atmosphere = { "oxygen", "oxygen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "nitrogen", "carbon dioxide" };
                int sendAmount = 4;
                Item[] sending = new Item[sendAmount];
                for (int i = 0; i < sendAmount; i++)
                {
                    Item item = new Item();
                    item.type = "GAS";
                    item.name = atmosphere[Random.Range(0, atmosphere.Length)];
                    sending[i] = item;
                }
                Port[] ports = GetComponentsInChildren<Port>();
                foreach (Port p in ports)
                {
                    if (p.transform.parent.GetComponentInChildren<LineRenderer>() != null)
                    {
                        StartCoroutine(Send(sending, p));
                    }
                }
                break;
        }
    }

    IEnumerator Send(Item[] sending, Port port)
    {
        if (port != null && port.connection != null)
        {
            yield return new WaitForSeconds(2);

            for (int i = 0; i < sending.Length; i++)
            {
                port.connection.GetComponentInParent<NodeAction>().RecieveItem(sending[i]);
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public string type;
    public string name; 
}