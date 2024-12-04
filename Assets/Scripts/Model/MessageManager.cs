using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MessageManager : MonoBehaviour
{
    public GameObject messagePrefab; // Le préfab pour les messages
    public Transform messageContainer; // Le conteneur pour les messages (Content du ScrollRect)
    public ScrollRect scrollRect; // Le ScrollRect

    void Start()
    {
        // Exemple de messages pour tester
        AddMessage("Message reçu 1", false);
        AddMessage("Message envoyé 1", true);
        AddMessage("Message reçu 2", false);
    }

    public void AddMessage(string message, bool isSent)
    {
        GameObject messageObject = Instantiate(messagePrefab, messageContainer);
        Text messageText = messageObject.GetComponent<Text>();
        messageText.text = message;

        // Aligner le message à gauche ou à droite
        RectTransform rectTransform = messageObject.GetComponent<RectTransform>();
        if (isSent)
        {
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-10, 0); // Ajustez la position en fonction de la hauteur des messages
        }
        else
        {
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);
            rectTransform.anchoredPosition = new Vector2(10, 0); // Ajustez la position en fonction de la hauteur des messages
        }

        // Faire défiler automatiquement vers le bas
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
