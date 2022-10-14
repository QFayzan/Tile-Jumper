using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    public UnityEvent buttonClick;
    private void Awake()
    {
        if (buttonClick == null) { buttonClick = new UnityEvent(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Characters.jumpTimer > 3.0f)
        {
            this.gameObject.SetActive(false);
        }
        else if (Characters.jumpTimer < 3.0f)
        {
            this.gameObject.SetActive(true);
        }
    }
    
    
}
