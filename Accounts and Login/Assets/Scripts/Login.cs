using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    GameObject userField, passField, loginButton, newUserButton;
    string dbPath;
    WWW userDB;
    // Start is called before the first frame update
    void Start()
    {
        dbPath = "localhost:3000/retrieve/";
        userField = GameObject.FindGameObjectWithTag("User Text");
        passField = GameObject.FindGameObjectWithTag("Pass Text");
        loginButton = GameObject.FindGameObjectWithTag("Proceed Button");
        newUserButton = GameObject.FindGameObjectWithTag("New User Button");

    }

    public void NewUser()
    {
        SceneManager.LoadScene("New_User");
    }
    
    public void CheckCreds()
    {

    }
}
