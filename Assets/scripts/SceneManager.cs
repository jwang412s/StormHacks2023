using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneManager : MonoBehaviour
{
    public InputField myInputField;
    public Button myButton;
    public Button message;

    void Start()
    {
        myButton.onClick.AddListener(OnButtonPress);
    }

    public void OnButtonPress()
    {
        string inputText = myInputField.text;
        message.GetComponentInChildren<Text>().text = inputText;
    }
}
