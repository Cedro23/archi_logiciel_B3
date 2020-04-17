using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasLevelManager : MonoBehaviour
{
    #region Fields

    public SimulationManager simManager;

    public Transform panelTransform;
    public GameObject panelSim;
    public GameObject panelResults;

    public InputField inpInitDistance;
    public InputField inpInitAngle;
    public InputField inpInitThrust;
    public InputField inpMassA;
    public InputField inpMassB;
    public InputField inpRadA;
    public InputField inpRadB;

    public Text userTextMessage;
    public Text txtCurDistance;
    public Text txtCurVelocity;
    public Text txtCurGravForce;

    public Button btnStart;
    public Button btnStop;
    public Button btnResults;

    public WSManagerResults wsManResults;

    #endregion

    #region Public Methods

    public void GatherInfoAndStartSimulation()
    {
        string message = string.Empty;

        if (float.TryParse(inpInitDistance.text, out float initDist))
        {
            if (float.TryParse(inpInitAngle.text, out float initAngle))
            {
                if (float.TryParse(inpInitThrust.text, out float initThrust))
                {
                    if (float.TryParse(inpMassA.text, out float mA))
                    {
                        if (float.TryParse(inpMassB.text, out float mB))
                        {
                            simManager.StartSimulation(initDist, mA, mB, initAngle, initThrust);
                            btnStart.interactable = false;
                            btnResults.interactable = false;
                            btnStop.interactable = true;
                            panelTransform.position = new Vector3(panelTransform.position.x + 679, panelTransform.position.y, 0);
                        }
                        else
                        {
                            message = "La masse B n'est pas renseignée";
                        }
                    }
                    else
                    {
                        message = "La masse A n'est pas renseignée";
                    }
                }
                else
                {
                    message = "La poussée initiale n'est pas renseignée";
                }
            }
            else
            {
                message = "L'angle inital n'est pas renseigné";
            }
        }
        else
        {
            message = "La distance initiale n'est pas renseignée";
        }
        UpdateUserTextMessage(true, message);

    }

    public void OnInitialDistanceChanged()
    {
        if (float.TryParse(inpInitDistance.text, out float parseResult))
        {
            simManager.UpdateOrbitingPosition(parseResult);
        }
    }

    public void OnRayonAChanged()
    {
        if (float.TryParse(inpRadA.text, out float parseResult))
        {
            simManager.UpdateOrbitingRadius(parseResult);
        }        
    }

    public void OnRayonBChanged()
    {
        if (float.TryParse(inpRadB.text, out float parseResult))
        {
            simManager.UpdateFixedRadius(parseResult);
        }
    }

    public void OnBtnStopClick()
    {
        simManager.StopSim();

        btnStop.interactable = false;
        btnStart.interactable = true;        
        btnResults.interactable = true;
        panelTransform.position = new Vector3(panelTransform.position.x - 679, panelTransform.position.y, 0);
    }

    public void Disconnect()
    {
        SceneManager.LoadScene("RegisterLogin", LoadSceneMode.Single);
    }

    public void CheckResults()
    {
        panelSim.SetActive(false);
        panelResults.SetActive(true);
        wsManResults.DoActionGet("results");
    }

    public void BackToSim()
    {
        panelSim.SetActive(true);
        panelResults.SetActive(false);
    }

    public void UpdateTxtCurDistance(string newText)
    {
        txtCurDistance.text = newText;
    }

    public void UpdateTxtCurVelocity(string newText)
    {
        txtCurVelocity.text = newText;
    }

    public void UpdateTxtCurGravForce(string newText)
    {
        txtCurGravForce.text = newText;
    }

    #endregion

    #region Private Methods

    public void UpdateUserTextMessage(bool isDisplayed, string message)
    {
        if (isDisplayed)
        {
            userTextMessage.text = message;
            userTextMessage.enabled = true;
        }
        else
        {
            userTextMessage.enabled = false;
        }
    }

    #endregion
}
