using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        controller.enabled = false;
        player.transform.position = respawnPoint.transform.position;
        controller.enabled = true;
    }


}
