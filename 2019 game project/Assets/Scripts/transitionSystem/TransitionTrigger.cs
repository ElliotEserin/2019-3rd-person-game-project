using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionTrigger : MonoBehaviour
{

    public Canvas TransitionCanvas;
    public Vector3 CoordinatesToGoTo;
    public bool GoToNewScene = false;
    public string SceneToGoTo;
    public string LocationName;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        TransitionCanvas.enabled = false;
        animator = GameObject.FindGameObjectWithTag("fade").GetComponent<Animator>();
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
            OnTransition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TransitionCanvas.enabled = false;
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("fadeOut");
    }

    public void OnTransition()
    {
        if (GoToNewScene) //load new scene
        {
            FadeToLevel();
            SceneManager.LoadScene(SceneToGoTo);
            FindObjectOfType<TransitionManager>().TranslatePlayer(CoordinatesToGoTo);

            TransitionCanvas.enabled = false;
        }
        else //translate player in current scene
        {

            FindObjectOfType<TransitionManager>().TranslatePlayer(CoordinatesToGoTo);

            TransitionCanvas.enabled = false;
        }
    }

}
