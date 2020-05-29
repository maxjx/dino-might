using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour {
    public InputField Username;
    public InputField Password;
    public InputField ReconfirmPassword;
    public Button RegistrationButton;
    void Start()
    {
        RegistrationButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.backend.Registration(Username.text, Password.text));
        });
    }

}
