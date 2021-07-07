using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
       bool loaded;
       public string Dialogue;

    private void Start() {
        loaded=false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"&&loaded==false)
        {
            FindObjectOfType<DialogueGenerator>().loaddialogue(Dialogue);
            loaded=true;
        }
        
    }
}
