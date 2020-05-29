using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField Username;
    public InputField Password;
    public Button LoginButton;
    public Button RegistrationButton;
    void Start()
    {
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.backend.Login(Username.text, Password.text));
        });
    }

}
