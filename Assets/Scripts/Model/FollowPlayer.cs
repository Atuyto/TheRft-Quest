using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public OVRCameraRig mainCameraRig;
    public Vector3 positionOffset = new Vector3(0, 0, 5);
    public Vector3 rotationOffset = new Vector3(0, 0, 0);

    private Camera mainCamera;

    void Start()
    {
        if (mainCameraRig == null)
        {
            mainCameraRig = FindObjectOfType<OVRCameraRig>();
        }

        if (mainCameraRig != null)
        {
            mainCamera = mainCameraRig.centerEyeAnchor.GetComponent<Camera>();
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            Transform centerEyeCamera = mainCameraRig.centerEyeAnchor;

            if (centerEyeCamera != null)
            {
                Vector3 targetPosition = centerEyeCamera.position;
                targetPosition.x = mainCameraRig.transform.position.x + positionOffset.x + mainCameraRig.centerEyeAnchor.position.x ;
                targetPosition.z = mainCameraRig.transform.position.z + positionOffset.z + mainCameraRig.centerEyeAnchor.position.z;
                
                
                transform.position = targetPosition;

                transform.rotation = Quaternion.Euler(rotationOffset);
            }
        }
    }
}
