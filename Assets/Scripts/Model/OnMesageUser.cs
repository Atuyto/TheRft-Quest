using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnMessageUser : MonoBehaviour
{
    public Message lastMessageReceived;
    public GameObject readyText;
    public TextMeshProUGUI messageText; // Utiliser un seul TextMeshProUGUI pour afficher tous les messages

    void Start()
    {
        Player player = Player.Instance;
        player.messages.OnListChanged += onMessageReceived;
    }

    void Update()
    {
        // Vous pouvez ajouter des mises � jour ici si n�cessaire
    }

    private void onMessageReceived(List<Message> messages)
    {
        lastMessageReceived = messages[messages.Count - 1];
        if (lastMessageReceived.from == "Oculus")
        {
            return;
        }

        readyText.gameObject.SetActive(true);

        // Mettre � jour le texte sur la plane en fonction du type de message
        if (messageText != null)
        {
            messageText.text = ""; // Effacer le texte pr�c�dent

            for (int i = 0; i < messages.Count; i++)
            {
                Message currentMessage = messages[i];

                // Ajouter une ligne vide si le dernier message �tait de l'autre utilisateur
                if (i > 0 && messages[i - 1].from != currentMessage.from)
                {
                    messageText.text += "\n";
                }

                // Ajouter le nouveau message
                if (currentMessage.from == "User") // Remplacez "User" par l'identifiant de l'utilisateur
                {
                    // Message envoy� par l'utilisateur
                    messageText.text += $"<align=right>{currentMessage.message}</align>\n";
                }
                else
                {
                    // Message re�u
                    messageText.text += $"<align=left>{currentMessage.message}</align>\n";
                }
            }
        }
    }

    void OnDestroy()
    {
       Player player = Player.Instance;
        player.messages.OnListChanged -= onMessageReceived;
    }
}
