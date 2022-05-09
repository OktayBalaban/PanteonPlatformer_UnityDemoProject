using UnityEngine;

public class starObstacleMover : MonoBehaviour
{
    public float speed;

    // Position Vectors
    private Vector3 position1;
    private Vector3 position2;
    
    private enum positionStatus {pos1, pos2};
    positionStatus obstaclePosStatus;
    
    void Start()
    {
        if(transform.position.x == -13f)
        {

            obstaclePosStatus = positionStatus.pos1;
        }
        else
        {
            obstaclePosStatus = positionStatus.pos2;
        }

        position1 = new Vector3(-13, transform.position.y, transform.position.z);
        position2 = new Vector3(9, transform.position.y, transform.position.z);
    }

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
