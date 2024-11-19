using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGhostPosition : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public float distanceFromCamera = 5f;
    public Vector3 offset = new Vector3(0, 2, 0); // Décalage optionnel par rapport à la position cible (ajustez la valeur en Y ici)

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
            forwardDirection.Normalize(); // Normalisation pour conserver une direction cohérente

            // Position de l'objet à une distance donnée dans la direction de la caméra
            Vector3 targetPosition = mainCamera.transform.position + forwardDirection * distanceFromCamera + offset;

            transform.position = targetPosition;

            // Oriente l’objet pour qu’il soit toujours face à la caméra
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, -90, 0); // Inverse pour être face à la caméra
        }

        StartCoroutine(WaitAndLoadScene(5f, "Enigma1"));
    }

    private IEnumerator WaitAndLoadScene(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
