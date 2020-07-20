using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AdvancedCollect : MonoBehaviour
{
    // Start is called before the first frame update
    const float DIAMETER = 1f, CURVE_ANGLE = 5f;
    const int PER_PATH = 5;
    public GameObject[] collectibles, markers;
    public GameObject marker;
    public Vector3[] positions, sequence, curvePath;
    private int destPoint, pathProgress, curveNum;
    private float destAngle, rotSpeed;
    private NavMeshAgent agent;
    private bool facing, targetAcquired, plotted, curvePlotted;
    void Start()
    {
        facing = false;
        targetAcquired = false;
        rotSpeed = 50f;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        collectibles = GameObject.FindGameObjectsWithTag("R_Collectible");
        positions = new Vector3[collectibles.Length];
        bool[] used = new bool[collectibles.Length];
        for (int i = 0; i < collectibles.Length; i++)
        {
            int nextPos = (int) Random.Range(0f, used.Length - 1);
            while (used[nextPos])
            {
                nextPos = (int)Random.Range(0f, used.Length - 1);
            }
            positions[nextPos] = collectibles[i].transform.position;
            used[nextPos] = true;
        }
        destPoint = -1;
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
           // if ((destAngle > -60f && destAngle < 60f) && Vector3.Distance(transform.position, destVector) > 10)
            //    noRotate(destAngle);
            //else
                facing = rotation(destAngle);
        }

        
    }
    void acquireTarget()
    {
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
            plotPath(transform.position);
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
                    curvePlotted = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, sequence[pathProgress]) < 0.5f)
                {
                    Destroy(markers[pathProgress]);
                    pathProgress++;
                    if (pathProgress != sequence.Length) agent.destination = sequence[pathProgress];
                    progressPath();
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
        curveNum = (int) Mathf.Floor(angle / CURVE_ANGLE);
        curvePath = new Vector3[curveNum];
        for(int i = 0; i < curveNum; i++)
        {
            
        }
        curvePlotted = true;
    }
    void plotPath(Vector3 startPoint)
    {
        float distance = Vector3.Distance(positions[destPoint], startPoint);
        sequence = new Vector3[(int) Mathf.Ceil(distance / DIAMETER)];
        markers = new GameObject[(int)Mathf.Ceil(distance / DIAMETER)];
        sequence[0] = transform.position;
        Vector3 direction = (positions[destPoint] - startPoint) / distance; 
        for (int i = 1; i < sequence.Length; i++)
        {
            sequence[i] = (direction * (DIAMETER * i)) + transform.position;
            markers[i] = Instantiate(marker);
            markers[i].transform.position = sequence[i];
            markers[i].SetActive(false);
        }
        plotted = true;
        pathProgress = 0;
    }
    void progressPath()
    {
        int toProject = pathProgress + PER_PATH;
        for(int i = pathProgress; i < toProject; i++)
        {
            if (toProject >= markers.Length) break;
            markers[i].SetActive(true);
        }
    }
}
