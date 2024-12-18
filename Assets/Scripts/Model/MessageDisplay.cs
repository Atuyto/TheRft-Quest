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

            if (messages != null && messages.Count > 0)
            {
                // Efface le texte précédent
                messageText.text = "";

                // Parcourir tous les messages et les ajouter à messageText
                foreach (Message message in messages)
                {
                    messageText.text += message.ToString() + "\n"; 
                }
            }
           
        }
    }
}
    

