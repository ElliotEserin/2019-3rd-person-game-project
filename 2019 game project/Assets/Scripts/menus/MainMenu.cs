using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("fade").GetComponent<Animator>();
    }
    public void PlayGame()
    {
        animator.SetTrigger("fadeOut");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
