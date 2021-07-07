using UnityEngine;

public class Buttons : MonoBehaviour
{
    
    public void OnButtonClick()
    {
        if(gameObject.name=="PlayButton")
        {
            StartCoroutine(FindObjectOfType<LoadNewLevel>().LoadLevel());

        }
        if(gameObject.name=="QuitButton")
        {
            StartCoroutine(FindObjectOfType<LoadNewLevel>().LoadLevel(-2));
        }

    }
    
}
