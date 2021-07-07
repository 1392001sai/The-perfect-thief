using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdIdleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform guard;
    public Transform SuspiciousSymbol;
    public Transform AlertedSymbol;
    Vector2 newrot;
    void TurnToOpposite()
    {
        newrot=guard.rotation.eulerAngles;
        if(newrot.y==180)
        newrot.y=0;
        else
        {
            newrot.y=180;
        }
        guard.rotation=Quaternion.Euler(newrot);
        newrot.y=0;
        SuspiciousSymbol.rotation=Quaternion.Euler(newrot);
        AlertedSymbol.rotation=Quaternion.Euler(newrot);
        
    }
    
}
