using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileCode : MonoBehaviour
{
    //more types of tiles can be added here
    bool isExplodeTile = false;
    bool isJumpTile = false;
    bool isNormalTile = false;
    bool isHealthTile = false;
    bool isYellowTile = false;
    bool isGreenTile = false;
    bool greenTileSpawned = false;
    [SerializeField] private int i = 0;

    
    //when a tile object is initialized it will be assigned a color and function on startup

    void Start()
    {
        i = Random.Range(0,100);
        if (i<3)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
            isHealthTile = true;
            this.gameObject.name = "Magenta";
        }
        if (i > 3 && i < 15)
        { 
            //gameobject red
            GetComponent<Renderer>().material.color = Color.red;
            isExplodeTile = true;
            this.gameObject.name = "Red";
            //explode / game over property 
        }
        if (i>15 && i<30)
        {
            //gameobject blue
            GetComponent<Renderer>().material.color = Color.blue;
            isJumpTile = true;
            this.gameObject.name = "Blue";
            //Bounce the player
            //lauches player gently in the air then ends itself
        }
         if(i>30 && i<40)
        {
            //Yellow is HP -1 tile
            this.gameObject.name = "Yellow";
            GetComponent<Renderer>().material.color = Color.yellow;
            isYellowTile = true;
        }
         if (i >40)
        {
            //white is normal tile
            this.gameObject.name = "White";
            GetComponent<Renderer>().material.color = Color.white;
            isNormalTile = true;
        }
         if (i>80 && !greenTileSpawned)
        {
                //Green is stage clear tile
                this.gameObject.name = "Green";
                GetComponent<Renderer>().material.color = Color.green;
                isGreenTile = true;
                greenTileSpawned = true;
        }
        //more types can be added here
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isExplodeTile)
            {
                //Instant death
                other.gameObject.GetComponent<PlayerController>().Death();
                other.gameObject.GetComponent<PlayerController>().isDead = true;
                other.gameObject.GetComponent<PlayerController>().GameOver();
                Destroy(gameObject);
            }
            else if (isJumpTile)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 4, ForceMode.Impulse);
                other.gameObject.GetComponent<Animator>().SetBool("IsJumping", true);
                //other.transform.position = new Vector3(other.transform.position.x, 4, other.transform.position.z);
                FindObjectOfType<AudioManager>().Play("Jump");
                Destroy(gameObject);
                ScoreManager.score++;
                GameManager.tilesDestroyed++;
            }
            else if (isHealthTile)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 4, ForceMode.Impulse);
                other.gameObject.GetComponent<Animator>().SetBool("IsJumping", true);
                //other.transform.position = new Vector3(other.transform.position.x, 4, other.transform.position.z);
                FindObjectOfType<AudioManager>().Play("Jump");
                Destroy(gameObject);
                ScoreManager.score++;
                GameManager.tilesDestroyed++;
                other.gameObject.GetComponent<PlayerController>().health += 1;
                FindObjectOfType<AudioManager>().Play("HPup");
            }
            else if (isYellowTile)
                {
                    //Implement bounce and then destroy
                    other.gameObject.GetComponent<PlayerController>().health -= 1;
                    other.gameObject.GetComponent<PlayerController>().BounceUp(other.gameObject);
                    other.gameObject.GetComponent<Animator>().SetBool("IsJumping", true);
                    //Play Hurt Sound
                    FindObjectOfType<AudioManager>().Play("Hurt");
                    Destroy(gameObject);
                }
            else if (isGreenTile && greenTileSpawned)
            {
                StageSpawner.stageLength++;
                //BounceUp(GameObject.FindGameObjectWithTag("Player"));
                GameManager.tilesDestroyed = 0;
                greenTileSpawned = false;
            }

        }
    }
    void OnCollisionExit(Collision other)
    {
       if (other.gameObject.tag=="Player")
       {
            if (isNormalTile)
            {
                //Score++ when i implement it
                ScoreManager.score++;
                //tiles destroyed is used to show how many tiles are blown up
                Destroy(gameObject, 0.5f);
                GameManager.tilesDestroyed++;
                GameManager.tilesToDestroy--;
                FindObjectOfType<AudioManager>().Play("Score");
            }

        }
       
    }
}
