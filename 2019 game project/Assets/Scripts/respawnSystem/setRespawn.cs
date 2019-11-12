using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class setRespawn : MonoBehaviour
{

    public GameObject diamond;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") { other.GetComponent<RespawnSystem>().currentRespawnLocation = transform; }
        diamond.GetComponent<diamondSpin>().rotation = new Vector3(0.25f,0.5f,0.25f);

        diamond.GetComponent<Light>().intensity = 0;
        diamond.GetComponent<Light>().color = new Color(1,1,1);
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody inst = diamond.GetComponent<Rigidbody>();

        if (inst == null)
        {
            diamond.AddComponent<Rigidbody>();
            diamond.GetComponent<diamondSpin>().enabled = false;
            diamond.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        }
    }

}
