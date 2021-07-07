using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float speed;
    public float offtime;
    public float range;
    bool active;
    Vector2 newpos;
    Vector2 originalPos;
    public bool PlayerFound;
    public GameObject laser;
    public Transform player;
    public float laserHeight;
    public float laserWidth;
   
    
    private void Start() {
        active=true;
        originalPos=transform.position;
        //Debug.Log(originalPos);
        PlayerFound=false;
    }

    private void Update() {
        if(active==true)
        {
            newpos=transform.position;
            newpos.x+=speed*Time.deltaTime;
            newpos.x=Mathf.Clamp(newpos.x,originalPos.x,originalPos.x+range);
            //Debug.Log(newpos.x);
            transform.position=newpos;
            if(transform.position.x==originalPos.x+range)
            {
                active=false;
                laser.SetActive(false);
                StartCoroutine(WaitTime());
            }
            
        }
        if(player!=null)
        if(Mathf.Abs(transform.position.x-player.position.x)<(laserWidth)&&transform.position.y-player.position.y<(laserHeight)&&transform.position.y>player.position.y&&PlayerFound==false&&laser.activeInHierarchy==true&&GameManagerScript.called==false)
        { 

            Debug.Log(gameObject.name);
            FindObjectOfType<AudioManagerScript>().PlaySound("laser");
            //Debug.Log(gameObject.name);
            PlayerFound=true;
            if(GameManagerScript.Alertactive==false)
        {
            GameManagerScript.Alertactive=true;
        }
        }

        
    }
    IEnumerator WaitTime()
    {
        
        yield return new WaitForSeconds(offtime);
        transform.position=originalPos;
        laser.SetActive(true);
        active=true;
    }
    
}
