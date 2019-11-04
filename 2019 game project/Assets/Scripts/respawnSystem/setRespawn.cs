using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawn : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<RespawnSystem>().currentRespawnLocation = transform;

    }

}
