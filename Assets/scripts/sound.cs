using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class sound 
{
    public string name;
    public AudioClip clip;

    public float volume;

    public float pitch;
    public bool loop;
    public float MorphTime;
    [HideInInspector]
    public AudioSource source;   
    
}
