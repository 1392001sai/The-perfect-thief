using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySoundCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public float size;
    Vector2 newsize;
    public float speed;
    void Destroy()
    {
        Destroy(gameObject);
    }
    private void FixedUpdate() {
        if(transform.localScale.x==size)
        {
            Destroy(gameObject);
        }
        newsize=transform.localScale;
        newsize.x+=speed*Time.deltaTime;
        newsize.y=newsize.x;   
        newsize.x=Mathf.Clamp(newsize.x,0,size);
        transform.localScale=newsize;
    }
    
}
