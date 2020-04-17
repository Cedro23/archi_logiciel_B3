using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class WSManagerResults : MonoBehaviour
{
    #region Const

    private const string API_ADDRESS = "http://127.0.0.1/";

    #endregion

    #region Fields

    public ResultsManager rsManager;

    private int nbOfBodies;
    private int nbOfStartParams;
    private int nbOfLaunchParams;

    #endregion

    #region Properties

    public int NbOfLaunchParams { get => nbOfLaunchParams; set => nbOfLaunchParams = value; }
    public int NbOfStartParams { get => nbOfStartParams; set => nbOfStartParams = value; }
    public int NbOfBodies { get => nbOfBodies; set => nbOfBodies = value; }

    #endregion

    #region Unity Methods

    private void Start()
    {
        //Get everylast id

        nbOfBodies = 3;
        nbOfLaunchParams = 3;
        nbOfStartParams = 2;

        DoActionGet("celestialbody");
        DoActionGet("startparams");
        DoActionGet("launchparams");
    }

    #endregion

    #region Public Methods

    public void DoActionGet(string route)
    {
        StartCoroutine(GetMethod(route));
    }

    public void DoActionPost(string route, WWWForm form)
    {
        StartCoroutine(PostMethod(route, form));
    }

    #endregion

    #region Private Methods

    private IEnumerator GetMethod(string route)
    {
        string url = string.Concat(API_ADDRESS, route);
        url = string.Concat(url, "/");

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string res = www.responseCode.ToString();
                byte[] data = www.downloadHandler.data;

                switch (route)
                {
                    case "celestialbody":
                        nbOfBodies = Regex.Matches(Encoding.Default.GetString(data), "id").Count;
                        break;
                    case "startparams":
                        nbOfStartParams = Regex.Matches(Encoding.Default.GetString(data), "id").Count;
                        break;
                    case "launchparams":
                        nbOfLaunchParams = Regex.Matches(Encoding.Default.GetString(data), "id").Count;
                        break;
                    case "results":
                        if (res == "200")
                        {
                            rsManager.RecieveResults(Encoding.Default.GetString(data).ToString()); //pass results in parametre
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }   

    private IEnumerator PostMethod(string route, WWWForm form)
    {
        string url = string.Concat(API_ADDRESS, route);
        url = string.Concat(url, "/");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string res = www.responseCode.ToString();
                switch (route)
                {
                    case "celestialbody":
                        if (res == "200")
                        {
                            Debug.Log("celestial body ok");
                        }
                        break;
                    case "launchparams":
                        if (res == "200")
                        {
                            Debug.Log("launch params ok");
                        }
                        break;
                    case "startparams":
                        if (res == "200")
                        {
                            Debug.Log("start params ok");
                        }
                        break;
                    case "results":
                        if (res == "200")
                        {
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    #endregion
}
