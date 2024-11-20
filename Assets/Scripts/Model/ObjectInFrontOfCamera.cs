using UnityEngine;

public class ObjectInFrontOfCamera : MonoBehaviour
{
    public OVRCameraRig mainCameraRig;
    public Vector3 positionOffset = new Vector3(0, 0, 5); 
    public Vector3 rotationOffset = new Vector3(0, 0, 0);

    void Start()
    {
        if (mainCameraRig == null)
        {
            mainCameraRig = FindObjectOfType<OVRCameraRig>();
        }

        if (mainCameraRig != null)
        {
            Transform centerEyeCamera = mainCameraRig.centerEyeAnchor;

            if (centerEyeCamera != null)
            {
                Vector3 targetPosition = centerEyeCamera.position + centerEyeCamera.forward * positionOffset.z +
                                         centerEyeCamera.right * positionOffset.x +
                                         centerEyeCamera.up * positionOffset.y;

                transform.position = targetPosition;

                transform.rotation = Quaternion.Euler(rotationOffset);
            }
        }
    }
}
