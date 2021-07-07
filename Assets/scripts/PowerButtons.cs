using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButtons : MonoBehaviour
{
    public bool clicked;
    public bool active;
    public Text s;
    public Text r;

    private void Start() {
        clicked=false;
        active=true;
    }

    public void click()
    {
        if(active==true)
        {
            
            clicked=true;
        }
    }
}
