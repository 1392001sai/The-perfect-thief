using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour
{
     public Animator fadeout;
     public float waittime;
     public int buildIndex;
     bool fade;
    // Start is called before the first frame update

    private void Start() {
        fade=false;
    }
    void Update()
    {
        if(fade==false&&Input.anyKey==true)
        {
            StartCoroutine(nextLevel());
            fade=false;
        }
    }

    IEnumerator nextLevel()
    {
        //yield return new WaitForSeconds(3);
        fadeout.SetBool("fadeout",true);
        yield return new WaitForSeconds(waittime);
        SceneManager.LoadScene(buildIndex);


    }
}
