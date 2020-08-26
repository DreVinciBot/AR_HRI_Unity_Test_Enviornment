using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class New_User : MonoBehaviour
{
    //User input fields
    GameObject genderField, ageField, submitButton;
    //Post-input informative fields
    GameObject userText, passText, welcomeText, proceedButton;
    bool created;
    float timeToMenu;
    // Start is called before the first frame update
    void Start()
    {
        genderField = GameObject.FindGameObjectWithTag("Gender Field");
        ageField = GameObject.FindGameObjectWithTag("Age Field");
        submitButton = GameObject.FindGameObjectWithTag("Submit Button");

        userText = GameObject.FindGameObjectWithTag("User Text");
        userText.SetActive(false);
        passText = GameObject.FindGameObjectWithTag("Pass Text");
        passText.SetActive(false);
        welcomeText = GameObject.FindGameObjectWithTag("Welcome Text");
        welcomeText.SetActive(false);
        proceedButton = GameObject.FindGameObjectWithTag("Proceed Button");
        proceedButton.SetActive(false);
        created = false;
    }

    // Update is called once per frame
    public void makeUser()
    {
        if (genderField.GetComponent<Dropdown>().options[genderField.GetComponent<Dropdown>().value].text == "Gender")
        {
            return;
        }
        if(ageField.GetComponent<InputField>().text[0] == '0')
        {
            return;
        }
        USER_DATA.age = int.Parse(ageField.GetComponent<InputField>().text);
        USER_DATA.gender = genderField.GetComponent<Dropdown>().options[genderField.GetComponent<Dropdown>().value].text;
        USER_DATA.username = USER_DATA.userID + "_" + USER_DATA.gender[0] + USER_DATA.age.ToString() + "_Haven";
        genderField.SetActive(false);
        ageField.SetActive(false);
        submitButton.SetActive(false);
        created = true;
        userText.GetComponent<Text>().text = "Username: " + USER_DATA.username;
        passText.GetComponent<Text>().text = "Password: " + USER_DATA.password;
        welcomeText.SetActive(true);
        userText.SetActive(true);
        passText.SetActive(true);
        proceedButton.SetActive(true);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
