using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler {
    
    Animator animCtrl;
    RectTransform joyStickRectTransform;
    RectTransform stickRecTransform;

    Vector2 inputVector = Vector2.zero;

    void Start() {
        joyStickRectTransform = GetComponent<RectTransform>();
        animCtrl = GetComponentInChildren<Animator>();
        stickRecTransform = animCtrl.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joyStickRectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out point
        );

        point.x = 2 * point.x / joyStickRectTransform.sizeDelta.x;
        point.y = 2 * point.y / joyStickRectTransform.sizeDelta.y;

        inputVector = point.magnitude > 1 ? point.normalized : point;

        stickRecTransform.anchoredPosition = new Vector2(
            inputVector.x * (joyStickRectTransform.sizeDelta.x / 2.5f), 
            inputVector.y * (joyStickRectTransform.sizeDelta.y / 2.5f)
        );
    }

    public void OnPointerDown(PointerEventData eventData) {
        animCtrl.SetBool("pressed", true);
        OnDrag(eventData);
    }
    
    public void OnPointerUp(PointerEventData eventData) {
        animCtrl.SetBool("pressed", false);
        inputVector = Vector2.zero;
        stickRecTransform.anchoredPosition = Vector2.zero;
    }

    public float GetHorizontal() {
        return inputVector.x;
    }

    public float GetVertical() {
        return inputVector.y;
    }
}
