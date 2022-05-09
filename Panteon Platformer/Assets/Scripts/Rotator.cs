using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 50f;
    public float knockbackForce;

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
