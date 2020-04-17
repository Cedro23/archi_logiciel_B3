using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    #region Fields

    public Text txtResult;

    #endregion

    #region Public Methods

    public void RecieveResults(string result)
    {
        Debug.Log(result);
        txtResult.text = result;
    }

    #endregion


}
