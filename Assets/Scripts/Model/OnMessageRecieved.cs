using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OnMessageRecieved : MonoBehaviour
{
    public SystemMessage lastMessagesRecieved;
    public GameObject readyText;

    void Start()
    {
        
        Main.systemMessage.OnListChanged += onMessageRecieved;
    }

    void Update()
    {
        
    }

    private void onMessageRecieved(List<SystemMessage> systemMessages)
    {
        lastMessagesRecieved = systemMessages[systemMessages.Count - 1];
        if (lastMessagesRecieved.title == "Oculus"){
            return;
        }
        //readyText.gameObject.SetActive(true);
        switch (lastMessagesRecieved.code)
        {
            case "12501": // CODE Enigme 2
                SceneManager.LoadScene("Enigma2");
                break;
            case "12502": // CODE Enigme 3
                SceneManager.LoadScene("Enigma3");
                break;
            default:
                //TODO
                break;
        }
    }
}
