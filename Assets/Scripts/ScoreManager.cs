using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static float score = 0;
    public static int highScore = 0;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        score = 0; //must be hard coded as 0 in start since gameplay is supposed to loop to restart
    }
    void Update()
    {
        scoreText.text = "Score : " + score.ToString();
        //Debug.Log(score);
        if (score > highScore)
        {
            highScore = (int)score;
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
    private void OnEnable()
    {
        PlayerPrefs.GetInt("HighScore");
    }

}