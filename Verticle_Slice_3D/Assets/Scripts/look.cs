using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour
{
    public float sens = 100f;
    public Transform Body;
    float xrotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens;
        float mouseY = Input.GetAxis("Mouse Y") * sens;

        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);

        Body.Rotate(Vector3.up * mouseX);
    }

}
