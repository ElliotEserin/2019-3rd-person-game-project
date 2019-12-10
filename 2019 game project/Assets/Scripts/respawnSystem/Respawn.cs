using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPointPosition;
    GameObject player;
    CharacterController controller;
    GameObject[] fadingPlatforms;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CharacterController>();
        fadingPlatforms = GameObject.FindGameObjectsWithTag("fadingPlatform");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {            
            controller.enabled = false;
            other.transform.position = other.GetComponent<RespawnSystem>().currentRespawnLocation.position;
            controller.enabled = true;

            for (int i = 0; i < fadingPlatforms.Length; i++)
            {
                fadingPlatforms[i].SetActive(true);
                var renderer = fadingPlatforms[i].GetComponent<fadingPlatform>();
                renderer.colour.a = 1;
                renderer.render.material.color = renderer.colour;
            }

             
        }
        else
        {
            Destroy(other.gameObject);
        }
        
    }


}
