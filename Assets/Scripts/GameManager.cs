using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //makes the game work

    //prefab things housing every enemy
    public GameObject[] allTreasureSets;
    public GameObject[] enemySets;

    public bool isGameActive;

    //current treasure prefab in play
    private GameObject currentSet, currentEnemies, titleScreen, gameOverScreen;

    public GameObject player;
    //idk
    static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        //no need for publics cluttering gamemanager
        titleScreen = GameObject.Find("Title Screen");
        gameOverScreen = GameObject.Find("Game Over Screen");
        gameOverScreen.gameObject.SetActive(false);
    }


    public void StartGame()
    {
        isGameActive = true;
        SpawnTreasures();
        SpawnEnemies();
        titleScreen.gameObject.SetActive(false);
        Instantiate(player, GameObject.Find("Drill").transform.position, Quaternion.identity);
        
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
        Destroy(currentEnemies);
        Destroy(currentSet);
    }

    public static void Escape()
    {

    }

    public static void SpawnEnemies()
    {
        instance.StartCoroutine(instance.EnemyTime());
    }
    public static void SpawnTreasures()
    {
        instance.StartCoroutine(instance.TreasureTime());
    }
    
    private IEnumerator EnemyTime()
    {
        if(currentEnemies != null)
            Destroy(currentEnemies);

        int i = Random.Range(0, enemySets.Length);

        yield return currentEnemies = Instantiate(enemySets[i], new Vector2(0,0), Quaternion.identity); 
    }

    private IEnumerator TreasureTime()
    {
        if(currentSet != null)
            Destroy(currentSet);

        int i = Random.Range(0, allTreasureSets.Length);

        //yield return new WaitForSeconds(3);
        //idk maybe add something if do replayability

        yield return currentSet = Instantiate(allTreasureSets[i], new Vector2(0,0), Quaternion.identity);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

