using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMessageRecieved : MonoBehaviour
{
    void Start()
    {
        
        Main.systemMessage.OnListChanged += onMessageRecieved;
    }

    void Update()
    {
        
    }

    private void onMessageRecieved(List<SystemMessage> systemMessages)
    {
        SystemMessage lastMessagesRecieved = systemMessages[systemMessages.Count - 1];
        switch (lastMessagesRecieved.code)
        {
            case "12501": // CODE Enigme 2
                SceneManager.LoadScene("Enigma2");
                break;
            case "12502": // CODE Enigme 3
                SceneManager.LoadScene("SceneFree");
                break;
            default:
                //TODO
                break;
        }
    }
}
