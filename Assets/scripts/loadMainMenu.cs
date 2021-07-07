using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMainMenu : MonoBehaviour
{
    public LoadNewLevel loadNewLevel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(loadNewLevel.LoadLevel(1));
    }


}
