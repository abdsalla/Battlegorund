using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    public Text scoreText;
    public Text livesText;
    public int scoreNum;

    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreNum == 1)
        {
            scoreText.text = "P1 Score: " + instance.score1.ToString();
            livesText.text = "P1 Lives Left: " + instance.lives1.ToString();
        }
        else if (scoreNum == 2)
        {
            scoreText.text = "P2 Score: " + instance.score2.ToString();
            livesText.text = "P2 Lives Left: " + instance.lives2.ToString();
        }
    }
}
