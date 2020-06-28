using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AdvancedCollect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] collectibles;
    public Vector3[] positions, sequence;
    private int destPoint;
    private float destAngle, rotSpeed;
    private NavMeshAgent agent;
    private bool facing, targetAcquired;
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
        //GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Facing:  " + facing + ", Target Acquired:  " + targetAcquired);
        if (Vector3.Distance(transform.position, positions[destPoint]) < 0.5f)
        {
            targetAcquired = false;
            facing = false;
        }
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
            //Debug.Log("Destination Vector: " + destVector);
            Debug.Log("Destination Angle: " + destAngle);
            //if (destAngle > -45f && destAngle < 45f)
            //    noRotate(destAngle);
            //else
                facing = rotation(destAngle);
        }

        
    }
    void acquireTarget()
    {
        agent.ResetPath();
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
        agent.destination = positions[destPoint];
    }
    bool rotation(float angle)
    {
        if (Mathf.Abs(destAngle) < 5) return true;
        float preAngle, angleChange;
        angleChange = rotSpeed * Time.deltaTime;
        //Debug.Log("angleChange value: " + angleChange);
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

    }

}
