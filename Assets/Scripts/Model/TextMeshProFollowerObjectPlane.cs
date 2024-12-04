using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshProFollowerObjectPlane : MonoBehaviour
{
    public Transform planeTransform; // La transform de la plane
    public TextMeshPro textMeshPro; // Le TextMeshPro à faire suivre
    public float offset = 0.1f; // Offset pour éviter que le texte ne soit trop près de la plane

    void Update()
    {
        if (planeTransform != null && textMeshPro != null)
        {
            // Ajuster la position du TextMeshPro pour qu'il suive la plane
            textMeshPro.transform.position = planeTransform.position + planeTransform.forward * offset;

            // Ajuster l'orientation du TextMeshPro pour qu'il soit aligné avec la plane
            textMeshPro.transform.rotation = planeTransform.rotation;
        }
    }
}
