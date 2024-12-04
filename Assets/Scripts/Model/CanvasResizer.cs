using UnityEngine;

public class CanvasResizer : MonoBehaviour
{
    public Transform planeTransform; // La transform de la plane
    public RectTransform canvasRectTransform; // Le RectTransform du Canvas
    public Camera mainCamera; // La caméra principale

    void Update()
    {
        if (planeTransform != null && canvasRectTransform != null && mainCamera != null)
        {
            // Obtenir les dimensions de la plane
            MeshRenderer meshRenderer = planeTransform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Bounds bounds = meshRenderer.bounds;

                // Calculer les coins de la plane dans l'espace 3D
                Vector3[] corners = new Vector3[4];
                corners[0] = bounds.min; // Coin inférieur gauche
                corners[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z); // Coin inférieur droit
                corners[2] = bounds.max; // Coin supérieur droit
                corners[3] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z); // Coin supérieur gauche

                // Convertir les coins de la plane en coordonnées de l'écran
                Vector2[] screenCorners = new Vector2[4];
                for (int i = 0; i < 4; i++)
                {
                    screenCorners[i] = mainCamera.WorldToScreenPoint(corners[i]);
                }

                // Calculer la taille du Canvas en fonction des coins de la plane
                float width = Mathf.Abs(screenCorners[1].x - screenCorners[0].x);
                float height = Mathf.Abs(screenCorners[2].y - screenCorners[0].y);

                // Ajuster la taille du Canvas
                canvasRectTransform.sizeDelta = new Vector2(width, height);

                // Ajuster la position du Canvas pour qu'il soit centré sur la plane
                Vector2 center = new Vector2((screenCorners[0].x + screenCorners[2].x) / 2, (screenCorners[0].y + screenCorners[2].y) / 2);
                canvasRectTransform.anchoredPosition = center;
            }
        }
    }
}
