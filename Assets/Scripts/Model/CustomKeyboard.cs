using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class CustomKeyboard : MonoBehaviour
{

    public OVRVirtualKeyboard keyboard;

    void Start()
    {
        // Trouver l'instance de OVRVirtualKeyboard
        keyboard = FindObjectOfType<OVRVirtualKeyboard>();

        if (keyboard != null)
        {
            // S'abonner aux événements
            keyboard.CommitTextEvent.AddListener(OnCommitText);
            keyboard.BackspaceEvent.AddListener(OnBackspace);
            keyboard.EnterEvent.AddListener(OnEnter);
            keyboard.KeyboardShownEvent.AddListener(OnKeyboardShown);
            keyboard.KeyboardHiddenEvent.AddListener(OnKeyboardHidden);
        }
    }

    void OnDestroy()
    {
        if (keyboard != null)
        {
            // Se désabonner des événements
            keyboard.CommitTextEvent.RemoveListener(OnCommitText);
            keyboard.BackspaceEvent.RemoveListener(OnBackspace);
            keyboard.EnterEvent.RemoveListener(OnEnter);
            keyboard.KeyboardShownEvent.RemoveListener(OnKeyboardShown);
            keyboard.KeyboardHiddenEvent.RemoveListener(OnKeyboardHidden);
        }
    }

    private void OnCommitText(string text)
    {
        WebSocketManager webSocketManager = FindObjectOfType<WebSocketManager>();
        Message message = new Message(text, "1", "2");
        webSocketManager.SendMessage(JsonConvert.SerializeObject(message));
        Debug.Log("Text committed: " + text);
        // Ajoutez votre logique ici
    }

    private void OnBackspace()
    {
        Debug.Log("Backspace pressed");
        // Ajoutez votre logique ici
    }

    private void OnEnter()
    {
        
        Debug.Log("Enter pressed");
        // Ajoutez votre logique ici
    }

    private void OnKeyboardShown()
    {
        Debug.Log("Keyboard shown");
        // Ajoutez votre logique ici
    }

    private void OnKeyboardHidden()
    {
        Debug.Log("Keyboard hidden");
        // Ajoutez votre logique ici
    }
}
