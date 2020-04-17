using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForceManager : MonoBehaviour
{

    #region Constants

    private const double GRAV_CONSTANT = 0.0000000667;

    #endregion

    #region Fields

    public CanvasLevelManager cnvLvlManager;
    public SimulationManager simManager;

    public CelestialBody fixedBody;
    public CelestialBody orbitingBody;

    private double massA;
    private double massB;

    private bool isSimulating = false;

    private LineRenderer lrForVector;
    private LineRenderer lrNorVector;
    public Material matForwardVector;
    public Material matNormalVector;

    private float initThrust;
    private float initAngle;
    private double initDistance;

    #endregion

    private void Start()
    {
        //Setup line renderers
        lrForVector = orbitingBody.GetComponentsInChildren<LineRenderer>()[0];
        lrForVector.material = matForwardVector;
        lrForVector.positionCount = 2;
        lrForVector.startWidth = 0.3f;
        lrForVector.endWidth = 0.3f;

        lrNorVector = orbitingBody.GetComponentsInChildren<LineRenderer>()[1];
        lrNorVector.material = matNormalVector;
        lrNorVector.positionCount = 2;
        lrNorVector.startWidth = 0.3f;
        lrNorVector.endWidth = 0.3f;
    }

    private void FixedUpdate()
    {
        if (isSimulating)
        {
            //Calc normal direction
            Vector3 heading = fixedBody.transform.position - orbitingBody.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            double gravForce = GRAV_CONSTANT * ((massA * massB) / Mathf.Pow(distance, 2));

            //Display normal and forward vectors
            lrForVector.SetPosition(0, transform.position);
            lrForVector.SetPosition(1, orbitingBody.transform.InverseTransformDirection(orbitingBody.rb.velocity));

            //Direction vector between planetoid and "sun"
            lrNorVector.SetPosition(0, transform.position);
            lrNorVector.SetPosition(1, direction * (float)gravForce);

            //Add acceleration force to orbiting object
            orbitingBody.rb.AddForce(direction * (float)gravForce, ForceMode.Acceleration);

            //Update current infos
            float speed = orbitingBody.GetComponent<Rigidbody>().velocity.magnitude;

            cnvLvlManager.UpdateTxtCurDistance($"Distance : {distance.ToString("0.00")}");
            cnvLvlManager.UpdateTxtCurVelocity($"Vitesse : {speed.ToString("0.00")}");
            cnvLvlManager.UpdateTxtCurGravForce($"Force Grav. : {gravForce.ToString("0.00")}");
        }
    }

    public void GatherInfos(double mA, double mB, Vector3 launchDirection, float initialThrust, float initialAngle, double initialDistance)
    {
        initThrust = initialThrust;
        initAngle = initialAngle;
        initDistance = initialDistance;

        massA = mA;
        massB = mB;

        GiveInitialImpulse(launchDirection, initialThrust);
    }

    public void StopSim()
    {
        this.enabled = false;
        orbitingBody.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        simManager.SendResults(massA, orbitingBody.radius, initThrust, initAngle, massB, initDistance, 60.0f); //change the duration with a timer value
    }

    #region Private methods

    private void GiveInitialImpulse(Vector3 launchDirection, float initialThrust)
    {
        //give initial impulse
        orbitingBody.rb.AddForce(launchDirection * initialThrust, ForceMode.Impulse);

        this.enabled = true;
        isSimulating = true;
    }

    #endregion
}