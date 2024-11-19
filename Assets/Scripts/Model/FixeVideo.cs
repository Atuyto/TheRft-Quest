using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
     public OVRCameraRig cameraRig;
    public float distanceFromCamera = 5f;
    public Vector3 offset = Vector3.zero; // Décalage optionnel par rapport à la position cible

    private Camera mainCamera;

    void Start()
    {
        if (cameraRig == null)
        {
            cameraRig = FindObjectOfType<OVRCameraRig>();
        }

        if (cameraRig != null)
        {
            mainCamera = cameraRig.centerEyeAnchor.GetComponent<Camera>();
        }

        if (mainCamera != null)
        {
            Vector3 forwardDirection = mainCamera.transform.forward;
            forwardDirection.y = 0; // Nous ignorons la composante verticale pour ne pas affecter l'inclinaison
            forwardDirection.Normalize(); // Normalisation pour conserver une direction cohérente

            // Position de l'objet à une distance donnée dans la direction de la caméra
            Vector3 targetPosition = mainCamera.transform.position + forwardDirection * distanceFromCamera;

            // Ajuste la hauteur de l'objet à celle de la caméra + un offset
            targetPosition.y = mainCamera.transform.position.y + 0f;

            transform.position = targetPosition;

            // Oriente l’objet pour qu’il soit toujours face à la caméra
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // Inverse pour être face à la caméra
        }
    }

    void Update()
    {
       
    }
}
