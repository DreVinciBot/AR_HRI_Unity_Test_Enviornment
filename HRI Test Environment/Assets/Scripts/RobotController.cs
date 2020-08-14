
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public Transform player;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Move the Robot with mouse click
                agent.SetDestination(hit.point);
            }
        }

        //just have the robot follow the player
        agent.SetDestination(player.position);

    }
}
// =======
// using UnityEngine;
// using UnityEngine.AI;
//
// public class RobotController : MonoBehaviour
// {
//     public Camera cam;
//
//     public NavMeshAgent agent;
//
//     // Update is called once per frame
//     void Update()
//     {
//         if(Input.GetMouseButtonDown(0))
//         {
//                 Ray ray = cam.ScreenPointToRay(Input.mousePosition);
//                 RaycastHit hit;
//
//                 if(Physics.Raycast(ray, out hit))
//                 {
//                         agent.SetDestination(hit.point);
//                 }
//         }
//     }
// }
// >>>>>>> Stashed changes
