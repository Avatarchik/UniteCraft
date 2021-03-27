using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [FormerlySerializedAs("CameraRotation")] public Transform cameraRotation;
    [FormerlySerializedAs("MouseSensitivity")] public float mouseSensitivity;
    private float xRotation;

    public void Update()
    {
        xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraRotation.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime));
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}