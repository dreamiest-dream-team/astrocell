using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public bool mouseOver;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
