using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class CustomKeyboard : MonoBehaviour
{

    public OVRVirtualKeyboard keyboard;
    private string text;

    void Start()
    {
        // Trouver l'instance de OVRVirtualKeyboard
        keyboard = FindObjectOfType<OVRVirtualKeyboard>();

        if (keyboard != null)
        {

            keyboard.CommitTextEvent?.RemoveAllListeners();
            keyboard.BackspaceEvent?.RemoveAllListeners();
            keyboard.EnterEvent?.RemoveAllListeners();
            keyboard.KeyboardShownEvent?.RemoveAllListeners();
            keyboard.KeyboardHiddenEvent?.RemoveAllListeners();

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
        this.text += text;
    }

    private void OnBackspace()
    {
        Debug.Log("Backspace pressed");
        // Ajoutez votre logique ici
    }

    private void OnEnter()
    {
        WebSocketManager webSocketManager = FindObjectOfType<WebSocketManager>();
        Message message = new Message(this.text, "1", "2");
        webSocketManager.SendMessage(JsonConvert.SerializeObject(message));
        this.text = "";

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
