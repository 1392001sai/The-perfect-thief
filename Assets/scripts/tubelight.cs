using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tubelight : MonoBehaviour
{

public Transform Player;
bool WithinRange;
public Vector2 startpt;
public Vector2 endpt;
AlternateMovement AlternateMovement;


private void Start() {
    AlternateMovement=FindObjectOfType<AlternateMovement>();

    WithinRange=false;
}

private void Update() {
    if(AlternateMovement.invisibility==false)
    {
        if(Player!=null)
    if(Player.position.x>startpt.x&&Player.position.x<endpt.x&&Player.position.y>startpt.y&&Player.position.y<endpt.y)
    {
        if(WithinRange==false)
        {
        WithinRange=true;
        //Debug.Log(1);
        AlternateMovement.ChangeNoOfViewers('i');
        }
    }
    else if(WithinRange==true)
    {
        AlternateMovement.ChangeNoOfViewers('d');
        WithinRange=false;
        
    }
    }
    else
    {
        WithinRange=false;
    }
    
    
}
}
