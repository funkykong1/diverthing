using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
public void DestroyThis()
    {
        //animation event for removing pickup sparkle
        Destroy(gameObject);
    }
}
