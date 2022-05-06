using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private float gridWidth;
    private float gridHeight;

    private int gridColumnNumber;
    private int gridRowNumber;
    private Vector3 wallPos;

    private float wallWidth, wallHeight;
    private float wallLeftBound;
    private float wallBottomBound;

    private GameObject parentGrid;
    private GameObject[,] gridArray;
    RaycastHit[] hit;
    [SerializeField] public GameObject prefabPlane;

    public float paintCounter;
    private float totalGrid;
    public double percentagePainted;

    public Camera FPSCamera;
    public bool isActive;

    public bool isVictoryStarted;


    void Start()
    {
        isActive = false;
        isVictoryStarted = false;
        percentagePainted = 0;
        parentGrid = GameObject.Find("GridHolder");

        // Number of grids for paintingWall
        gridColumnNumber = 100   ;
        gridRowNumber = 40;

        // Variables for real time information on the percentage of wall paint
        paintCounter = 0;
        totalGrid = gridColumnNumber * gridRowNumber;

        gridArray = new GameObject[gridColumnNumber, gridRowNumber];

        gridWidth = 0.045f;
        gridHeight = 0.045f;

        wallLeftBound = -3.5f;
        wallBottomBound = 10f;

        for (int y= 0; y < gridRowNumber; y++)
        {
            for (int x = 0; x < gridColumnNumber; x++)
            {
                gridArray[x, y] = Instantiate(prefabPlane, new Vector3(wallLeftBound + gridWidth / 2 + gridWidth * x, wallBottomBound + gridHeight / 2 + gridHeight * y, 107), Quaternion.identity);
                gridArray[x, y].transform.Rotate(270, 0, 0);
                gridArray[x, y].transform.parent = parentGrid.transform;
                gridArray[x, y].name = "plane (" + x + "," + y + ")";
            }
        }

    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButton(0))
            {
                hit = Physics.SphereCastAll(FPSCamera.transform.position, 0.1f, FPSCamera.transform.forward, 5f);
                foreach (RaycastHit obj in hit)
                {
                    // Paint the plane red and increment the counter by 1 for every newly painted plane hit by the SphereCast
                    if (obj.collider.gameObject.tag == "PaintablePlane" && !obj.collider.gameObject.GetComponent<Painter>().isPainted)
                    {
                        obj.collider.gameObject.GetComponent<Painter>().paintRed();
                        obj.collider.gameObject.GetComponent<Painter>().isPainted = true;
                        paintCounter++;

                        // Calculating the percentage painted
                        percentagePainted = Math.Floor((paintCounter / totalGrid) * 100);
                        Debug.Log(percentagePainted + "%");
                    }
                }     
            }

            // Switch the music at the end
            if (percentagePainted == 100 && !isVictoryStarted)
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().SwitchMusic();
                isVictoryStarted = true;
            }
        }



    }

    // Activates the planes isActive mode for second stage
    public void planeActivator()
    {
        isActive = true;
    }
}
