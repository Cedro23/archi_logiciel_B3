using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    #region Fields

    private static int iterationNb = 0;

    public GravitationalForceManager gfManager;
    public WSManagerResults wsManager;

    public CelestialBody fixedBody;
    public CelestialBody orbitingBody;

    private Vector3 launchDirection;

    #endregion

    #region Public Methods

    public void StartSimulation(double initialDistance, double massA, double massB, float initialAngle, float initialThrust)
    {

        if (fixedBody.currentState == CelestialBody.CurrentState.Ready && orbitingBody.currentState == CelestialBody.CurrentState.Ready)
        {
            ClampDistance(initialDistance);

            //Set orbiting body to initial distance (1 unit = 1000 km)
            orbitingBody.transform.position = new Vector3(0, (float)initialDistance, 0);

            initialAngle = 90 - initialAngle;

            //Set initial angle on the planetoid
            launchDirection = new Vector3(0, Mathf.Cos(initialAngle * Mathf.Deg2Rad), Mathf.Sin(initialAngle * Mathf.Deg2Rad));

            gfManager.GatherInfos(massA, massB, launchDirection, initialThrust, initialAngle, initialDistance);
            //replace with observer pattern
        }
    }

    public void UpdateOrbitingPosition(float newPos)
    {
        orbitingBody.transform.position = new Vector3(0, newPos, 0);
        ClampDistance(newPos);
    }

    public void UpdateOrbitingRadius(float newRad)
    {
        orbitingBody.transform.localScale = new Vector3(newRad, newRad, newRad);
        orbitingBody.radius = newRad;
        ClampDistance();
    }

    public void UpdateFixedRadius(float newRad)
    {
        fixedBody.transform.localScale = new Vector3(newRad, newRad, newRad);
        fixedBody.radius = newRad;
        ClampDistance();
    }

    public void StopSim()
    {
        gfManager.StopSim();
    }

    public void SendResults(double bodyMass, double bodyRadius, float speed, float angle, double stationMass, double distance, float duration)
    {
        iterationNb++;

        WWWForm formBody = new WWWForm();
        formBody.AddField("name", "body");
        formBody.AddField("mass", bodyMass.ToString());
        formBody.AddField("radius", bodyRadius.ToString());
        wsManager.DoActionPost("celestialbody", formBody);



        WWWForm formLaunch = new WWWForm();
        formLaunch.AddField("speed", speed.ToString());
        formLaunch.AddField("angle", angle.ToString());
        wsManager.DoActionPost("launchparams", formLaunch);

        WWWForm formStart = new WWWForm();
        formStart.AddField("station_mass", stationMass.ToString());
        formStart.AddField("distance", distance.ToString());
        formStart.AddField("celestial_body", wsManager.NbOfBodies);
        wsManager.DoActionPost("startparams", formStart);
        wsManager.NbOfBodies++;

        WWWForm formRes = new WWWForm();
        formRes.AddField("iteration_nb", speed.ToString());
        formRes.AddField("succes", "true");
        formRes.AddField("date_time", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
        formRes.AddField("sim_duration", duration.ToString());
        formRes.AddField("start_params", wsManager.NbOfStartParams);
        formRes.AddField("launch_params", wsManager.NbOfLaunchParams);
        wsManager.DoActionPost("results", formRes);
        wsManager.NbOfStartParams++;
        wsManager.NbOfLaunchParams++;
    }

    #endregion

    #region Private Methods

    private void ClampDistance(double initialDistance)
    {
        //Clamp distance to à minimum
        if (initialDistance < fixedBody.radius + orbitingBody.radius)
        {
            initialDistance = fixedBody.radius / 2 + orbitingBody.radius;
            orbitingBody.transform.position = new Vector3(0, (float)initialDistance, 0);
        }
    }

    private void ClampDistance()
    {
        //Clamp distance to à minimum
        if (Vector3.Distance(fixedBody.transform.position, orbitingBody.transform.position) < fixedBody.radius + orbitingBody.radius)
        {
            double initialDistance = fixedBody.radius / 2 + orbitingBody.radius;
            orbitingBody.transform.position = new Vector3(0, (float)initialDistance, 0);
        }
    }

    #endregion
}
