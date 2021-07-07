using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueGenerator : MonoBehaviour
{
    public  Text dialogueText;
    public dialogue[] dialogues;
     public void loaddialogue(string name)
    {
        dialogue currentDialogue=Array.Find(dialogues,dialogue=>dialogue.Name==name);
        StopAllCoroutines();
        StartCoroutine(typeSentence(currentDialogue));


    }
    IEnumerator typeSentence(dialogue currentDialogue)
    {
        dialogueText.text="";
        foreach(char letter in currentDialogue.sentence.ToCharArray())
        {
            dialogueText.text+=letter;
            yield return null;

        }
        yield return new WaitForSeconds(2f);
        dialogueText.text="";



    }
}
