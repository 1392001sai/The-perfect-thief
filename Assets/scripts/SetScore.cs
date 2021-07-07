using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("highscore",0)!=0)
        GetComponent<Text>().text=PlayerPrefs.GetInt("highscore").ToString()+" s";
        else
        GetComponent<Text>().text="--";
        
        
    }


}
