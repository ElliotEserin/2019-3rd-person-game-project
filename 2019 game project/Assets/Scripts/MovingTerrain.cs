
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    public bool moveOnX = false;
    public bool flippedX = false;
    public float xMax;
    public bool moveOnY = false;
    public bool flippedY = false;
    public float yMax;
    public bool moveOnZ = false;
    public bool flippedZ = false;
    public float zMax;
    public float moveSpeed;
    public bool rotation;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float w = 1f;
    public Vector3 originalPos;

    int xDirection = -1;
    int yDirection = -1;
    int zDirection = -1;


    void Start()
    {
        originalPos = transform.localPosition;
        if (flippedX == true)
        {
            xDirection = 1;
        }
        if (flippedY == true)
        {
            yDirection = 1;
        }
        if (flippedZ == true)
        {
            zDirection = 1;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (moveOnX)
            moveX();
        if (moveOnY)
            moveY();
        if (moveOnZ)
            moveZ();
        if (rotation)
            rotate();
    }

    void moveX()
    {
        if (xDirection == 1)
        {
            transform.localPosition = transform.localPosition + new Vector3(moveSpeed, 0, 0);

            if (transform.localPosition.x >= originalPos.x + xMax)
            {
                xDirection = -1;
            }
        }
        else if (xDirection == -1)
        {
            transform.localPosition = transform.localPosition - new Vector3(moveSpeed, 0, 0);

            if (transform.localPosition.x <= originalPos.x - xMax)
            {
                xDirection = 1;
            }
        }
    }

    void moveY()
    {
        if (yDirection == 1)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, moveSpeed, 0);

            if (transform.localPosition.y >= originalPos.y + yMax)
            {
                yDirection = -1;
            }
        }
        else if (yDirection == -1)
        {
            transform.localPosition = transform.localPosition - new Vector3(0, moveSpeed, 0);

            if (transform.localPosition.y <= originalPos.y - yMax)
            {
                yDirection = 1;
            }
        }
    }

    void moveZ()
    {
        if (zDirection == 1)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, 0, moveSpeed);

            if (transform.localPosition.z >= originalPos.z + zMax)
            {
                zDirection = -1;
            }
        }
        else if (zDirection == -1)
        {
            transform.localPosition = transform.localPosition - new Vector3(0, 0, moveSpeed);

            if (transform.localPosition.z <= originalPos.z - zMax)
            {
                zDirection = 1;
            }
        }
    }

    void rotate()
    {
        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ), Space.Self);
    }
}
