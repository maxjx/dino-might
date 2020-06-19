using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageManager : MonoBehaviour {
    public GameObject login;
    public GameObject start;
    public GameObject register;

    void Start() {
        if (Global.isLoggedIn) {
            login.SetActive(false);
            start.SetActive(true);
        }
    }

    public void LoginSuccess() {
        login.SetActive(false);
        start.SetActive(true);
    }

    public void Register() {
        login.SetActive(false);
        register.SetActive(true);
    }

    public void RegisterSuccess() {
        start.SetActive(true);
        register.SetActive(false);
    }
}
