using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoorTrigger : MonoBehaviour
{
    bool loaded;

    private void Start() {
        loaded=false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"&&loaded==false)
        {
            FindObjectOfType<DialogueGenerator>().loaddialogue("magic door");
            loaded=true;
        }
        
    }
}
