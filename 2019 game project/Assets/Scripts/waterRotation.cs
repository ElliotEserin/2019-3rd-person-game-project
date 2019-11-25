using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRotation : MonoBehaviour
{
    public GameObject water;
    public Vector3 angle;

    void Start()
    {
        water = GameObject.FindGameObjectWithTag("water");
        angle = new Vector3(0, 0.15f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (water == null)
            Debug.Log("object not found");

        else
            water.transform.Rotate(angle, Space.Self);
    }
}
