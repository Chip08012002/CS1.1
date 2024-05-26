using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // public float mouseSensitivity = 200f;

    // float xRotate;
    // float yRotate;

    // public float topClamp = -90f;
    // public float bottomClamp = 90f;


    // void Start()
    // {
    //     Cursor.lockState = CursorLockMode.Locked;
    // }

    // void Update()
    // {
    //     float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
    //     float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

    //     xRotate -= mouseY;
    //     xRotate = Mathf.Clamp(xRotate, topClamp, bottomClamp);

    //     yRotate += mouseX;

    //     transform.localRotation = Quaternion.Euler(xRotate, yRotate, 0);
    // }

    public float mouseSensitivity = 500f;
    float xRotate;
    float yRotate;

    float topClamp = -90f;
    float bottomClamp = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, topClamp, bottomClamp);
        yRotate += mouseX;

        transform.localRotation = Quaternion.Euler(xRotate, yRotate, 0f);
    }
}
