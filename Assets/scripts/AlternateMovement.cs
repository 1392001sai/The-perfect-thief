using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum playerStates
{
    Running,Sneaking,standing
}
public class AlternateMovement : MonoBehaviour
{
    float speed;
    public float RunSpeed;
    public float SneakSpeed;

    Vector2 NewRot;
    Vector2 NewPos;
    
    public bool IsVisible;
    public bool IsSneaking;
    public int NoOfViewers;
    public bool invisibility;
    public float invisibilityTime;
    public float invisibilityCoolDown;
    public float invisibilityCoolDownTime;
    float timeSpentInvisible;
    public playerStates playerState;

    public Animator PlayerAnim;

    public SpriteRenderer[] TurnInvisible;
    bool  CoolDown;
    GameManagerScript gameManager;
    public MovementButtons LeftButton;
    public MovementButtons RightButton;
    public PowerButtons invisibilityButton;
    public PowerButtons SneakButton;



    private void Start() 
    {
        gameManager=FindObjectOfType<GameManagerScript>();
        NewRot.x=0;
        IsVisible=false;
        invisibility=false;
        IsSneaking=false;
        CoolDown=false;
        playerState=playerStates.standing;
        speed=RunSpeed;
        timeSpentInvisible=0;
        invisibilityCoolDownTime=0;
        CoolDown=false;

    }
    void FixedUpdate()
    {

        if(LeftButton.Pressed==true)
        {
            if(IsSneaking==true)
            {
            playerState=playerStates.Sneaking;
            }
            else
            {
                playerState=playerStates.Running;
            }
            NewPos=transform.position;
            NewPos.x-=speed*Time.deltaTime;
            NewRot.y=180;
            transform.rotation=Quaternion.Euler(NewRot);
            transform.position=NewPos;
            
        }
        else if(RightButton.Pressed==true)
        {
            if(IsSneaking==true)
            {
                playerState=playerStates.Sneaking;
            }
            else
            {
                playerState=playerStates.Running;

            }
            NewPos=transform.position;
            NewPos.x+=speed*Time.deltaTime;
            NewRot.y=0;
            transform.rotation=Quaternion.Euler(NewRot);
            transform.position=NewPos;
                      
        }
        else if(LeftButton.Pressed==false&&RightButton.Pressed==false)
        {
            playerState=playerStates.standing;
            
        }
        
    }
    private void Update() {
        
         if(SneakButton.clicked==true)
        {
            SneakButton.clicked=false;

            if(IsSneaking==false)
            {
            SneakButton.s.gameObject.SetActive(true);
            SneakButton.r.gameObject.SetActive(false);
            IsSneaking=true;
            speed=SneakSpeed;
            }
            else
            {
            SneakButton.s.gameObject.SetActive(false);
            SneakButton.r.gameObject.SetActive(true);
                IsSneaking=false;
                speed=RunSpeed;
            }
        }
        if(playerState==playerStates.standing)
        {

            PlayerAnim.SetBool("IsStanding",true);
            PlayerAnim.SetBool("IsSneaking",false);
            PlayerAnim.SetBool("IsRunning",false);

        }
        else  if(playerState==playerStates.Running)
        { 

            PlayerAnim.SetBool("IsStanding",false);
            PlayerAnim.SetBool("IsSneaking",false);
            PlayerAnim.SetBool("IsRunning",true);

        }
        else  if(playerState==playerStates.Sneaking)
        {

            PlayerAnim.SetBool("IsStanding",false);
            PlayerAnim.SetBool("IsSneaking",true);
            PlayerAnim.SetBool("IsRunning",false);
        }
        ChangeVisibilty();
        if(CoolDown==true)
        {
        invisibilityCoolDownTime+=Time.deltaTime;
        gameManager.UpdateInvisibilityButton((invisibilityCoolDown-invisibilityCoolDownTime)/invisibilityCoolDown);
        }
        invisibilityCoolDownTime=Mathf.Clamp(invisibilityCoolDownTime,0,invisibilityCoolDown);
        
        
    }


    public void ChangeNoOfViewers(char a)
    {
        if(invisibility==false)
        {
        if(a=='i')
        {
            NoOfViewers++;
        }
        if(a=='d')
        {
            NoOfViewers--;
        }
        }

    }

    void ChangeVisibilty()
    {
        if(invisibilityButton.clicked==true&&CoolDown==false&&invisibility==false&&(playerState==playerStates.standing||playerState==playerStates.Sneaking))
        {
            invisibilityCoolDownTime=0;
            gameManager.UpdateInvisibilityButton((invisibilityCoolDown-invisibilityCoolDownTime)/invisibilityCoolDown);
            invisibilityButton.active=false;
            invisibilityButton.clicked=false;
            IsVisible=false;
            invisibility=true;
            NoOfViewers=0;
            
        foreach (SpriteRenderer Part in TurnInvisible)
        {  
            Part.color-= new Color(0,0,0,0.5f);
            
        }
            
        }
        if(invisibility==false)
        {
            if(IsVisible==false&&NoOfViewers>0)
            {
                IsVisible=true;
            }
            if(IsVisible==true&&NoOfViewers==0)
            {
                IsVisible=false;
            }

            
        }
        if(invisibility==true)
        {
            timeSpentInvisible+=Time.deltaTime;
            timeSpentInvisible=Mathf.Clamp(timeSpentInvisible,0,invisibilityTime);

        }
        if(invisibility==true&&(timeSpentInvisible==invisibilityTime||(playerState!=playerStates.standing&&playerState!=playerStates.Sneaking)))
        {
            
            CoolDown=true;

            foreach (SpriteRenderer Part in TurnInvisible)
        {
           
            Part.color+= new Color(0,0,0,0.5f);
            
        }
            
           invisibility=false;
           timeSpentInvisible=0;
        }
        if(invisibilityCoolDownTime==invisibilityCoolDown)
        {
            invisibilityButton.active=true;
            CoolDown=false;
        }
        

    }
    


     

}
