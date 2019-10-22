using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionTrigger : MonoBehaviour
{

    public Canvas TransitionCanvas;
    public Vector3 CoordinatesToGoTo;
    public string LocationName;

    // Start is called before the first frame update
    void Start()
    {
        TransitionCanvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        TransitionCanvas.enabled = true;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        GameObject.Find("transitionButton").GetComponentInChildren<Text>().text = ("Go to: " +  LocationName);

        if(Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<TransitionManager>().TranslatePlayer(CoordinatesToGoTo);

            TransitionCanvas.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TransitionCanvas.enabled = false;
    }

}
