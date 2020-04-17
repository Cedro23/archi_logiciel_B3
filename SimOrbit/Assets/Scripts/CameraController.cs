using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CelestialBody planetoid;
    public CelestialBody fixedBody;
    public Vector3 center;
    public Vector3 offset;

    void Start()
    {

    }

    void Update()
    {
        if (fixedBody.currentState == CelestialBody.CurrentState.Ready && planetoid.currentState == CelestialBody.CurrentState.Ready)
        {
            if (planetoid.radius > fixedBody.radius)
            {
                offset = new Vector3((float)planetoid.radius + Vector3.Distance(planetoid.transform.position, fixedBody.transform.position), 0, 0);
            }
            else
            {
                offset = new Vector3((float)fixedBody.radius + Vector3.Distance(planetoid.transform.position, fixedBody.transform.position), 0, 0);
            }
        }



        center = ((fixedBody.transform.position - planetoid.transform.position) / 2.0f) + planetoid.transform.position;
        transform.position = center + offset;
    }
}
