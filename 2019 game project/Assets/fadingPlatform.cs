using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadingPlatform : MonoBehaviour
{
    public float alphaDecreaseRate = 0.5f;

    public MeshRenderer render;
    public Color colour;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        Debug.Log(render.gameObject);
        colour = render.material.color;
    }

    private void OnTriggerStay(Collider other)
    {  
        colour.a -= alphaDecreaseRate * Time.deltaTime;
        render.material.color = colour;
        Debug.Log("changed alpha to: " + colour.a);
        
        if(colour.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
