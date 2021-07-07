using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
[ExecuteInEditMode]
public class SetUpScript : MonoBehaviour
{
    public int roomno;
    public SpriteRenderer[] sprites;
    //public Light2D[] lights;

    private void Start() {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingLayerName="room"+roomno.ToString();    
        }
        
    } 

    
}
