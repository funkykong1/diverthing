using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //prefab things housing every enemy
    public GameObject[] allTreasureSets;
    public GameObject[] enemySets;

    //how many treasures collected
    public int tally;

    public bool isGameActive;

    //need to be public. ui text
    public TextMeshProUGUI scoreText, highScoreText, gameOverText;

    //timer which acts as a score in the end
    public float gameTimer;

    public Button restartButton;

    //current treasure prefab in play
    private GameObject currentSet, currentEnemies, titleScreen;


    //idk
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


    }

    void Start()
    {
        titleScreen = GameObject.Find("Title Screen");
    }

    void Update()
    {

    }

    public void StartGame()
    {
        isGameActive = true;
        //SpawnTreasures();
        //SpawnEnemies();
        titleScreen.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        isGameActive = false;

        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
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

