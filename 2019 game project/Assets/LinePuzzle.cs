using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePuzzle : MonoBehaviour
{
    public float lineLength = 20f;
    public float ringCentre = 4f;
    public GameObject[] connections;
    public Material lineMaterial;
    public Color lineColour = new Color(1,1,1,0.5f);
    public float lineWidth;

    private LineRenderer line;
    private float distance;




    private void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.positionCount = connections.Length;
        line.material = lineMaterial;
        line.material.color = lineColour;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
}

    private void Update()
    {
        settingLinePositionsAndRaycasts();
    }

    public void settingLinePositionsAndRaycasts()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            Transform connectionPoint = connections[i].transform;
            line.SetPosition(i, connectionPoint.transform.position);

            Physics.Raycast(connections[i].transform.position, connections[i++].transform.position - connections[i].transform.position);
        }
    }
}
