using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("The player entered the trigger area.");

        GameObject Player = GameObject.Find("Player");
        Player.

        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
