using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
     public OVRCameraRig cameraRig;
    public float distanceFromCamera = 5f;
    public Vector3 offset = Vector3.zero; // D�calage optionnel par rapport � la position cible

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
            forwardDirection.Normalize(); // Normalisation pour conserver une direction coh�rente

            // Position de l'objet � une distance donn�e dans la direction de la cam�ra
            Vector3 targetPosition = mainCamera.transform.position + forwardDirection * distanceFromCamera;

            // Ajuste la hauteur de l'objet � celle de la cam�ra + un offset
            targetPosition.y = mainCamera.transform.position.y + 0f;

            transform.position = targetPosition;

            // Oriente l�objet pour qu�il soit toujours face � la cam�ra
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // Inverse pour �tre face � la cam�ra
        }
    }

    void Update()
    {
       
    }
}
