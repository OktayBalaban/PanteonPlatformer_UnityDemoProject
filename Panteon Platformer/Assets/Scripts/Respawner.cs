using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    private bool isRespawning;
    private Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "KillerObstacle")
        {
            Debug.Log("Killed");
            respawn();
        }

    }

    private void respawn()
    {
        transform.position = new Vector3(respawnPoint.x, respawnPoint.y, respawnPoint.z);
    }
}
