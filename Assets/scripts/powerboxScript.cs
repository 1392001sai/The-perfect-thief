using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerboxScript : MonoBehaviour
{
    bool disabling;
    public CameraRotate cameraRotate;
    public GameObject spotlight;
    public float offtime;
    public float touchrange;


    void Start()
    {
        disabling=false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"&&disabling==false)
        {
            if(Input.touchCount>0)
           {
            Debug.Log(1);
            Touch touch =Input.GetTouch(0);
            Vector3 touchpos=Camera.main.ScreenToWorldPoint(touch.position);
            //Debug.Log(touchpos.x-transform.position.x);
            if(close(touchpos,transform.position)==true)
            {
                FindObjectOfType<AudioManagerScript>().PlaySound("short circuit");
                disabling=true;
                cameraRotate.disabled=true;
                spotlight.SetActive(false);
                StartCoroutine(enable());

            }
           }


        }
        
    }

    IEnumerator enable()
    {
        yield return new WaitForSeconds(offtime);
        spotlight.SetActive(true);
        cameraRotate.disabled=false;
        disabling=false;
    }
    
    bool close(Vector3 a,Vector3 b)
    {
       
        if(Mathf.Abs(a.x-b.x)<touchrange&&Mathf.Abs(a.y-b.y)<touchrange)
        return true;
        else
        return false;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,touchrange);
    }    

}
