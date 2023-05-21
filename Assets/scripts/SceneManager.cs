using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;

public class SceneManager : MonoBehaviour
{
    public InputField myInputField;
    public Button myButton;
    public Button message;

    [System.Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public class MessagesWrapper
    {
        public Message[] messages;
    }

    void Start()
    {
        myButton.onClick.AddListener(OnButtonPress);
    }

    public void OnButtonPress()
    {
        string inputText = myInputField.text;
        StartCoroutine(PostRequest(inputText));
    }

    IEnumerator PostRequest(string inputText)
    {
        string url = "https://api.openai.com/v1/chat/completions";

        var messages = new Message[]
        {
            new Message { role = "system", content = "You are a helpful assistant." },
            new Message { role = "user", content = inputText }
        };
        var wrapper = new MessagesWrapper { messages = messages };

        string json = JsonUtility.ToJson(wrapper);

        UnityWebRequest request = UnityWebRequest.Post(url, json);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer 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");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var N = JSON.Parse(request.downloadHandler.text);
            string response = N["choices"][0]["message"]["content"].Value;
            message.GetComponentInChildren<Text>().text = response;  
        }
    }
}