using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AudioManagerScript : MonoBehaviour
{
    public sound[] sounds;
    sound bgm;
    Coroutine LastMorphSound;
    
    private void Awake() 
    {
        foreach(sound s in sounds)
        {
            s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip=s.clip;
            s.source.volume=s.volume;
            s.source.pitch=s.pitch;
            s.source.loop=s.loop;
            if(s.name=="bgm")
            {
                s.source.volume=0;
            }
        }
        
    }
    float finalVolume,InitialVolume;
    sound sa;
    private void Start() 
    {
        PlaySound("bgm");
        SetUpMorphSound("bgm");
        
        
    }
   
    

    public void PlaySound(string name)
    {
        sound s=Array.Find(sounds,sound=>sound.name==name);
        s.source.Play();
    }

    public void SetUpMorphSound(string name)
    {
        if(LastMorphSound!=null)
        {
            StopCoroutine(LastMorphSound);
        }
        LastMorphSound=StartCoroutine(MorphSound(name));

    }

    IEnumerator MorphSound(string name)
    {
        
        float startTime=Time.time,percentComplete=0;
        sound s=Array.Find(sounds,sound=>sound.name==name);
        float finalVolume,InitialVolume;
        if(s.source.volume==0)
        {
            finalVolume=s.volume;
        }
        else
        {
            finalVolume=0;
        }
        
        InitialVolume=s.source.volume;
        while(percentComplete-1<0.1)
        {
            s.source.volume=Mathf.Lerp(InitialVolume,finalVolume,percentComplete);
            percentComplete=(Time.time-startTime)/s.MorphTime;

            yield return null;
        }
    }
}
