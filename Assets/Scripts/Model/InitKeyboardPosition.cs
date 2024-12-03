using UnityEngine;

public class InitKeyboardPosition : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public float distanceFromCamera = 5f;
    public Vector3 offset = Vector3.zero; // Décalage optionnel


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
    }

    public void Update()
    {
        if (mainCamera == null) return;

        Vector3 forwardDirection = mainCamera.transform.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();

        Vector3 targetPosition = mainCamera.transform.position + forwardDirection * distanceFromCamera;
        targetPosition.y = mainCamera.transform.position.y + -0.3f;

        transform.position = targetPosition;

        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}

