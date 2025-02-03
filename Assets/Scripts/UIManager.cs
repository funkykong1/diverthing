using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //does ui stuff

    private Color32 active = new Color(1,1,1,1);
    private Color32 inactive = new Color(1,1,1,0.25f);
    
    public Image[] lifeSprites;
    public Sprite[] scoreSprites;
    public Image scoreUI, goalUI;
    public int score, goal;

    private GameObject leaveText;
    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        leaveText = GameObject.Find("Leave Text");
        
    }

    private void Start()
    {
        score = 0;
        goal = 7;
        
        instance.goalUI.sprite = instance.scoreSprites[goal];

        leaveText.SetActive(false);
    }

    public static void UpdateLives (int l)
    {
        foreach(Image i in instance.lifeSprites)
            i.color = instance.inactive;

        for (int i = 0; i < l; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }
    }

    public static void UpdateScore (int s)
    {
        if(instance.score >= 9)
            return;
        //update scoretext
        instance.score += s;
        if(instance.scoreSprites[instance.score] != null)
            instance.scoreUI.sprite = instance.scoreSprites[instance.score];
        

        //if sufficient goodies collected, let player escape
        if (instance.score == instance.goal)
        {
            GameManager.Escape();
            instance.StartCoroutine(instance.ShowText());
        }
    }

    //shows instructional text for 5 seconds
    public IEnumerator ShowText()
    {
        leaveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        leaveText.gameObject.SetActive(false);
    }

}
