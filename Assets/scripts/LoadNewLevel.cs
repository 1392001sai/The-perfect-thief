using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
     ButtonClick,AnyInput,timed  
}
public class LoadNewLevel : MonoBehaviour
{

    Animator TransitionAnim;
    public TransitionType Type;
    bool loading;
    float waittime;
    float startTime;
    public float time;

    private void Start() 
    {
        startTime = 0;
        TransitionAnim=gameObject.GetComponent<Animator>();
        waittime=TransitionAnim.runtimeAnimatorController.animationClips[0].length;
        loading=false;
    }
    private void Update() 
    {
        startTime += Time.deltaTime;
        if(Input.anyKey==true)
        {
            if(Type==TransitionType.AnyInput&&loading==false)
            {
                StartCoroutine(LoadLevel());
                loading=true;
            }
        }
        if(startTime>time)
        {
            if (Type == TransitionType.timed && loading == false)
            {
                StartCoroutine(LoadLevel());
                loading = true;
            }

        }
        
    }

    public IEnumerator LoadLevel(int NextLevelIndex=-1)
    {
        if(NextLevelIndex==-1)
        {
            NextLevelIndex=SceneManager.GetActiveScene().buildIndex+1;
        }
        
        TransitionAnim.SetTrigger("Start");
        if(SceneManager.GetActiveScene().buildIndex==1||SceneManager.GetActiveScene().buildIndex==2)
        {
        FindObjectOfType<AudioManagerScript>().SetUpMorphSound("bgm");
        }
        yield return new WaitForSeconds(waittime);
        if(NextLevelIndex==-2)
        {
            Application.Quit();
        }
        else
           SceneManager.LoadScene(NextLevelIndex);
    }
    
}
