using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AddNodeMenuManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject entryPrefab;
    public Transform entries;

    public Transform instantiatedNodes;

    public Vector3 offset;
    public Vector3 menuMouseOffset;

    private Transform curEntry;
    private bool over;

    private ModeController mode;

    public void OnPointerEnter(PointerEventData eventData)
    {
        over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
    }

    private void Start()
    {
        mode = FindObjectOfType<ModeController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.position = Input.mousePosition + menuMouseOffset;

            OpenMenu();
        }

        if (Input.GetMouseButtonDown(0) && !over)
        {
            CloseMenu();
        }

        if (!mode.edit)
        {
            CloseMenu();
        }
    }

    private void GenerateMenus()
    {
        if (curEntry.GetComponent<Node>() != null)
        {
            CreateNode();
            return;
        }

        CloseMenu();

        int i = 0;
        foreach (Transform child in curEntry)
        {
            GameObject go = Instantiate(entryPrefab, transform.position - offset * i, Quaternion.identity, transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = child.name;
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { SetCurEntry(child); });
            i++;
        }
    }

    public void SetCurEntry(Transform entry)
    {
        curEntry = entry;
        GenerateMenus();
    }

    private void CreateNode()
    {
        GameObject go = Instantiate(curEntry.gameObject, Vector3.zero, Quaternion.identity, instantiatedNodes);

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        go.transform.localPosition = pos;

        CloseMenu();
    }

    public void OpenMenu()
    {
        curEntry = entries;
        GenerateMenus();
    }

    public void CloseMenu()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
