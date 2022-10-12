using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreReached;
    public TextMeshProUGUI roundsReached;
    public TextMeshProUGUI highestScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreReached.text = " Your Score Was : " + ScoreManager.score;
        roundsReached.text = "You completed : " + StageSpawner.roundNumber + " Rounds";
        highestScore.text = "All time high score is : " + ScoreManager.highScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
