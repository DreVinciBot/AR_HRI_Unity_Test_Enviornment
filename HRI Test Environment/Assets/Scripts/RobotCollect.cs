using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RobotCollect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] collectibles;
    public Vector3[] positions;
    private int destPoint;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        collectibles = GameObject.FindGameObjectsWithTag("R_Collectible");
        positions = new Vector3[collectibles.Length];
        for (int i = 0; i < collectibles.Length; i++)
        {
            positions[i] = collectibles[i].transform.position;
        }
        destPoint = 0;
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (destPoint >= positions.Length)
        {
            agent.isStopped = true;
            return;
        }
        agent.destination = positions[destPoint];
        destPoint++;
    }
}
