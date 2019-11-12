using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePuzzle : MonoBehaviour
{
    LineRenderer line;
    Vector3 currentPosition;
    Vector3 endPosition;
    public float lineLength = 20f;
    public float ringCentre = 4f;

    private void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.5f;
        line.endWidth = 0f;
        line.positionCount = 2;
    }

    private void Update()
    {
        currentPosition = new Vector3(transform.position.x, transform.position.y + ringCentre, transform.position.z);
        endPosition = new Vector3(transform.position.x + lineLength, transform.position.y + ringCentre, transform.position.z);
        line.SetPosition(0, currentPosition);
        line.SetPosition(1, endPosition);
    }
}
