using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public Canvas DialogueCanvas;

    void Start()
    {
        DialogueCanvas = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
        DialogueCanvas.enabled = false;
    }

    public void OnTriggerEnter(Collider other) //showing the dialogue UI
    {
        DialogueCanvas.enabled = true;
        TriggerDialogue();
    }

    public void OnTriggerStay(Collider other) //pressing C to go to the next section of dialogue
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    public void OnTriggerExit(Collider other) //hiding the dialogue UI
    {
        DialogueCanvas.enabled = false;
        Destroy(this,0f);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
