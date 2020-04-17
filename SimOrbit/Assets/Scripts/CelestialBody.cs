using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    #region Fields

    public Rigidbody rb;
    public double mass;
    public double radius;

    public enum CurrentState //Current state
    {
        Initialising,
        Ready
    }
    public CurrentState currentState = CurrentState.Initialising;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (SetScale())
        {
            currentState = CurrentState.Ready;
        }
    }

    #region Private methods

    private bool SetScale()
    {
        this.transform.localScale = new Vector3((float)radius, (float)radius, (float)radius);

        return true;
    }

    #endregion
}
