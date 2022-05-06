using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starObstacleMover : MonoBehaviour
{
    public float speed;

    // Position Vectors
    private Vector3 position1;
    private Vector3 position2;
    
    private enum positionStatus {pos1, pos2};
    positionStatus obstaclePosStatus;
    

    // Start is called before the first frame update
    void Start()
    {
        if(transform.position.z == -49f)
        {

            obstaclePosStatus = positionStatus.pos1;
        }
        else
        {

            obstaclePosStatus = positionStatus.pos2;
        }

        position1 = new Vector3(transform.position.x, transform.position.y, -50f);
        position2 = new Vector3(transform.position.x, transform.position.y, -60f);
    }

    // Update is called once per frame
    void Update()
    {
        if (obstaclePosStatus == positionStatus.pos1)
        {
            if(Vector3.Distance(transform.position, position2) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, position2, speed * Time.deltaTime);
            }
            else
            {
                obstaclePosStatus = positionStatus.pos2;
            }
        }

        else if (obstaclePosStatus == positionStatus.pos2)
        {
            if (Vector3.Distance(transform.position, position1) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, position1, speed * Time.deltaTime);
            }
            else
            {
                obstaclePosStatus = positionStatus.pos1;
            }
        }
    }
}
