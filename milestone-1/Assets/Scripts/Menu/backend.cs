using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class backend : MonoBehaviour {
/*
    // Purely for debugging purposes
    void Start()
    {
        StartCoroutine(GetRequest("https://dinomight.000webhostapp.com/backend/GetData.php"));
        StartCoroutine(Login("testuser", "12345"));
        StartCoroutine(Registration("testuser", "12345"));
    }
*/

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("inputUsername", username);
        form.AddField("inputPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight.000webhostapp.com/backend/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Received: " + www.downloadHandler.text);
            }
        }
    }

    public IEnumerator Registration(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("inputUsername", username);
        form.AddField("inputPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight.000webhostapp.com/backend/Registration.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Received: " + www.downloadHandler.text);
            }
        }
    }
}
