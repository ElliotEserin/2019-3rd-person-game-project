
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    public bool moveOnX = false;
    public float xMax;
    public bool moveOnY = false;
    public float yMax;
    public bool moveOnZ = false;
    public float zMax;
    public float moveSpeed;
    public Vector3 originalPos;

    int xDirection = -1;
    int yDirection = -1;
    int zDirection = -1;


    void Start()
    {
        originalPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if(moveOnX)
            moveX();
        if(moveOnY)
            moveY();
        if(moveOnZ)
            moveZ();
    }

    void moveX()
    {
        if (xDirection == 1)
        {
            transform.position = transform.position + new Vector3(moveSpeed, 0, 0);

            if(transform.position.x >= originalPos.x + xMax)
            {
                xDirection = -1;
            }
        }
        else if (xDirection == -1)
        {
            transform.position = transform.position - new Vector3(moveSpeed, 0, 0);

            if (transform.position.x <= originalPos.x - xMax)
            {
                xDirection = 1;
            }
        }
    }

    void moveY()
    {
        if (yDirection == 1)
        {
            transform.position = transform.position + new Vector3(0, moveSpeed, 0);

            if (transform.position.y >= originalPos.y + yMax)
            {
                yDirection = -1;
            }
        }
        else if (yDirection == -1)
        {
            transform.position = transform.position - new Vector3(0, moveSpeed, 0);

            if (transform.position.y <= originalPos.y - yMax)
            {
                yDirection = 1;
            }
        }
    }

    void moveZ()
    {
        if (zDirection == 1)
        {
            transform.position = transform.position + new Vector3(0, 0, moveSpeed);

            if (transform.position.z >= originalPos.z + zMax)
            {
                zDirection = -1;
            }
        }
        else if (zDirection == -1)
        {
            transform.position = transform.position - new Vector3(0, 0, moveSpeed);

            if (transform.position.z <= originalPos.z - zMax)
            {
                zDirection = 1;
            }
        }
    }
}
