using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFallScript : MonoBehaviour
{

    public GameObject rock;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        rock.AddComponent<Rigidbody>();
    }
}
