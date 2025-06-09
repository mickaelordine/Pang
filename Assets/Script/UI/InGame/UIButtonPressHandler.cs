using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UIButtonPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action OnPress;
    public Action OnRelease;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPress?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnRelease?.Invoke();
    }
}
