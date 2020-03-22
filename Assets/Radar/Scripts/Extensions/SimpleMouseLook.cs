using UnityEngine;

public class SimpleMouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 100.0f;
    [SerializeField]
    private float clampAngle = 80.0f;

    private float rotY = 0.0f; 
    private float rotX = 0.0f; 

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}
