using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondSpin : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject diamond;
    public Vector3 rotation = new Vector3(2f, 2f, 2f);

    // Update is called once per frame
    void Update()
    {
        diamond.transform.Rotate(rotation, Space.Self);
    }
}
