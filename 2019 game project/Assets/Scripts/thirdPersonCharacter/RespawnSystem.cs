using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public Transform currentRespawnLocation;

    private void Start()
    {
        currentRespawnLocation = GameObject.Find("setRespawnTriggerOne").transform;
    }
}
