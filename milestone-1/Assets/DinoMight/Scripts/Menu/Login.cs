using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public InputField Username;
    public InputField Password;
    public Button LoginButton;
    public GameObject loadingCircle;
    public Text errorMessage;
    public GameObject playMenu;
    public GameObject loginMenu;
    
    public void OnLoginButton() {
        LoginButton.interactable = false;
        loadingCircle.SetActive(true);
        StartCoroutine(LoginForm(Username.text, Password.text));
    }

    public IEnumerator LoginForm(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("inputUsername", username);
        form.AddField("inputPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight.000webhostapp.com/backend/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                errorMessage.text = www.error;
            } else {
                if (www.isDone) {
                    if (!www.downloadHandler.text.Contains(".")) {
                        Global.playerId = int.Parse(www.downloadHandler.text);
                        playMenu.SetActive(true);
                        www.Dispose();
                        loginMenu.SetActive(false);
                        Global.isLoggedIn = true;
                    } else {
                        errorMessage.text = www.downloadHandler.text;
                    }
                }
            }
            LoginButton.interactable = true;
            loadingCircle.SetActive(false);
            www.Dispose();
        }
    }

}
