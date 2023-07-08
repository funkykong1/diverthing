using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMaster : MonoBehaviour
{
    public static List<GameObject> allItems = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Treasure"))
            allItems.Add(go);
    }
}
