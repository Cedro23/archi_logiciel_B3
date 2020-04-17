using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public Button btnRegister;
    public Button btnLogin;

    public InputField inpUsername;
    public InputField inpPassword1;
    public InputField inpPassword2;

    public Text userTextMessage;

    public WSManager wsManager;

    #region Public Methods

    public void CheckIfUsernameIsEmpty()
    {
        if (!string.IsNullOrEmpty(inpUsername.text))
        {
            //unlock password1
            inpPassword1.interactable = true;
        }
        else
        {
            //lock password1 & 2

            inpPassword1.interactable = false;
            string userTextMessage = "Le nom d'utilisateur est vide";
            UpdateUserTextMessage(true, userTextMessage);
        }
    }

    public void CheckIfPassword1IsEmpty()
    {
        if (!string.IsNullOrEmpty(inpPassword1.text))
        {
            if (CheckIfPassword1MeetsRequirements())
            {
                //unlock password2 & button login
                UpdateUserTextMessage(false, null);

                inpPassword2.interactable = true;
                btnLogin.interactable = true;
            }
            else
            {
                string userTextMessage = "Le mot de passe ne remplie pas les conditions nécessaires";
                UpdateUserTextMessage(true, userTextMessage);
            }

        }
        else
        {
            //lock password 2 & button login

            inpPassword2.interactable = false;
            btnLogin.interactable = false;
        }
    }

    public void CheckIfPassword2IsEmpty()
    {
        if (!string.IsNullOrEmpty(inpPassword2.text))
        {
            //check if passwords match
            if (CheckIfPassword2EqualsPassword1())
            {
                UpdateUserTextMessage(false, null);
                btnRegister.interactable = true;
            }
            else
            {
                string userTextMessage = "Les mots de passe ne correspondent pas";
                UpdateUserTextMessage(true, userTextMessage);
                btnRegister.interactable = false;
            }
        }
        else
        {
            string userTextMessage = "Le mot de passe n'est pas confirmé";
            UpdateUserTextMessage(true, userTextMessage);
            btnRegister.interactable = false;
        }
    }

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

    public void Register()
    {
        //call to wsmanager
        Debug.Log("Call to ws register");

        string route = "register";

        string username = inpUsername.text;
        string password1 = inpPassword1.text;
        string password2 = inpPassword2.text;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password1", password1);
        form.AddField("password2", password2);

        wsManager.DoActionPost(route, form);
    }

    public void Login()
    {
        //call to wsmanager
        Debug.Log("Call to ws login");

        string route = "login";

        string username = inpUsername.text;
        string password = inpPassword1.text;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        wsManager.DoActionPost(route, form);
    }

    public void WSLogin()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void WSRegister()
    {
        string userTextMessage = "Vous êtes bien enregistré";
        UpdateUserTextMessage(true, userTextMessage);
    }

    #endregion


    #region Private Methods

    private bool CheckIfPassword2EqualsPassword1()
    {
        string password1 = inpPassword1.text;
        string password2 = inpPassword2.text;

        if (password1 == password2)
            return true;

        return false;
    }

    private bool CheckIfPassword1MeetsRequirements()
    {
        string password = inpPassword1.text;
        string username = inpUsername.text;

        if (password.Length > 9 && !IsDigitsOnly(password) && !password.Contains(username))
            return true;

        return false;
    }

    bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }

    #endregion
}
