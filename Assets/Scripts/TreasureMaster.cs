using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMaster : MonoBehaviour
{

    //thing is updated via Treasure.cs
    public static List<GameObject> allItems = new List<GameObject>();



    void Start()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Treasure"))
            allItems.Add(go);
    }
}
