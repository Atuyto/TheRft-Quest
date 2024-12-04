using TMPro;
using UnityEngine;

public class TextMeshProFollower : MonoBehaviour
{
    public Transform planeTransform; // La transform de la plane
    public TextMeshPro textMeshPro; // Le TextMeshPro � faire suivre
    public float offset = 0.1f; // Offset pour positionner le texte derri�re la plane
    public float yOffset = 0.05f; // Ajustement vertical pour d�caler le texte

    void Update()
    {
        if (planeTransform != null && textMeshPro != null)
        {
            // Calculer la position du texte dans l'espace local de la plane
            Vector3 localPosition = new Vector3(0, yOffset, -offset); // D�calage vertical (Y) et arri�re (Z)

            // Convertir la position locale en position mondiale
            Vector3 worldPosition = planeTransform.TransformPoint(localPosition);

            // Appliquer la nouvelle position au texte
            textMeshPro.transform.position = worldPosition;

            // Alignement de la rotation du texte avec celle de la plane
            textMeshPro.transform.rotation = planeTransform.rotation;

            // Si n�cessaire, appliquer une correction pour inverser l'orientation du texte
            // Cette ligne peut �tre ajust�e si le texte est encore mal orient�
            textMeshPro.transform.Rotate(90, 0, 180);
        }
    }
}
