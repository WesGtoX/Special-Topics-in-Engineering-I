using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    Animator animCtrl;
    bool downEvent = false;
    bool upEvent = false;
    bool heldEvent = false;

    void Start() {
        animCtrl = GetComponent<Animator>();
    }

    void LateUpdate() {
        downEvent = false;
        upEvent = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        animCtrl.SetBool("pressed", true);
        heldEvent = true;
        downEvent = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        animCtrl.SetBool("pressed", false);
        heldEvent = false;
        upEvent = true;
    }

    public bool GetButton() {
        return heldEvent;
    }

    public bool GetButtonDown() {
        return downEvent;
    }

    public bool GetButtonUp() {
        return upEvent;
    }
}
