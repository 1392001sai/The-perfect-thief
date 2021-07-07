using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static bool Alertactive;
    public static Vector2 RestartPos;
    public static bool RestartPosSet;
    static Animator lightanim;
    static LoadNewLevel loadNewLevel;
    public float waittime;
    //GameObject player;
    private static GameManagerScript instance;
    static Image invisibiltyButton;

    public static bool called;
    


    private void Awake() {  
    lightanim=GameObject.FindGameObjectWithTag("global light").GetComponent<Animator>();   
    invisibiltyButton=GameObject.Find("Fill").GetComponent<Image>();
    loadNewLevel=GameObject.FindGameObjectWithTag("TransitionScreen").GetComponent<LoadNewLevel>();
    called=false;
    
    invisibiltyButton.fillAmount=0;
        
    
        if(instance!=null)
       {
           Destroy(gameObject);
       }
       else
       {
           FindObjectOfType<DialogueGenerator>().loaddialogue("intro");
           /*player=FindObjectOfType<AlternateMovement>().gameObject;
           if(RestartPosSet==false)
           {
               RestartPos=player.transform.position;
           }
           
           player.transform.position=RestartPos;*/
           instance=this;
           DontDestroyOnLoad(gameObject);
           

       

        
       }
    }


    


    private void Update() {
        if(Alertactive==true&&called==false)
        { 
            called=true;
             StopAllCoroutines();          
            Restart();
            Alertactive=false;
            
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           StartCoroutine(loadNewLevel.LoadLevel(1));
        }
    }

    void Restart()
    { 
        FindObjectOfType<DialogueGenerator>().loaddialogue("alerted");
        FindObjectOfType<AudioManagerScript>().PlaySound("alarm");
        lightanim.SetBool("alert",true);
        StartCoroutine(loadNewLevel.LoadLevel(SceneManager.GetActiveScene().buildIndex));
  
    }

     public void UpdateInvisibilityButton(float sliderAmount)
    {
        invisibiltyButton.fillAmount=sliderAmount;


    }
    public void ToBeContinued()
    {
        
        StartCoroutine(loadNewLevel.LoadLevel());
    }
}
