using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Registration : MonoBehaviour {
    public InputField username;
    public InputField password;
    public InputField reconfirmPassword;
    public Button registrationButton;
    public Text errorText;
    public Text informText;
    public GameObject loadingCircle;
    public GameObject canvas;
    public void OnRegistrationButtonClick() {
        if (password.text.Length <= 7 || username.text.Length <=5) {
            errorText.text = "Username and Password must be at least 6 and 8 characters respectively";
        } else if (password.text.Equals(reconfirmPassword.text)) {
            registrationButton.interactable = false;
            loadingCircle.SetActive(true);
            StartCoroutine(RegistrationForm());
        } else {
            errorText.text = "Passwords entered do not match.";
        }
    }

    private IEnumerator RegistrationForm() {
        WWWForm form = new WWWForm();
        form.AddField("inputUsername", username.text);
        form.AddField("inputPassword", password.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight2.000webhostapp.com/backend/Registration.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                errorText.text = www.error;
            } else {
                if (www.isDone) {
                    if (www.downloadHandler.text.Contains("successfully")) {
                        informText.text = www.downloadHandler.text;
                        www.Dispose();
                        canvas.GetComponent<MainPageManager>().RegisterSuccess();
                    } else {
                        errorText.text = www.downloadHandler.text;
                    }
                }
            }
            registrationButton.interactable = true;
            loadingCircle.SetActive(false);
            www.Dispose();
        }
    }

}
