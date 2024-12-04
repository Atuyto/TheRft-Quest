using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    public TextMeshPro messageText;
    public int maxMessages = 10;

    void Start()
    {
        Player player = Player.Instance;
        player.messages.OnListChanged += OnNewMessage;
    }

    // Lorsque la liste des messages change
    public void OnNewMessage(List<Message> messages)
    {
        // Ajouter les nouveaux messages à la fin sans effacer les anciens
        int startIndex = Mathf.Max(0, messages.Count - maxMessages); // Assurer qu'on n'affiche pas trop de messages
        messageText.text = "";  // On efface le texte au début

        for (int i = startIndex; i < messages.Count; i++)
        {
            DisplayMessage(messages[i]);  // Afficher chaque message
        }
    }

    // Lorsque le script est détruit, on désinscrit la callback
    public void OnDestroy()
    {
        Player player = Player.Instance;
        player.messages.OnListChanged -= OnNewMessage;
    }

    // Fonction pour afficher un message spécifique
    private void DisplayMessage(Message message)
    {
        string messageToDisplay = "";

        // Si l'expéditeur est celui de l'utilisateur
        if (message.from == "1")
        {
            messageToDisplay = "<align=right>" + message.message + "</align>\n";  // Aligner à droite
        }
        else
        {
            messageToDisplay = "<align=left>" + message.message + "</align>\n";  // Aligner à gauche
        }

        // Ajouter le message au texte total
        messageText.text += messageToDisplay;
    }
}
