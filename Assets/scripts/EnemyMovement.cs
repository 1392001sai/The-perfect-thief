using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public enum guardstates
{
    patrol,suspicious,identifying,alerted,standing
};

public class EnemyMovement : MonoBehaviour
{
    Transform pathholder;
    //public Transform PatrolPathholder;
    //public Transform SuspiciousPathholder;
    public GaurdStateInfo PatrolState;
    public GaurdStateInfo SuspiciousState;
    public guardstates guardstate;
    Vector2[] points;
    Rigidbody2D rb;
    //Vector2 velocity;
    float velocity;
    float speed;
    float WaitTime;
    Vector2 NewRot;
    //public float ViewDistance;
    //public float ViewAngle;
   public UnityEngine.Experimental.Rendering.Universal.Light2D SpotLight;
   public Transform target;
   public LayerMask obstacles;
   movement movement;
   public int RoomNo;
   //public Transform GuardMesh;
   LightDetection lightDetection;
   public float HearingAccuracy;
   public Animator EnemyAnim;
   public GameObject AlertedSymbol;
   public GameObject SuspiciousSymbol;
   Vector2 StompPosition;
   bool CheckStomp;
   Vector2 InvestigstePt;
   bool checkPt;
   bool guardMovement;
   guardstates currentstate;
   bool IsWaiting;
   float TimeWaited;

    

    private void Start() {
        lightDetection=SpotLight.GetComponent<LightDetection>();
        movement=target.gameObject.GetComponent<movement>();
        rb=gameObject.GetComponent<Rigidbody2D>();
        NewRot.x=0;
        CheckStomp=false;
        checkPt=false;
        TimeWaited=0;
        IsWaiting=true;
        //guardMovement=false;
        //currentstate=guardstates.standing;
        //Debug.Log(2);
        patrol();
        //Debug.Log(EnemyAnim.runtimeAnimatorController.animationClips[0].name);
        WaitTime= EnemyAnim.runtimeAnimatorController.animationClips[0].length;
        lightDetection.PlayerSeen+=identifying;
        lightDetection.PlayerSpotted+=alerted;
        lightDetection.PlayerEscaped+=suspicious;
        //lightDetection.PlayerEscaped+=SetInvestigation;
       // movement.PlayerRunning+=CheckPlayerRange;
       // movement.PlayerStomping+=InvestigateStomping;
        
        
       //suspicious();
        
        
    
    }
    private void OnDrawGizmos() {
        foreach (Transform point in PatrolState.pathholder)
        {
            Gizmos.DrawSphere(point.position,0.2f);
            
            
        }
        Gizmos.color=Color.green;
        foreach (Transform point in SuspiciousState.pathholder)
        {
            Gizmos.DrawSphere(point.position,0.2f);
            
            
        }
        
        
    }
    



   /*bool SearchForPlayer()
   {
       float AngleToPlayer;
       if(Vector2.Distance(transform.position,target.position)<ViewDistance)
       {
           AngleToPlayer = Vector2.Angle(transform.right,(target.position-transform.position).normalized);
           if(AngleToPlayer<ViewAngle/2)
           {
               Debug.Log("pass angle");
               if(!Physics.Linecast(transform.position,target.position,obstacles)&&movement.IsVisible==true)
               {
                   
                   return true;

               }
           }
       }
       return false;
   }*/
    int nextpt=1;
    Vector2 TargetPt;
    Vector2 originalPt;
    float dir;
    Vector2 newpos;
        
