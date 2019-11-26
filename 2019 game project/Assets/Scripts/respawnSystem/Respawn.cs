using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPointPosition;
    CharacterController controller;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {            
            controller.enabled = false;
            other.transform.position = other.GetComponent<RespawnSystem>().currentRespawnLocation.position;
            controller.enabled = true;
        }
        else
        {
            Destroy(other.gameObject);
        }
        
    }


}
