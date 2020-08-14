using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera m_camera;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + m_camera.transform.rotation*
        Vector3.forward, m_camera.transform.rotation*Vector3.up);
    }
}
