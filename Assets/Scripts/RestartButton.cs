using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public UnityEvent buttonClick;
    private void Awake()
    {
        if (buttonClick == null) { buttonClick = new UnityEvent(); }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
