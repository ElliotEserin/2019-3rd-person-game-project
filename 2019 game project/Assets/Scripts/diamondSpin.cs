using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondSpin : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject diamond;
    public Vector3 rotation;

    void Start()
    {
        rotation = new Vector3(0, 2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        diamond.transform.Rotate(rotation, Space.Self);
    }
}
