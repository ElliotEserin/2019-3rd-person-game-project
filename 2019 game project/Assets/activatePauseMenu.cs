using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePauseMenu : MonoBehaviour
{
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(!(canvas.activeInHierarchy));

            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            
        }
    }
}
