using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : Characters
{
    public TextMeshProUGUI ScoreToGet;
    public GameObject jumpButton;
    public GameObject jumpInstruction;
    public GameObject pedestal;
    [SerializeField]int endgame = 0;
    [SerializeField]float endgame2 = 0;
    [SerializeField]public static int tilesDestroyed = 0;
    public static int  tilesToDestroy = 0;
    public bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        endgame = StageSpawner.totalTiles;
        endgame2 = endgame * (int)0.6f;
        Destroy(pedestal, 4.0f);
    }


    // Update is called once per frame
    void Update()
    {
        if (Characters.jumpTimer > 3.0f)
        {
            jumpButton.SetActive(true);
            jumpInstruction.SetActive(true);
        }
        else if (Characters.jumpTimer < 3.0f)
        {
            jumpButton.SetActive(false);
            jumpInstruction.SetActive(false);
        }
        //ShowTilesToDestroy();
        if (tilesDestroyed > endgame2)
        {
            NextStage();
        }

    }
    //gameplay loop is complete
    void NextStage()     
    {
       endgame = StageSpawner.totalTiles;
       endgame2 = endgame * (0.60f);
        Mathf.RoundToInt(endgame2);
        if (tilesDestroyed > (int)endgame2)
        {
            
            StageSpawner.stageLength++;
            BounceUp(GameObject.FindGameObjectWithTag("Player"));
            tilesDestroyed = 0;
            endgame = 0;
            endgame2 = 0;
        }
    }
    //ShowReducing nneds work
    /*void ShowTilesToDestroy()
    {
        tilesToDestroy = endgame - (int)endgame2; //needs work right here
        ScoreToGet.text = "Tiles to Destroy :  " + Mathf.RoundToInt(tilesToDestroy).ToString();
    }*/

}
