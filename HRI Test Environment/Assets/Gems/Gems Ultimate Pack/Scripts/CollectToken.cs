using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectToken : MonoBehaviour
{
    public float duration;
    public Image fillImage;

    public float t;



    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "HumanAgent")
        {
            t = 0;
            fillImage.fillAmount = 0;
            //Debug.Log("Contact made...");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "HumanAgent")
        {
            StartCoroutine(Timer(duration));

            t += Time.deltaTime;
            if(t > 5)
            {
                Destroy(this.gameObject);
                fillImage.fillAmount = 0;
                //Debug.Log("Object Destroyed...");
                //ScoringSystem.theScore += 50;

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HumanAgent")
        {
            t = 0;
            fillImage.fillAmount = 0;
        }
    }

    public IEnumerator Timer(float duration)
    {
        float startTime = Time.time;
        float time = duration;
        float value = 0;

        while (Time.time - startTime < duration)
        {
            //time += Time.deltaTime;
            value = t / duration;
            fillImage.fillAmount = value;
            yield return null;
        }
    }
}
