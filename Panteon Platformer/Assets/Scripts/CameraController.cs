using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera Managing Variables
    public GameObject TPSCamera;
    public GameObject paintRaycastCamera;
    public GameObject Stage2Camera;

    private enum selectedCamera { tps, fps};
    private selectedCamera activeCamera;


    // TPS Camera Variables
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;


    // FPS Camera Variables
    float mouseSensitivity = 1f;
    public Transform player;

    void Start()
    {
        // Hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        activeCamera = selectedCamera.tps;

        TPSCamera.SetActive(true);
        paintRaycastCamera.SetActive(false);

        if (!useOffsetValues && activeCamera == selectedCamera.tps)
        {
            offset = target.position - transform.position;
        }
    }

    // LateUpdate is used for better camera performance
    void LateUpdate()
    {
        if (activeCamera == selectedCamera.tps)
        {
            TPSCamera.transform.position = target.position - offset;

            // Fix camera x axis
            TPSCamera.transform.position = new Vector3(-2, TPSCamera.transform.position.y, TPSCamera.transform.position.z);
        }
        else if (activeCamera == selectedCamera.fps)
        {
            // Mouse Input 
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;


            // Up and Down
            paintRaycastCamera.transform.position = new Vector3(paintRaycastCamera.transform.position.x + mouseX, paintRaycastCamera.transform.position.y + mouseY, paintRaycastCamera.transform.position.z);
        }
    }

    public void SwitchToFPS()
    {
        activeCamera = selectedCamera.fps;

        TPSCamera.SetActive(false);
        Stage2Camera.SetActive(true);
    }
}
