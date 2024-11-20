using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGhostPosition : MonoBehaviour
{
    public OVRCameraRig mainCameraRig;
    public Vector3 positionOffset = new Vector3(0, 0, 5); // Position offset from the camera
    public Vector3 rotationOffset = new Vector3(0, 0, 0); // Rotation offset

    void Start()
    {
        if (mainCameraRig == null)
        {
            mainCameraRig = FindObjectOfType<OVRCameraRig>();
        }

        if (mainCameraRig != null)
        {
            // Get the center eye camera
            Transform centerEyeCamera = mainCameraRig.centerEyeAnchor;

            if (centerEyeCamera != null)
            {
                // Calculate the position in front of the center eye camera
                Vector3 targetPosition = centerEyeCamera.position + centerEyeCamera.forward * positionOffset.z +
                                         centerEyeCamera.right * positionOffset.x +
                                         centerEyeCamera.up * positionOffset.y;

                // Set the object's position
                transform.position = targetPosition;

                // Set the object's rotation
                transform.rotation = Quaternion.Euler(rotationOffset);
            }
        }
        StartCoroutine(WaitAndLoadScene(5f, "Enigma1"));
    }

    private IEnumerator WaitAndLoadScene(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
