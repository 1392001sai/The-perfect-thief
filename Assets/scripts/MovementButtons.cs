using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;


public class MovementButtons : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public bool Pressed;


    private void Start() {
        Pressed=false;

    }
    public void OnPointerDown(PointerEventData eventData)
 {
     Pressed = true;
 }
 
 public void OnPointerUp(PointerEventData eventData)
 {
     Pressed = false;
 }


    

    
}
