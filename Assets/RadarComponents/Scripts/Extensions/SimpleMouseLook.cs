// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj
using UnityEngine;

/// <summary>
/// Simple Mouse Look script
/// </summary>
public class SimpleMouseLook : MonoBehaviour
{
    [SerializeField]
    private bool isCursorLocked = false;
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

        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.fixedDeltaTime;
        rotX += mouseY * mouseSensitivity * Time.fixedDeltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}
