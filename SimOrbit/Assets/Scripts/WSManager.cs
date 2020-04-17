using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WSManager : MonoBehaviour
{
    #region Const

    private const string API_ADDRESS = "http://127.0.0.1/";

    #endregion

    #region Fields

    public CanvasController cvController;

    #endregion

    public void DoActionPost(string route, WWWForm form)
    {
        StartCoroutine(PostMethod(route, form));
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
                    case "results":
                        break;
                    case "register":
                        if (res == "200")
                        {
                            cvController.WSRegister();
                        }
                        break;
                    case "login":
                        Debug.Log(res);
                        if (res == "200")
                        {
                            cvController.WSLogin();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
