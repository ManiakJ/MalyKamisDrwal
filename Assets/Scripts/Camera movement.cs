using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;

    private float xRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //patrzenie gora dol
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //pelny obrot 360 lewo prawo
        if (playerBody != null)
        {
            playerBody.Rotate(0f, mouseX, 0f, Space.Self);
        }
    }
}

