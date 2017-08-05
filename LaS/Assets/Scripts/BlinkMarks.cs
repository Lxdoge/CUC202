
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkMarks : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Character2Ctrl2D>().blinkmarkon = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Character2Ctrl2D>().blinkmarkon = false;
    }
}


