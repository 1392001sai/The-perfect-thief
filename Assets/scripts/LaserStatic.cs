using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStatic : MonoBehaviour
{
    public float waittime;
    public float delay;
    bool changestate;
    public GameObject laser;
    bool PlayerFound;
    public Transform player;
    public float laserHeight;
    public float laserWidth;
   

    private void Start() {
        changestate=true;
        PlayerFound=false;

    }

    private void Update() {
        if(changestate==true)
        {
           StartCoroutine(waiting());
        }
        if(player!=null)
        if(Mathf.Abs(transform.position.x-player.position.x)<(laserWidth)&&transform.position.y-player.position.y<(laserHeight)&&transform.position.y>player.position.y&&PlayerFound==false&&laser.activeInHierarchy==true&&GameManagerScript.called==false)
        { FindObjectOfType<AudioManagerScript>().PlaySound("laser");
            

            //Debug.Log(gameObject.name);
            PlayerFound=true;
            if(GameManagerScript.Alertactive==false)
        {
            GameManagerScript.Alertactive=true;
        }
        }
        
    }

    IEnumerator waiting()
    {
        changestate=false;
        yield return new WaitForSeconds(waittime-delay);
        delay=0;
        laser.SetActive(!laser.activeInHierarchy);
        changestate=true;

    }
    
}
