using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPointPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
            

        controller.enabled = false;
        other.transform.position = other.GetComponent<RespawnSystem>().currentRespawnLocation.position;
        controller.enabled = true;
        
    }


}
