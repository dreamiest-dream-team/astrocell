using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelectable : MonoBehaviour
{
    [SerializeField]
    private GameObject background_selected;

    [Space]

    [SerializeField]
    private bool deleteable;

    private bool selected = false;
    private bool mouseOver;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = mouseOver;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selected = false;
        }

        background_selected.SetActive(selected);

        if (selected)
        {
            if (deleteable && Input.GetKeyDown(KeyCode.Delete))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
