using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private Color32 active = new Color(1,1,1,1);
    private Color32 inactive = new Color(1,1,1,0.25f);
    
    public Image[] lifeSprites;
    public Sprite[] scoreSprites;
    public Image scoreUI;
    public int score = 0;
    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
        instance.score += s;

        instance.scoreUI.sprite = instance.scoreSprites[instance.score];
    }
}
