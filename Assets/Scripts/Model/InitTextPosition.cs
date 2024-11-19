using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitTextPosition : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public float distanceFromCamera = 5f;
    public Vector3 offset = Vector3.zero; // D�calage optionnel par rapport � la position cible
    public AudioSource audio;

    private Camera mainCamera;

    void Start()
    {
        audio.Play();
        if (cameraRig == null)
        {
            cameraRig = FindObjectOfType<OVRCameraRig>();
        }

        if (cameraRig != null)
        {
            mainCamera = cameraRig.centerEyeAnchor.GetComponent<Camera>();
        }

        Vector3 forwardDirection = mainCamera.transform.forward;
        forwardDirection.Normalize(); // Normalisation pour conserver une direction coh�rente

        // Position de l'objet � une distance donn�e dans la direction de la cam�ra
        Vector3 targetPosition = mainCamera.transform.position + forwardDirection * distanceFromCamera;

        // Ajuste la hauteur de l'objet � celle de la cam�ra + un offset
        targetPosition.y = mainCamera.transform.position.y + 6f;

        transform.position = targetPosition + offset;

        // Oriente l�objet pour qu�il soit toujours face � la cam�ra
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToCamera);
        //transform.Rotate(0, 180, 0); // Inverse pour �tre face � la cam�ra
    }
}