    /*IEnumerator GaurdMovement(guardstates currentstate)
    {
        //Debug.Log(currentstate);
        Vector2 dir;
        int nextpt=1;
        Vector2 TargetPt;
        
        //int curpt=0;
        while(guardstate==currentstate)
        {
        if(CheckStomp==true)
        {
            TargetPt=StompPosition;
            CheckStomp=false;

        }
        else if(checkPt==true)
        {
            TargetPt=InvestigstePt;
            checkPt=false;
        }
        else
        {
            TargetPt=points[nextpt];
        }


        dir = (TargetPt-new Vector2(transform.position.x,TargetPt.y)).normalized;
        //curpt=TargetPt;
        if(Mathf.Abs(dir.x)<0.3f)
        dir.x=0;
        velocity=speed*dir;
        
        
        rb.velocity=velocity;
        if(dir.x>0)
        {
            //Debug.Log(3);
            NewRot.y=0;
            transform.rotation=Quaternion.Euler(NewRot);
        }
        else
        {
            //Debug.Log(1);
            NewRot.y=180;
            transform.rotation=Quaternion.Euler(NewRot);
        }
        if(velocity.x!=0)
        {
        yield return new WaitUntil(()=>guardstate!=currentstate||Mathf.Abs(transform.position.x-TargetPt.x)<0.3f);
        }
        if(velocity.x==0)
        {
            guardstate=guardstates.standing;

        }
        velocity.x=0;
        rb.velocity=velocity;
        if(guardstate==currentstate&&Mathf.Abs(transform.position.x-TargetPt.x)<0.5f)
        {
        guardstate=guardstates.standing;
        yield return new WaitForSeconds(WaitTime);
        if(guardstate==guardstates.standing)
        guardstate=currentstate;
        nextpt=(nextpt+1)%pathholder.childCount;
        }
        }
   }*/
   //int k=0;
   void StartGuardMovement(guardstates currentstateset)
   {

       //Debug.Log(1);
       
       if(currentstate!=currentstateset){
       nextpt=1;
       }
       
       if(checkPt==true)
        {
            //Debug.Log(1);
            TargetPt=InvestigstePt;
            checkPt=false;
        }
           
        else  if(CheckStomp==true)
        {
            TargetPt=StompPosition;
            CheckStomp=false;

        }
        else
        {
            TargetPt=points[nextpt];
        }
        originalPt=transform.position;
        dir=TargetPt.x-originalPt.x;
       
        //Debug.Log(dir);
        if(dir>0)
        {
            //Debug.Log(3);
            NewRot.y=0;
            transform.rotation=Quaternion.Euler(NewRot);
            SuspiciousSymbol.transform.rotation=Quaternion.Euler(NewRot);
            AlertedSymbol.transform.rotation=Quaternion.Euler(NewRot);
        }
        else
        {
            //Debug.Log(1);
            NewRot.y=180;
            transform.rotation=Quaternion.Euler(NewRot);
            NewRot.y=0;
            SuspiciousSymbol.transform.rotation=Quaternion.Euler(NewRot);
            AlertedSymbol.transform.rotation=Quaternion.Euler(NewRot);
        }
        if(TargetPt.x==originalPt.x)
        {
            //Debug.Log(2);
            velocity=0;
        }
        else if(TargetPt.x>originalPt.x)
        {
           
            velocity=speed;
            
            
        }
        else if(TargetPt.x<originalPt.x)
        {
            
            
            velocity=-speed;
            
            //Debug.Log(1);
        }
        //if(velocity!=0)
        currentstate=currentstateset;
        guardMovement=true;
        
   }

    private void Update() {
        if(guardstate==guardstates.standing)
        {
            EnemyAnim.SetBool("IsIdle",true);
            EnemyAnim.SetBool("IsWalking",false);
            EnemyAnim.SetBool("IsIdentifying",false);
            EnemyAnim.SetBool("IsRunning",false);
        }
        if(guardstate==guardstates.patrol)
        {
            EnemyAnim.SetBool("IsIdle",false);
            EnemyAnim.SetBool("IsWalking",true);
            EnemyAnim.SetBool("IsIdentifying",false);
            EnemyAnim.SetBool("IsRunning",false);
        }
        if(guardstate==guardstates.suspicious)
        {
            SuspiciousSymbol.SetActive(true);
            EnemyAnim.SetBool("IsIdle",false);
            EnemyAnim.SetBool("IsWalking",false);
            EnemyAnim.SetBool("IsIdentifying",false);
            EnemyAnim.SetBool("IsRunning",true);
        }
        if(guardstate==guardstates.identifying)
        {
            
            EnemyAnim.SetBool("IsIdle",false);
            EnemyAnim.SetBool("IsWalking",false);
            EnemyAnim.SetBool("IsIdentifying",true);
            EnemyAnim.SetBool("IsRunning",false);
        }
        if(guardstate==guardstates.alerted)
        {
            SuspiciousSymbol.SetActive(false);
            AlertedSymbol.SetActive(true);
        }
        if(guardMovement==true)
        {
            
        newpos=transform.position;
        newpos.x+=velocity*Time.deltaTime;
        //Debug.Log(velocity);
        /*if(RoomNo==1&&TargetPt.x<originalPt.x)
        {
        originalPt.x+=1;
        }*/
        
        if(velocity>0)
        newpos.x=Mathf.Clamp(newpos.x,originalPt.x,TargetPt.x);
        else
        newpos.x=Mathf.Clamp(newpos.x,TargetPt.x,originalPt.x);
        //Debug.Log(newpos);
        transform.position=newpos;
        }
        if(transform.position.x==TargetPt.x&&guardMovement==true)
        {
            if(guardstate==guardstates.patrol||guardstate==guardstates.suspicious)
            {
                
                guardMovement=false;
                //StopCoroutine(Standing());
                //StartCoroutine(Standing());
             
                
            }
               if(IsWaiting==false)
            {
                guardstate=guardstates.standing;
                IsWaiting=true;
            }
        }
        
        if(IsWaiting==true)
        {
            
            TimeWaited+=Time.deltaTime;
            TimeWaited=Mathf.Clamp(TimeWaited,0,WaitTime);
        }
        
        if(guardMovement==true&&(guardstate!=guardstates.suspicious&&guardstate!=guardstates.patrol))
        {
            //Debug.Log(1);
            guardMovement=false;
        }
        //Debug.Log(currentstate);
        if(TimeWaited==WaitTime&&IsWaiting==true)
        {
            nextpt=(nextpt+1)%pathholder.childCount;
            //Spathholder.GetChild(nextpt).name
            //Debug.Log(currentstate);
            StartGuardMovement(currentstate); 
            //k=1;
            if(originalPt.x!=TargetPt.x)
            {
                    
            guardstate=currentstate;
            }
            IsWaiting=false;
            TimeWaited=0;
            
        }
        //Debug.Log(TimeWaited+"waited");
        if(guardstate!=guardstates.standing)
        {
           // if(guardstate!=guardstates.suspicious)
            //Debug.Log(guardstate);
            
            IsWaiting=false;
            TimeWaited=0;
            
        }
        //Debug.Log(TimeWaited+"statechange");
        //Debug.Log(guardstate);
        //Debug.Log(TimeWaited);

    }

