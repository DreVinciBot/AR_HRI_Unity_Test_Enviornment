using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CollectToken : MonoBehaviour
{
    public float duration;
    public Image fillImage;
    public GameObject gemPrefab;
    //public Transform spawnPoint;
    public float t;
    public Vector3 spawnPoint;
    public int randXPosition;
    public int randZPosition;

    public void spawnGem()
    {
        GameObject a = Instantiate(gemPrefab)as GameObject;
        randXPosition = Random.Range(-2, 12);
        randZPosition = Random.Range(22,36);
        spawnPoint = new Vector3(randXPosition, 1.2f, randZPosition);
        a.GetComponent<AnimationScript>().enabled = true;
        a.transform.position = spawnPoint;
        a.GetComponent<NavMeshObstacle>().enabled = true;

    }

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
                spawnGem();
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
