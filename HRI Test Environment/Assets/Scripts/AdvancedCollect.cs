using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AdvancedCollect : MonoBehaviour
{
    // Start is called before the first frame update
    const float DIAMETER = 1f, CURVE_ANGLE = 3f, CURVE_DIAMETER = .3f;
    const int PER_PATH = 100;
    public GameObject[] collectibles, markers, curveMarkers;
    public GameObject marker, curveMarker, lArrow, rArrow;
    public Vector3 initPos;
    public Vector3[] positions, sequence, curvePath;
    private int destPoint, pathProgress, curveNum;
    private float initAngle, destAngle, rotSpeed;
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
            initAngle = Vector3.SignedAngle(this.transform.forward, positions[destPoint] - transform.position, Vector3.up);
        }
        if (facing || curvePlotted)
            GoToNextPoint();
        else
        {
            Vector3 destVector = positions[destPoint] - transform.position;
            destAngle = Vector3.SignedAngle(this.transform.forward, positions[destPoint] - transform.position, Vector3.up);
            if ((initAngle > -60f && initAngle < 60f) && Vector3.Distance(transform.position, destVector) > 30)
            {
                noRotate(destAngle);
            }
            else
            {
                facing = rotation(destAngle);
            }
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
        if (!plotted && !curvePlotted)
        {
            plotPath(transform.position, null);
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
        curveNum = (int) Mathf.Floor(Mathf.Abs(angle) / CURVE_ANGLE);
        curvePath = new Vector3[curveNum];
        curveMarkers = new GameObject[curveNum];
        int sign = 1;
        if (angle < 0) sign = -1;
        for(int i = 0; i < curveNum; i++)
        {
            Quaternion nextAngle = Quaternion.Euler(0, CURVE_ANGLE * (i+1) * sign, 0);
            Vector3 nextToFace = (nextAngle * transform.forward);
            curvePath[i] = nextToFace * (CURVE_DIAMETER * (i + 1)) + transform.position;
            curveMarkers[i] = Instantiate(curveMarker);
            curveMarkers[i].transform.position = curvePath[i];
        }
        plotPath(curvePath[curveNum - 1], curvePath);
        curvePlotted = true;
    }
    void plotPath(Vector3 startPoint, Vector3 [] curvePath)
    {
        float distance = Vector3.Distance(positions[destPoint], startPoint);
        sequence = new Vector3[(int) Mathf.Ceil(distance / DIAMETER)];
        markers = new GameObject[(int)Mathf.Ceil(distance / DIAMETER)];
        sequence[0] = startPoint;
        Vector3 direction = (positions[destPoint] - startPoint) / distance; 
        for (int i = 1; i < sequence.Length; i++)
        {
            sequence[i] = (direction * (DIAMETER * i)) + startPoint;
            markers[i] = Instantiate(marker);
            markers[i].transform.position = sequence[i];
            //markers[i].SetActive(false);
        }
        if (curvePath != null && curvePath.Length != 0)
        {
            Vector3 [] fullPath = new Vector3[sequence.Length + curvePath.Length];
            GameObject[] allMarkers = new GameObject[markers.Length + curveMarkers.Length];
            for (int i = 0; i < curvePath.Length; i++)
            {
                fullPath[i] = curvePath[i];
                allMarkers[i] = curveMarkers[i];
            }
            for (int i = 0; i < sequence.Length; i++)
            {
                fullPath[i + curvePath.Length] = sequence[i];
                allMarkers[i + curveMarkers.Length] = markers[i];
            }
            sequence = fullPath;
            markers = allMarkers;
        }
        plotted = true;
        pathProgress = 0;
    }
    void progressPath()
    {
        int toProject = pathProgress + PER_PATH;
        if (markers.Length < toProject) toProject = markers.Length;
        for(int i = pathProgress; i < toProject; i++)
        {
            if (i >= markers.Length || markers[i] == null) continue;
            markers[i].SetActive(true);
        }
    }
    public void obstructedPath(Vector3 hitMarker)
    {
        if(Vector3.Distance(hitMarker, positions[destPoint]) < 30f)
        {
            //TODO: Too close pause the robot until human leaves
        }
        else if(Vector3.Distance(hitMarker, transform.position) < 30f) 
        {
            //TODO: Human close to robot, stationary rotation and avoidance
        }
        else
        {
            //TODO: Arc movement around obstacle
        }
    }
}
