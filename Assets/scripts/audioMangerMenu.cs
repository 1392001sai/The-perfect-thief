using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioMangerMenu : MonoBehaviour
{
    public AudioSource music;

    bool increaseSound;
    bool decreaseSound;
    float volume;
    public float maxVolume;
    public float changeAmount;
    public  bool Timed;
    public float waittime;
    private void Start() {
        increaseSound=true;
        decreaseSound=false;
        if(Timed==true)
        {
            StartCoroutine(Decrease());
        }
    }

    private void FixedUpdate() {
        if(increaseSound==true)
        {
            volume=music.volume;
            volume+=changeAmount;
            volume=Mathf.Clamp(volume,0,maxVolume);
            music.volume=volume;

        }
        if(music.volume==maxVolume)
        {
            increaseSound=false;
        }
        if(decreaseSound==true)
        {
            volume=music.volume;
            volume-=changeAmount;
            volume=Mathf.Clamp(volume,0,maxVolume);
            music.volume=volume;

        }
        if(music.volume==0)
        {
            decreaseSound=false;
        }
    }

    public void StartDecrease()
    {
        increaseSound=false;
        decreaseSound=true;
    }

    IEnumerator Decrease()
    {
        yield return new WaitForSeconds(waittime);
        decreaseSound=true;
        increaseSound=false;
    }
}
