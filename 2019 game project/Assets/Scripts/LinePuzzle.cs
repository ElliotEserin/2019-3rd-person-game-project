using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePuzzle : MonoBehaviour
{
    public float lineLength = 20f;
    public float ringCentre = 4f;
    public float lineWidth = 0.1f;
    public GameObject emitter;
    public GameObject reciever;
    public GameObject[] affectedObjects;
    public Material lineMaterial;
    public Color lineColour = new Color(1, 1, 1, 0.5f);

    private bool isBlocked = false;
    private bool wasBlocked = false;
    private LineRenderer line;




    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.material = lineMaterial;
        line.material.color = lineColour;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
}

    private void Update()
    {
        Raycasts();
    }

    public void Raycasts()
    {
        float range;
        RaycastHit hit = new RaycastHit();

        line.SetPosition(0, emitter.transform.position);

        range = Vector3.Distance(emitter.transform.position, reciever.transform.position);
        Physics.Raycast(emitter.transform.position, (reciever.transform.position - emitter.transform.position), out hit, range);

        if(hit.collider != null)
        {
            line.SetPosition(1, hit.point);

            wasBlocked = isBlocked;
            isBlocked = true;

            if (wasBlocked == false)
            {
                offEffect();
            }
        }
        else
        {
            line.SetPosition(1, reciever.transform.position);

            wasBlocked = isBlocked;
            isBlocked = false;

            if (wasBlocked == true)
            {
                onEffect();
            }
        }

    }

    public void onEffect()
    {
        for(int i = 0; i < affectedObjects.Length; i++)
        {
            affectedObjects[i].SetActive(true);
        }
    }

    public void offEffect()
    {
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            affectedObjects[i].SetActive(false);
        }
    }
}
