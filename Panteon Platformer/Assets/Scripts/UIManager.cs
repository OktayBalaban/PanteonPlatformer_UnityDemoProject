using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI controlling variables
    private enum ActiveUI {stage1, stage2};
    private ActiveUI activeUI;

    // Wall Painting Variables
    private GameObject gridManager;
    private double percentagePainted;
    public Text percentageText;

    // Race Position Variables
    float playerLocation;
    float checkingLocation;
    int playerPosition;




    // Start is called before the first frame update
    void Start()
    {
        // Start with the first stage UI
        activeUI = ActiveUI.stage1;

        positionFinder();

        // Get the Grid Manager for percentage painted updates
        gridManager = GameObject.Find("GridHolder");


    }

    // Update is called once per frame
    void Update()
    {
        if (activeUI == ActiveUI.stage1)
        {
            // Check and update the position
            playerPosition = positionFinder();

            // Update the text
            percentageText.text = "POSITION: " + playerPosition + "/11";
        }
        else if(activeUI == ActiveUI.stage2)
        {
            // Get the information of how much the wall painted and store it in percentage painted
            percentagePainted = gridManager.GetComponent<GridManager>().percentagePainted;

            // Update the text
            percentageText.text = "PERCENTAGE PAINTED: " + percentagePainted + "%";

        }
    }

    public void switchToStageTwo()
    {
        activeUI = ActiveUI.stage2;
    }

    int positionFinder()
    {
        int tempPlayerPosition = 1;
        playerLocation = GameObject.Find("Player").transform.position.z;

        for (int i = 1; i < 11; i++)
        {
            string AIName = "AI" + i;
            checkingLocation = GameObject.Find("AIPlayers").transform.Find(AIName).position.z;

            // Increment player position for every AI ahead of him
            if (checkingLocation > playerLocation)
            {
                tempPlayerPosition++;
            }
        }

        return tempPlayerPosition;
    }
}
