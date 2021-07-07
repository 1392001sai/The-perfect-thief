using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class door : MonoBehaviour
{

    public Transform spawnpoint;
    public Transform player;
    public float DoorWidth;
    public float DoorHeight;
    public bool unlocked;
    public float touchrange;
    
    static bool IsPressing=false;

   private void Update() {
       if(player!=null)
       if(Mathf.Abs(player.position.x-transform.position.x)<(DoorWidth/2)&&Mathf.Abs(player.position.y-transform.position.y)<(DoorHeight/2))   
       {
           
        if(Input.touchCount>0&&IsPressing==false&&unlocked==true)
        {
            Debug.Log(1);
            Touch touch =Input.GetTouch(0);
            Vector3 touchpos=Camera.main.ScreenToWorldPoint(touch.position);
            //Debug.Log(touchpos.x-transform.position.x);
            if(close(touchpos,transform.position)==true)
            {
               FindObjectOfType<AudioManagerScript>().PlaySound("door");
               IsPressing=true;
               AlternateMovement AlternateMovement=player.gameObject.GetComponent<AlternateMovement>();
               player.position=spawnpoint.position;
               //GameManagerScript.RestartPos=spawnpoint.position;
               //GameManagerScript.RestartPosSet=true;
           }
        }
         if(Input.touchCount>0&&IsPressing==false&&unlocked==false)
        {
            Debug.Log(1);
            Touch touch =Input.GetTouch(0);
            Vector3 touchpos=Camera.main.ScreenToWorldPoint(touch.position);
            //Debug.Log(touchpos.x-transform.position.x);
            if(close(touchpos,transform.position)==true)
            {

               FindObjectOfType<DialogueGenerator>().loaddialogue("locked door");
            }

        }
       
       }
       else if(Input.touchCount==0)
       {
           
           IsPressing=false;
       }

       
   }
    bool close(Vector3 a,Vector3 b)
    {
       
        if(Mathf.Abs(a.x-b.x)<touchrange&&Mathf.Abs(a.y-b.y)<touchrange)
        return true;
        else
        return false;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,touchrange);
    }
   
}
