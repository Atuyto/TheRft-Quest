using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class InitVideoPosition : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public float distanceFromCamera = 5f;
    public Vector3 offset = Vector3.zero; // Décalage optionnel par rapport à la position cible
    public VideoPlayer video;

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

        Vector3 forwardDirection = mainCamera.transform.forward;
        forwardDirection.y = 0; // Nous ignorons la composante verticale pour ne pas affecter l'inclinaison
        forwardDirection.Normalize(); // Normalisation pour conserver une direction cohérente

        // Position de l'objet à une distance donnée dans la direction de la caméra
        Vector3 targetPosition = mainCamera.transform.position + forwardDirection * distanceFromCamera;

        // Ajuste la hauteur de l'objet à celle de la caméra + un offset
        //targetPosition.y = mainCamera.transform.position.y + 2f;

        transform.position = targetPosition + offset;

        // Oriente l’objet pour qu’il soit toujours face à la caméra
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToCamera);
        //transform.Rotate(0, 180, 0); // Inverse pour être face à la caméra


        if (video != null)
        {
            video.loopPointReached += OnVideoEnd; // Abonne l'événement à la méthode.
        }
    }

    // Nouvelle coroutine pour attendre avant de charger la scène
    private IEnumerator WaitAndLoadScene(float delay, string sceneName)
    {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);

    }

    void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(WaitAndLoadScene(2f, "Prologue"));
    }
}
