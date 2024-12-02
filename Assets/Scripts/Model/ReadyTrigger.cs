using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyTrigger : MonoBehaviour
{
    
    public GameObject readyText; 
    private bool isReadyDisplayed = false;

    // Start is called before the first frame update
    void Start()
    {   
        if (readyText != null)
        {
            //readyText.gameObject.SetActive(false);
            readyText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        OnMessageRecieved onMessage = FindObjectOfType<OnMessageRecieved>();
        SystemMessage lastMessagesRecieved = onMessage.lastMessagesRecieved;
        /*switch (lastMessagesRecieved.code)
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
        }*/

        if (other.CompareTag("Hand"))
        {
            SceneManager.LoadScene("Enigma2");
        }
    }

}