    /*IEnumerator Standing()
    {
        guardstate=guardstates.standing;
        //Debug.Log(1);
        yield return new WaitForSeconds(WaitTime);
        if(checkPt==true)
        {
            yield return new WaitForSeconds(WaitTime);
        }
        //Debug.Log(1);
        if(guardstate==guardstates.standing&&transform.position.x==TargetPt.x)
        {
            guardstate=currentstate;
            nextpt=(nextpt+1)%pathholder.childCount;
            StartGuardMovement(currentstate);
            
        }
        
        

    }*/

    void identifying()
    {
        //StopAllCoroutines();
        guardstate=guardstates.identifying;
        
        //Debug.Log(1);
    }
    void SetInvestigation()
    {
        //Debug.Log(1);
        InvestigstePt=movement.gameObject.transform.position;
        checkPt=true;
    }
    void alerted()
    {
        //StopAllCoroutines();
        guardstate=guardstates.alerted;
        //Debug.Log(2);
        //restart game
    }
    void suspicious()
    {
        //StopAllCoroutines();
        SetInvestigation();
        if(guardstate!=guardstates.suspicious)
        {
            
        guardstate=guardstates.suspicious;
        
        pathholder=SuspiciousState.pathholder;
        speed=SuspiciousState.speed;
        points=new Vector2[pathholder.childCount];
        //Debug.Log(points.Length);
        for(int i=0;i<pathholder.childCount;i++)
        {
            points[i]=pathholder.GetChild(i).position;
        }
        }
        //sDebug.Log("co call");
        //StopCoroutine(GaurdMovement(guardstates.patrol));
        //StartCoroutine(GaurdMovement(guardstates.suspicious));
        StartGuardMovement(guardstates.suspicious);
        

    }
    //void ChangeGuardState(guardstates guardstate)
    void patrol()
    {
        //StopAllCoroutines();
        //if(guardstate!=guardstates.patrol)
        //{
        guardstate=guardstates.patrol;
        if(pathholder!=PatrolState.pathholder)
        {
        pathholder=PatrolState.pathholder;
        speed=PatrolState.speed;
        points=new Vector2[pathholder.childCount];
        for(int i=0;i<pathholder.childCount;i++)
        {
            points[i]=pathholder.GetChild(i).position;
        }
        }
        //Debug.Log(1);
        //StartCoroutine(GaurdMovement(guardstates.patrol));
        StartGuardMovement(guardstates.patrol);
        //}
    }
    void CheckPlayerRange(int PlayerRoomNo,float SoundRange)
    {
        if(PlayerRoomNo==RoomNo)
        {
            if(Vector2.Distance(target.position,transform.position)<SoundRange)
            {
                
                if(lightDetection.TimeSeen<(lightDetection.MaxAlertTime-HearingAccuracy))
                {
                    lightDetection.TimeSeen+=HearingAccuracy;
                }
                
                
            }
        }

    }
    void ReloadScene()
     {
        SceneManager.LoadScene("SampleScene");
     }
     void InvestigateStomping(Vector2 PlayerStompPosition,int PlayerRoomNo,float StompRange)
     {
         if(guardstate==guardstates.patrol||guardstate==guardstates.standing)
         {
          if(Vector2.Distance(transform.position,PlayerStompPosition)<StompRange)
          {
              if(RoomNo==PlayerRoomNo)
              {
                  //Debug.Log(1);
                  StompPosition=PlayerStompPosition;
                  if(lightDetection.TimeSeen<(lightDetection.MaxAlertTime-0.5f))
                  lightDetection.TimeSeen=lightDetection.MaxAlertTime-0.5f;
                  CheckStomp=true;
              }

          }
         }

     }


}
