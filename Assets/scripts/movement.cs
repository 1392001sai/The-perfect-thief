using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState
{
    Running,Sneaking,standing,stomping
}
public class movement : MonoBehaviour
{
    float speed;
    public float RunSpeed;
    public float SneakSpeed;
    //public float jumpspeed;
   // bool IsJumping;
    Vector2 velocity;
    Vector2 NewRot;
    Rigidbody2D rb;
    public bool IsVisible;
    public bool IsSneaking;
    public static event Action<int,float> PlayerRunning;//event necessary? remember static
    
   public static event Action<Vector2,int,float> PlayerStomping;
    public int NoOfViewers;
    public bool invisibility;
    public float invisibilityTime;
    float timeSpentInvisible;
    public PlayerState playerState;
    public int RoomNo;
    public float SoundRange;
    public Animator PlayerAnim;
    public GameObject SoundCircle;
    public GameObject StompCircle;
    float SoundCircleTime;
    public float TimeToNextSoundCircle;
    public SpriteRenderer[] TurnInvisible;
    public bool IsStomping;
    public float StompRange;
    float OriginalSoundRange;
    //Color PartColor; 


    private void Start() 
    {
        RoomNo=1;
        NewRot.x=0;
        rb=gameObject.GetComponent<Rigidbody2D>();
       // IsJumping=false;
        IsVisible=false;
        invisibility=false;
        IsSneaking=false;
        playerState=PlayerState.standing;
        speed=RunSpeed;
        SoundCircleTime=TimeToNextSoundCircle;
        IsStomping=false;
        timeSpentInvisible=0;

    }
    void FixedUpdate()
    {
       
        /*if(Input.GetKeyDown(KeyCode.W)&&IsJumping==false)
        {
            velocity.y=jumpspeed;
            IsJumping=true;
            
        }
        else
        {
            velocity.y=rb.velocity.y;
        }*/
        
        if(Input.GetKey(KeyCode.A)/*&&IsJumping==false*/)
        {
            if(IsSneaking==true)
            {
            playerState=PlayerState.Sneaking;
            }
            else
            {
                if(PlayerRunning!=null)
               PlayerRunning(RoomNo,SoundRange);
                playerState=PlayerState.Running;
            }
            velocity.x=-speed;
            NewRot.y=180;
            transform.rotation=Quaternion.Euler(NewRot);
            
        }
        else if(Input.GetKey(KeyCode.D)/*&&IsJumping==false*/)
        {
            if(IsSneaking==true)
            {
                playerState=PlayerState.Sneaking;
            }
            else
            {
               if(PlayerRunning!=null)
               PlayerRunning(RoomNo,SoundRange);
                playerState=PlayerState.Running;

            }
            velocity.x=speed;
            NewRot.y=0;
            transform.rotation=Quaternion.Euler(NewRot);
                      
        }
        else if((Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D))/*&&IsJumping==false*/)
        {

            velocity.x=0;
            playerState=PlayerState.standing;
            
        }
        
        rb.velocity=velocity;
        
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.S)&&IsStomping==false&&playerState==PlayerState.standing)
        {
            playerState=PlayerState.stomping;
            IsStomping=true;
            
            PlayerAnim.SetBool("IsStanding",false);
            PlayerAnim.SetBool("IsStomping",true);
            PlayerStomping(transform.position,RoomNo,StompRange);

        }
        if(SoundCircleTime!=TimeToNextSoundCircle)
        SoundCircleTime+=Time.deltaTime;
         if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(IsSneaking==false)
            {
            IsSneaking=true;
            speed=SneakSpeed;
            }
            else
            {
                IsSneaking=false;
                speed=RunSpeed;
            }
        }
        if(playerState==PlayerState.standing)
        {
            PlayerAnim.SetBool("IsStanding",true);
            PlayerAnim.SetBool("IsSneaking",false);
            PlayerAnim.SetBool("IsRunning",false);
            SoundCircleTime-=Time.deltaTime;

        }
        else  if(playerState==PlayerState.Running)
        {   
            if(SoundCircleTime==TimeToNextSoundCircle&&invisibility==false)
            {
            Instantiate(SoundCircle,transform.position,Quaternion.identity);
            SoundCircleTime=0;
            }
            PlayerAnim.SetBool("IsStanding",false);
            PlayerAnim.SetBool("IsSneaking",false);
            PlayerAnim.SetBool("IsRunning",true);

        }
        else  if(playerState==PlayerState.Sneaking)
        {
            PlayerAnim.SetBool("IsStanding",false);
            PlayerAnim.SetBool("IsSneaking",true);
            PlayerAnim.SetBool("IsRunning",false);
            SoundCircleTime-=Time.deltaTime;
        }
        
        SoundCircleTime=Mathf.Clamp(SoundCircleTime,0,TimeToNextSoundCircle);
        ChangeVisibilty();
        
        
    }

    /*private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="ground"&&IsJumping==true)
        {
            IsJumping=false;
            velocity.x=0;
            velocity.y=0;
            rb.velocity=velocity;
        }
    }*/

    public void ChangeNoOfViewers(char a)
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

    void ChangeVisibilty()
    {
        if(Input.GetKeyDown(KeyCode.I)&&invisibility==false&&(playerState==PlayerState.standing||playerState==PlayerState.Sneaking))
        {
            IsVisible=false;
            invisibility=true;
            NoOfViewers=0;
            OriginalSoundRange=SoundRange;
        SoundRange=0;
        foreach (SpriteRenderer Part in TurnInvisible)
        {   //Debug.Log(1);
            //Debug.Log(Part.gameObject.name);
            //PartColor=Part.color;
            Part.color-= new Color(0,0,0,0.5f);
            //Part.color=PartColor;
            
        }
            
            //play invisibility animation
            //start coroutine to stop invisibility
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
        if(invisibility==true&&(timeSpentInvisible==invisibilityTime||(playerState!=PlayerState.standing&&playerState!=PlayerState.Sneaking)))
        {
            
            foreach (SpriteRenderer Part in TurnInvisible)
        {
            //PartColor=Part.color;
            //PartColor.a=255;
            //Part.color=PartColor;
            Part.color+= new Color(0,0,0,0.5f);
            
        }
           invisibility=false;
           timeSpentInvisible=0;
           SoundRange=OriginalSoundRange;
        }
        

    }
    /*IEnumerator invisibilityControl()
    {
        IsVisible=false;
        invisibility=true;
        NoOfViewers=0;
        

        yield return new WaitForSeconds(invisibilityTime);
        
        invisibility=false;
        SoundRange=temp;
    }*/
    private void OnDrawGizmos() {
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position,SoundRange);
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,StompRange);
    }

    public void InstantiateStompCircle()
    {
        Instantiate(StompCircle,transform.position,Quaternion.identity);
    }
     


}
