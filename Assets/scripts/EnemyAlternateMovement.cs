using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;


public enum EnemyStates
{
    patrol,standing,identifying,alerted
}

public class EnemyAlternateMovement : MonoBehaviour
{
    


    public Transform pathholder;

    public EnemyStates enemyState;
    Vector2[] points;
    Rigidbody2D rb;
    float velocity;
    public float speed;
    float WaitTime;
    Vector2 NewRot;
   public UnityEngine.Experimental.Rendering.Universal.Light2D SpotLight;
   public Transform target;
   AlternateMovement AlternateMovement;
   AlternateLightDetection alternateLightDetection;
   public Animator EnemyAnim;
   public GameObject AlertedSymbol;
   public GameObject SuspiciousSymbol;
   Vector2 InvestigatePt;
   bool checkPt;
   bool guardMovement;
   //bool IsWaiting;
   float TimeWaited;
    int nextpt;
    Vector2 TargetPt;
    Vector2 originalPt;
    float dir;
    Vector2 newpos;

    

    private void Start() {
        alternateLightDetection=SpotLight.GetComponent<AlternateLightDetection>();
        AlternateMovement=target.gameObject.GetComponent<AlternateMovement>();
        NewRot.x=0;
        checkPt=false;
        TimeWaited=0;
        nextpt=0;
        points=new Vector2[pathholder.childCount];
        for(int i=0;i<pathholder.childCount;i++)
        {
            points[i]=pathholder.GetChild(i).transform.position;
        }
        SetUpGuardMovement();
        WaitTime= EnemyAnim.runtimeAnimatorController.animationClips[0].length;
        alternateLightDetection.PlayerSeen+=identifying;
        alternateLightDetection.PlayerSpotted+=alerted;
        alternateLightDetection.PlayerEscaped+=SetInvestigation;
        

    
    }
    private void OnDrawGizmos() {
        foreach (Transform point in pathholder)
        {
            Gizmos.DrawSphere(point.position,0.2f);
            
            
        }
       
        
        
    }
   
    
   void SetUpGuardMovement()
    {
       if(checkPt==true)
        {
            TargetPt=InvestigatePt;
            checkPt=false;
        }
        else
        {
            nextpt=(nextpt+1)%pathholder.childCount;
            TargetPt=points[nextpt];
        }
        originalPt=transform.position;
        dir=TargetPt.x-originalPt.x;
        if(dir>0)
        {
            NewRot.y=0;
            transform.rotation=Quaternion.Euler(NewRot);
            SuspiciousSymbol.transform.rotation=Quaternion.Euler(NewRot);
            AlertedSymbol.transform.rotation=Quaternion.Euler(NewRot);
        }
        else
        {
            NewRot.y=180;
            transform.rotation=Quaternion.Euler(NewRot);
            NewRot.y=0;
            SuspiciousSymbol.transform.rotation=Quaternion.Euler(NewRot);
            AlertedSymbol.transform.rotation=Quaternion.Euler(NewRot);
        }
         if(TargetPt.x>originalPt.x)
        {
           
            velocity=speed;
            
            
        }
        else
        {   
            velocity=-speed;
            
        }
        enemyState=EnemyStates.patrol;
        guardMovement=true;
        
   }

    private void Update() {
        if(enemyState==EnemyStates.standing)
        {
            EnemyAnim.SetBool("IsIdle",true);
            EnemyAnim.SetBool("IsWalking",false);
            EnemyAnim.SetBool("IsIdentifying",false);
        }
        if(enemyState==EnemyStates.patrol)
        {
            EnemyAnim.SetBool("IsIdle",false);
            EnemyAnim.SetBool("IsWalking",true);
            EnemyAnim.SetBool("IsIdentifying",false);
           
        }
        if(enemyState==EnemyStates.identifying)
        {
            SuspiciousSymbol.SetActive(true);
            
            EnemyAnim.SetBool("IsIdle",false);
            EnemyAnim.SetBool("IsWalking",false);
            EnemyAnim.SetBool("IsIdentifying",true);
           
        }
        if(enemyState==EnemyStates.alerted)
        {
            SuspiciousSymbol.SetActive(false);
            AlertedSymbol.SetActive(true);
        }
        if(guardMovement==true)
        {
            
        newpos=transform.position;
        newpos.x+=velocity*Time.deltaTime;
        
        if(velocity>0)
        newpos.x=Mathf.Clamp(newpos.x,originalPt.x,TargetPt.x);
        else
        newpos.x=Mathf.Clamp(newpos.x,TargetPt.x,originalPt.x);
        transform.position=newpos;
        }
        if( Mathf.Abs(transform.position.x-TargetPt.x)<0.3f&&guardMovement==true)
        {
                guardMovement=false;
                enemyState=EnemyStates.standing;
                TimeWaited=0;
        }

        
        if(enemyState==EnemyStates.standing)
        {  
            TimeWaited+=Time.deltaTime;
            TimeWaited=Mathf.Clamp(TimeWaited,0,WaitTime);
        }
        if(TimeWaited==WaitTime&&enemyState==EnemyStates.standing)
        {
            TimeWaited=0;
            SetUpGuardMovement(); 
        }
        if(enemyState!=EnemyStates.standing)
        {
            TimeWaited=0;
        }

    }



    void identifying()
    {
        FindObjectOfType<AudioManagerScript>().PlaySound("huh");
        guardMovement=false;
        enemyState=EnemyStates.identifying;
        
    }
    void SetInvestigation()
    {

        
        InvestigatePt=AlternateMovement.gameObject.transform.position;
        checkPt=true;
        SetUpGuardMovement();
    }
    void alerted()
    {

        enemyState=EnemyStates.alerted;
        if(GameManagerScript.Alertactive==false&&GameManagerScript.called==false)
        {
            GameManagerScript.Alertactive=true;
        }

    }

    private void OnTriggerStay2D(Collider2D other) {
        if(AlternateMovement.playerState==playerStates.Running&&other.CompareTag("Player"))
        {
            alerted();
        }
    }
    

     


}


