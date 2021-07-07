using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoor : MonoBehaviour
{
    bool started;
    public float touchrange;

    private void Start() {
        started=false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(Input.touchCount>0&&started==false&&other.gameObject.tag=="Player")
        {
            Debug.Log(1);
            Touch touch =Input.GetTouch(0);
            Vector3 touchpos=Camera.main.ScreenToWorldPoint(touch.position);
            //Debug.Log(touchpos.x-transform.position.x);
            if(close(touchpos,transform.position)==true)
            {
            Destroy(other.gameObject);
            started=true;
            FindObjectOfType<GameManagerScript>().ToBeContinued();
            }
          // 
        }
        
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
