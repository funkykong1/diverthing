using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

public GameObject[] allTreasureSets;

//current treasure prefab in play
private GameObject currentSet;

//idk
private static GameManager instance;

private void Awake()
{
    if (instance == null)
        instance = this;
    else
        Destroy(gameObject);
}

    private void Start()
    {
        SpawnTreasures();
    }

    public static void SpawnTreasures()
    {
        instance.StartCoroutine(instance.TreasureTime());
    }

    private IEnumerator TreasureTime()
    {
        if(currentSet != null)
            Destroy(currentSet);

        
        yield return new WaitForSeconds(3);
        //idk maybe add something if do replayability

        currentSet = Instantiate(allTreasureSets[0], new Vector2(0,0), Quaternion.identity);
    }
}

