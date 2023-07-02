using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform plr;

    // Update is called once per frame
    void Update()
    {
        transform.position = plr.transform.position + new Vector3(0, 0, -5);
    }
}
