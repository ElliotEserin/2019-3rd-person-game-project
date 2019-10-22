using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public void TranslatePlayer(Vector3 Coords)
    {
        GameObject player = GameObject.FindWithTag("Player");

        if(player == null)
        {
            Debug.Log("Player does not exist");
        }
        else
        {
            CharacterController controller = player.GetComponent<CharacterController>();

            controller.enabled = false;
            player.transform.position = Coords;
            controller.enabled = true;

            Debug.Log("The player has had its position transformed" + player.transform.position);
        }
    }

    public void TranslatePlayer(GameObject TranslatePoint)
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.Log("Player does not exist");
        }
        else
        {
            CharacterController controller = player.GetComponent<CharacterController>();

            controller.enabled = false;
            player.transform.position = TranslatePoint.transform.position;
            controller.enabled = true;

            Debug.Log("The player has had its position transformed" + player.transform.position);
        }
    }
}
