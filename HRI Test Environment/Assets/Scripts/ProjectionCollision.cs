using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionCollision : MonoBehaviour
{
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.FindGameObjectWithTag("Robot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Human")
        {
            robot.GetComponent<AdvancedCollect>().obstructed = true;
            robot.GetComponent<AdvancedCollect>().obstructedPath(transform.position);
        }
    }
}
