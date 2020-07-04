using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AdvancedCollect : MonoBehaviour
{
    // Start is called before the first frame update
    const float DIAMETER = 1f;
    public GameObject[] collectibles, markers;
    public GameObject marker;
    public Vector3[] positions, sequence;
    private int destPoint, pathProgress;
    private float destAngle, rotSpeed;
    private NavMeshAgent agent;
    private bool facing, targetAcquired, plotted;
    void Start()
    {
        facing = false;
        targetAcquired = false;
        rotSpeed = 50f;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        collectibles = GameObject.FindGameObjectsWithTag("R_Collectible");
        positions = new Vector3[collectibles.Length];
        for (int i = 0; i < collectibles.Length; i++)
        {
            positions[i] = collectibles[i].transform.position;
        }
        destPoint = 0;
        pathProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetAcquired)
        {
            acquireTarget();
        }
        if (facing)
            GoToNextPoint();
        else
        {
            Vector3 destVector = positions[destPoint] - transform.position;
            destAngle = Vector3.SignedAngle(this.transform.forward, positions[destPoint] - transform.position, Vector3.up);
            //if (destAngle > -45f && destAngle < 45f)
            //    noRotate(destAngle);
            //else
                facing = rotation(destAngle);
        }

        
    }
    void acquireTarget()
    {
        GameObject[] oldMarkers = GameObject.FindGameObjectsWithTag("Marker");
        foreach(GameObject marker in oldMarkers)
        {
            Destroy(marker);
        }
        agent.ResetPath();
        if (destPoint == positions.Length - 1) return;
        destPoint++;
        targetAcquired = true;
    }

    void GoToNextPoint()
    {
        if (destPoint >= positions.Length)
        {
            agent.isStopped = true;
            return;
        }
        if (!plotted)
        {
            plotPath();
        }
        else
        {
            if(pathProgress == sequence.Length)
            {
                agent.destination = positions[destPoint];
                if (Vector3.Distance(transform.position, positions[destPoint]) < 0.5f)
                {
                    targetAcquired = false;
                    facing = false;
                    plotted = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, sequence[pathProgress]) < 0.5f)
                {
                    Destroy(markers[pathProgress]);
                    pathProgress++;
                    agent.destination = sequence[pathProgress];
                }

            }

        }
        
    }
    bool rotation(float angle)
    {
        if (Mathf.Abs(destAngle) < 5) return true;
        float preAngle, angleChange;
        angleChange = rotSpeed * Time.deltaTime;
        if (angle < 0) angleChange *= -1;
        preAngle = angle;
        angle -= angleChange;
        this.transform.Rotate(0, angleChange, 0);
        return false;
    }
    void noRotate(float angle)
    {

    }
    void plotPath()
    {
        float distance = Vector3.Distance(positions[destPoint], transform.position);
        sequence = new Vector3[(int) Mathf.Floor(distance / DIAMETER)];
        markers = new GameObject[(int)Mathf.Floor(distance / DIAMETER)];
        sequence[0] = transform.position;
        Vector3 direction = (positions[destPoint] - transform.position) / distance;
        for (int i = 1; i < sequence.Length; i++)
        {
            sequence[i] = (direction * (DIAMETER * i)) + transform.position;
            markers[i] = Instantiate(marker);
            markers[i].transform.position = sequence[i];
        }
        plotted = true;
        pathProgress = 0;
    }
   
}
