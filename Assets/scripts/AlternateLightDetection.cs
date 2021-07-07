using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AlternateLightDetection : MonoBehaviour
{
    float ViewDistance;
    float ViewAngle;
    public float PlayerWidthConcession;
    public LayerMask obstacles;

    UnityEngine.Experimental.Rendering.Universal.Light2D spotlight;

    public Transform target;
    //public Transform ground;
    bool SeeingPlayer;
    //public Text YouWereSpotted;
    public float MaxAlertTime;
    public float TimeSeen;
    Color OriginalSpotLightColor;
    public event Action PlayerSeen;
    public event Action PlayerSpotted;
    public event Action PlayerEscaped;
    bool spottedPlayer;
    bool identifying;
    AlternateMovement AlternateMovement;

    private void Start() {
        spotlight=gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        ViewAngle=spotlight.pointLightOuterAngle;
        ViewDistance=spotlight.pointLightOuterRadius;
        SeeingPlayer=false;
        if(gameObject.tag!="gaurdlight")
        ViewAngle+=PlayerWidthConcession;
        TimeSeen=0;  
        OriginalSpotLightColor=spotlight.color;  
        AlternateMovement=FindObjectOfType<AlternateMovement>(); 
        spottedPlayer=false;
        identifying=false;
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.blue;
        Gizmos.DrawRay(transform.position,transform.up*(ViewDistance));//why divide by 2 incase it was on gaurd
    }
     void Update()
     {
         
         CheckForPlayer();
         
     }

     bool IsPlayerVisible()
     {
         float AngleToPlayer;

        if(AlternateMovement.invisibility==false)
        {
            if(target!=null)
            if(Vector2.Distance(transform.position,target.position)<ViewDistance)
        {
           AngleToPlayer = Vector2.Angle(transform.up,(target.position-transform.position).normalized);

           if(AngleToPlayer<ViewAngle/2)
           {
               
               if(!Physics.Linecast(transform.position,target.position,obstacles))
               {
                   if(gameObject.tag=="gaurdlight")
                   {
                    if(AlternateMovement.IsVisible==true)
                    return true;
                    else
                    {
                        return false;
                    }
                   }
                   else
                   {
                       return true;
                   }
               }
           }
        }

        }
        return false;
     }

     void CheckForPlayer()
     {

        if(IsPlayerVisible()==true)
        {          
                   
                  if(gameObject.tag!="gaurdlight"&&SeeingPlayer==false)
                  { 
                  AlternateMovement.ChangeNoOfViewers('i');
                  
                  }
                  
                  if(gameObject.tag=="gaurdlight"&&AlternateMovement.IsVisible==true)
                  {
                      TimeSeen+=Time.deltaTime;
                      if(identifying==false)
                      {
                          PlayerSeen();
                          identifying=true;
                      }

                      if(TimeSeen>=MaxAlertTime&&spottedPlayer==false)
                     {
                      PlayerSpotted();
                      spottedPlayer=true;
                     }
                      

                  }
                  else if(gameObject.tag=="CameraLight")
                  {
                      TimeSeen+=Time.deltaTime;
                      if(spottedPlayer==false&&TimeSeen>=MaxAlertTime)
                     {
                      PlayerSpotted();
                      spottedPlayer=true;
                     }

                      
                  }
                  if(gameObject.tag!="guardlight")
                  SeeingPlayer=true;

               
           
       }
        else
        {
           
           if(gameObject.tag!="gaurdlight"&&SeeingPlayer==true&&AlternateMovement.invisibility==false)
           {
           AlternateMovement.ChangeNoOfViewers('d');
           
           }
           
           if(gameObject.tag=="gaurdlight")
           {
               if(TimeSeen>0&&spottedPlayer==false&&identifying!=false)
               {
                   PlayerEscaped();

               }
               identifying=false;
               
           }
           SeeingPlayer=false;
           
           
       
        }
      
      
      TimeSeen=Mathf.Clamp(TimeSeen,0,MaxAlertTime);
      if(gameObject.tag=="gaurdlight"||gameObject.tag=="CameraLight")
      {
      spotlight.color=Color.Lerp(OriginalSpotLightColor,Color.red,TimeSeen/MaxAlertTime);
      }

     }

     
}


