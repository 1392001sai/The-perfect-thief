using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class CameraRotate : MonoBehaviour
{
    public float StartAngle;
    public float EndAngle;
    //Vector3 newrot;
    public float RotateSpeed;
    public float WaitTime;

    public bool PlayerFound;
    public UnityEngine.Experimental.Rendering.Universal.Light2D spotlight;
    Vector3 newrot;
    Rigidbody2D rb;
    bool active;
    float targetAngle;
    public bool disabled;
    AlternateLightDetection alternateLightDetection;

    private void Start() {
        PlayerFound=false;
        rb=gameObject.GetComponent<Rigidbody2D>();
        newrot=transform.rotation.eulerAngles;
        active=true;
        disabled=false;
        targetAngle=EndAngle;
        //transform.rotation=Quaternion.Euler(newrot);
        //StartCoroutine(Rotate());
        alternateLightDetection=spotlight.GetComponent<AlternateLightDetection>();
        alternateLightDetection.PlayerSpotted+=spotted;

        
    }

    private void Update() {
        if(disabled==false)
        {
        if(active==true&&PlayerFound==false)
        {
            newrot.z+=RotateSpeed*Time.deltaTime;
            newrot.z=Mathf.Clamp(newrot.z,StartAngle,EndAngle);
            transform.rotation=Quaternion.Euler(newrot);
            
        }
        //Debug.Log(transform.rotation.eulerAngles.z);
        //Debug.Log(EndAngle);
        if(newrot.z==targetAngle&&active==true) //why euler angles dont work
            {
            active=false;
            StartCoroutine(changeDirection());
            }
        }

        
        
    }
    IEnumerator changeDirection()
    {

        yield return new WaitForSeconds(WaitTime);
        if(targetAngle==EndAngle)
        {
            targetAngle=StartAngle;
        }
        else
        {
            targetAngle=EndAngle;
        }
        RotateSpeed=-RotateSpeed;
        active=true;
    }
    void spotted()
    {
        
        if(GameManagerScript.Alertactive==false&&GameManagerScript.called==false)
        {
            GameManagerScript.Alertactive=true;
        }
    }


}
