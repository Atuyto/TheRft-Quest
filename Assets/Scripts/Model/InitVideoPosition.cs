using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class InitVideoPosition : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public float distanceFromCamera = 5f;
    public Vector3 offset = Vector3.zero; // D�calage optionnel par rapport � la position cible
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
        forwardDirection.Normalize(); // Normalisation pour conserver une direction coh�rente

        // Position de l'objet � une distance donn�e dans la direction de la cam�ra
        Vector3 targetPosition = mainCamera.transform.position + forwardDirection * distanceFromCamera;

        // Ajuste la hauteur de l'objet � celle de la cam�ra + un offset
        //targetPosition.y = mainCamera.transform.position.y + 2f;

        transform.position = targetPosition + offset;

        // Oriente l�objet pour qu�il soit toujours face � la cam�ra
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToCamera);
        //transform.Rotate(0, 180, 0); // Inverse pour �tre face � la cam�ra


        if (video != null)
        {
            video.loopPointReached += OnVideoEnd; // Abonne l'�v�nement � la m�thode.
        }
    }

    // Nouvelle coroutine pour attendre avant de charger la sc�ne
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
