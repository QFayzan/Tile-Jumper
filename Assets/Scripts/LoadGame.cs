using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    float timer = 5;
    // Start is called before the first frame update
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            //SceneManager.LoadScene(Main);
        }
    }
}
