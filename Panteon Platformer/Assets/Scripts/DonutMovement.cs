using UnityEngine;

public class DonutMovement : MonoBehaviour
{
    public float speed;
    public float waitTime;
    private float waitTimeCounter;
    
    // Postion vectors
    private Vector3 donutStartingPosition;
    private Vector3 position1;
    private Vector3 position2;

    // Donuts will move towards 2 positions, but also have a starting point
    private enum positionStatus {Start, pos1, pos2 };
    positionStatus donutPosStatus;

    void Start()
    {
        // Get the donut's start position
        donutPosStatus = positionStatus.Start;
        donutStartingPosition = transform.position;

        position1 = donutStartingPosition - new Vector3(2, 0, 0);
        position2 = donutStartingPosition + new Vector3(2, 0, 0);
    }

    void Update()
    {
        if (waitTimeCounter < 0)
        {
            if (donutPosStatus == positionStatus.Start || donutPosStatus == positionStatus.pos2)
            {
                if (Vector3.Distance(transform.position, position1) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, position1, speed * Time.deltaTime);
                }
                else
                {
                    donutPosStatus = positionStatus.pos1;
                    waitTimeCounter = waitTime;
                }
            }

            else if (donutPosStatus == positionStatus.pos1)
            {
                if (Vector3.Distance(transform.position, position2) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, position2, speed * Time.deltaTime);
                }
                else
                {
                    donutPosStatus = positionStatus.pos2;
                    waitTimeCounter = waitTime;
                }
            }
        }
        else
        {
            waitTimeCounter -= Time.deltaTime;
        }
    }
}
