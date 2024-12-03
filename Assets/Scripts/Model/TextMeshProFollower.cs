using TMPro;
using UnityEngine;

public class TextMeshProFollower : MonoBehaviour
{
    public Transform planeTransform; // La transform de la plane
    public TextMeshPro textMeshPro; // Le TextMeshPro à faire suivre
    public float offset = 0.1f; // Offset pour positionner le texte derrière la plane
    public float yOffset = 0.05f; // Ajustement vertical pour décaler le texte

    void Update()
    {
        if (planeTransform != null && textMeshPro != null)
        {
            // Calculer la position du texte dans l'espace local de la plane
            Vector3 localPosition = new Vector3(0, yOffset, -offset); // Décalage vertical (Y) et arrière (Z)

            // Convertir la position locale en position mondiale
            Vector3 worldPosition = planeTransform.TransformPoint(localPosition);

            // Appliquer la nouvelle position au texte
            textMeshPro.transform.position = worldPosition;

            // Alignement de la rotation du texte avec celle de la plane
            textMeshPro.transform.rotation = planeTransform.rotation;

            // Si nécessaire, appliquer une correction pour inverser l'orientation du texte
            // Cette ligne peut être ajustée si le texte est encore mal orienté
            textMeshPro.transform.Rotate(90, 0, 180);
        }
    }
}
