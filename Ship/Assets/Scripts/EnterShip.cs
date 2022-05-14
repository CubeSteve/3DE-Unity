using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShip : MonoBehaviour
{
    public GameObject enterText;
    public bool inBox = false;

    //When object enters collider
    private void OnTriggerEnter(Collider other)
    {
        //When object is the player
        if (other.tag == "Player")
        {
            //Show text
            enterText.SetActive(true);
            inBox = true;
        }
    }

    //When object exits collider
    private void OnTriggerExit(Collider other)
    {
        //When object is the player
        if (other.tag == "Player")
        {
            //Hide text
            enterText.SetActive(false);
            inBox = false;
        }
    }
}
