using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageDisplay : MonoBehaviour
{
    public TMP_Text messageText;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(UpdateMessages());
    }

    IEnumerator UpdateMessages()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Mettre à jour toutes les secondes
            List<Message> messages = player.GetMessages();
            messageText.text = string.Join("\n", messages);
        }
    }
    
}
