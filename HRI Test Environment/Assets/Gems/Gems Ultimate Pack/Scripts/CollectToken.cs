using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectToken : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    float t;

    void OnTriggerEnter (Collider other)
    {

        if (other.gameObject.name == "HumanAgent")
        {
            t = 0;

            Debug.Log("Contact made...");
        } 
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "HumanAgent")
        {
            t += Time.deltaTime;
            if(t > 5)
            {
                Destroy(this.gameObject);
                Debug.Log("Object Destroyed...");
                //ScoringSystem.theScore += 50;

            }
        }
    }
}
