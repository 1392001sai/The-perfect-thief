using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LightDetection : MonoBehaviour
{
    float ViewDistance;
    float ViewAngle;
    public float PlayerWidthConcession;
    public LayerMask obstacles;

    UnityEngine.Experimental.Rendering.Universal.Light2D spotlight;

    public Transform target;
    //public Transform ground;
    bool SeeingPlayer;
    public Text YouWereSpotted;
    public float MaxAlertTime;
    public float TimeSeen;
    Color OriginalSpotLightColor;
    public event Action PlayerSeen;
    public event Action PlayerSpotted;
    public event Action PlayerEscaped;
    bool spottedPlayer;
    bool identifying;
    movement movement;

    private void Start() {
        spotlight=gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        ViewAngle=spotlight.pointLightOuterAngle;
        ViewDistance=spotlight.pointLightOuterRadius;
        SeeingPlayer=false;
        if(gameObject.tag!="gaurdlight")
        ViewAngle+=PlayerWidthConcession;
        TimeSeen=0;  
        OriginalSpotLightColor=spotlight.color;  
        movement=FindObjectOfType<movement>(); 
        spottedPlayer=false;
        identifying=false;
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.blue;
        Gizmos.DrawRay(transform.position,transform.up*(ViewDistance));//why divide by 2 incase it was on gaurd
    }
     void Update()
     {
         //Debug.Log(TimeSeen);
         CheckForPlayer();
         /*if(SeeingPlayer==true)
         {
             TimeSeen+=Time.deltaTime;
         }
         else
         {
             TimeSeen-=Time.deltaTime;
         }
         TimeSeen=Mathf.Clamp(TimeSeen,0,MaxAlertTime);
         if(gameObject.tag!="light")
         {
            spotlight.color=Color.Lerp(OriginalSpotLightColor,Color.red,TimeSeen/MaxAlertTime);
            if(TimeSeen>=MaxAlertTime)
            {
                YouWereSpotted.gameObject.SetActive(true);
                Invoke("ReloadScene",2);

            }
         }*/
     }

     bool IsPlayerVisible()
     {
         float AngleToPlayer;

        if(movement.invisibility==false)
        {
            if(Vector2.Distance(transform.position,target.position)<ViewDistance)
        {
           AngleToPlayer = Vector2.Angle(transform.up,(target.position-transform.position).normalized);

           if(AngleToPlayer<ViewAngle/2)
           {
               
               if(!Physics.Linecast(transform.position,target.position,obstacles))
               {
                   if(gameObject.tag=="gaurdlight")
                   {
                    if(movement.IsVisible==true)
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
         //if(gameObject.tag=="gaurdlight")
         //Debug.Log(TimeSeen);
        if(IsPlayerVisible()==true)
        {          
                   
                  if(gameObject.tag!="gaurdlight"&&SeeingPlayer==false)
                  { 
                  movement.ChangeNoOfViewers('i');
                  
                  }
                  
                  if(gameObject.tag=="gaurdlight"&&movement.IsVisible==true)
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
                      //EnemyMovement enemyMovement;
                      //enemyMovement=transform.parent.GetComponent<EnemyMovement>();
                      //enemyMovement.guardstate=guardstates.identifying;
                      //spotlight.color=Color.red;
                      //YouWereSpotted.gameObject.SetActive(true);
                      //Invoke("ReloadScene",2);

                  }
                  else if(gameObject.tag=="CameraLight")
                  {
                      TimeSeen+=Time.deltaTime;
                      if(spottedPlayer==false&&TimeSeen>=MaxAlertTime)
                     {
                      PlayerSpotted();
                      spottedPlayer=true;
                     }

                      //CameraRotate cameraRotate;
                      //cameraRotate=transform.parent.parent.GetComponent<CameraRotate>();
                      //cameraRotate.PlayerFound=true;
                      //spotlight.color=Color.red;
                      //YouWereSpotted.gameObject.SetActive(true);
                      //Invoke("ReloadScene",2);
                  }
                  if(gameObject.tag!="guardlight")
                  SeeingPlayer=true;

               
           
       }
        else
        {
           
           if(gameObject.tag!="gaurdlight"&&SeeingPlayer==true&&movement.invisibility==false)
           {
           movement.ChangeNoOfViewers('d');
           
           }
           
           if(gameObject.tag=="gaurdlight")
           {
               
               //TimeSeen-=Time.deltaTime;
               if(TimeSeen>0&&spottedPlayer==false&&identifying!=false)
               {
                   //EnemyMovement enemyMovement=
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
