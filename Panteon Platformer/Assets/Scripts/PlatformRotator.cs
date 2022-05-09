using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    // Variable to control rotation
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
