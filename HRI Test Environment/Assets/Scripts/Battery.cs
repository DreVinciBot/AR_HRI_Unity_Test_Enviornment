using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    // Start is called before the first frame update
    const float FULL_LEVEL = 100f, ENOUGH_JUICE = 0.5f;
    public GameObject home, levelIndicator;
    private float currLevel, distFromHome;
    
    void Start()
    {
        currLevel = FULL_LEVEL;
    }

    // Update is called once per frame
    void Update()
    {
        distFromHome = Vector3.Distance(this.transform.position, home.transform.position);    
        if(distFromHome / currLevel < ENOUGH_JUICE)
        {
            //TODO: implement GoHome() function
        }
        else
        {
            currLevel -= Time.deltaTime;
        }
    }
}
