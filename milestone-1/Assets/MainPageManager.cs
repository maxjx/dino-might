using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageManager : MonoBehaviour {
    public GameObject login;
    public GameObject start;

    void Start() {
        if (Global.isLoggedIn) {
            login.SetActive(false);
            start.SetActive(true);
        }
    }
}
