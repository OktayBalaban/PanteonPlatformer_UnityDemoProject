using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera Managing Variables
    public GameObject TPSCamera;
    public GameObject FPSCamera;

    private enum selectedCamera { tps, fps};
    private selectedCamera activeCamera;


    // TPS Camera Variables
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;


    // FPS Camera Variables
    float rotationOnX;
    float mouseSensitivity = 180f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        activeCamera = selectedCamera.tps;

        TPSCamera.SetActive(true);
        FPSCamera.SetActive(false);

        if (!useOffsetValues && activeCamera == selectedCamera.tps)
        {
            offset = target.position - transform.position;
        }

    }

    // Update is called once per frame
    // LateUpdate is used for better camera performance
    void LateUpdate()
    {
        if (activeCamera == selectedCamera.tps)
        {
            TPSCamera.transform.position = target.position - offset;
        }
        else if (activeCamera == selectedCamera.fps)
        {
            // Mouse Input 
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;


            // Up and Down
            rotationOnX -= mouseY;
            rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90f);
            FPSCamera.transform.localEulerAngles = new Vector3(rotationOnX, 0f, 0f);

            // Left and Right
            player.Rotate(Vector3.up * mouseX);

        }

    }

    public void SwitchToFPS()
    {
        activeCamera = selectedCamera.fps;

        TPSCamera.SetActive(false);
        FPSCamera.SetActive(true);

    }

}
