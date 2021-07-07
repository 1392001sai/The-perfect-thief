using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toBeContinuedScript : MonoBehaviour
{
    public Animator fadeout;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mainMenu());
        
    }

    IEnumerator mainMenu()
    {
        yield return new WaitForSeconds(3);
        fadeout.SetBool("fadeout",true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);


    }

  
}
