using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Controller Variables
    public float moveSpeed;
    public CharacterController controller;
    public float gravityScale;
    private Vector3 moveDirection;

    // Variable to control drag on rotating platforms
    private enum DragStatus {no, right, left};
    private DragStatus dragStatus;
    public float platformDragSpeed;

    // Respawn Variables
    private Vector3 respawnPoint;
    public float respawnLength;
    public float respawnEffectLength;
    private bool isRespawning;

    // KnockBackVariables
    public float knockbackForce;
    public float knockbackTime;
    private float knockbackCounter;

    // Stage Variables
    private enum stageStatus { stage1, transition, stage2 }
    private stageStatus stage;
    private Vector3 stageTwoTargetPosition;
    public GameObject cameraController;
    public GameObject gridManager;

    // Animation Variables
    public Animator anim;

    // Camera and Rendering
    public CameraController cameraScript;
    public GameObject renderingObj1;
    public GameObject renderingObj2;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraController = GameObject.Find("CameraController");
        cameraScript = cameraController.GetComponent<CameraController>();
        knockbackCounter = 0;
        
        respawnPoint = transform.position; 

        dragStatus = DragStatus.no;

        stage = stageStatus.stage1;

        gridManager = GameObject.Find("GridHolder");

        isRespawning = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Moving the player is only possible in stage 1

        if (stage == stageStatus.stage1 && !isRespawning)
        {
            if(knockbackCounter <= 0)
            {
                float yStore = moveDirection.y;

                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + transform.right * Input.GetAxis("Mouse X");
                moveDirection = moveDirection.normalized * moveSpeed;
                moveDirection.y = yStore;
            }
            else
            {
                knockbackCounter -= Time.deltaTime;
            }

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);


            // Platform drag controller
            if (dragStatus == DragStatus.right)
            {
                transform.position = new Vector3(transform.position.x + platformDragSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (dragStatus == DragStatus.left)
            {
                transform.position = new Vector3(transform.position.x - platformDragSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }

        }

        // Character will move to the second stage by the computer
        else if (stage == stageStatus.transition)
        {
            if (Vector3.Distance(transform.position,stageTwoTargetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, stageTwoTargetPosition, 5f * Time.deltaTime);
            }
            else
            {
                transitionCompletion();
            }
        }

        // Animations
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Mouse X"))));
        anim.SetBool("isGrounded", controller.isGrounded);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "PlatformLeft")
        {
            dragStatus = DragStatus.left;
        }
        else if (other.gameObject.tag == "PlatformRight")
        {
            dragStatus = DragStatus.right;
        }
        else if (other.gameObject.tag == "KillerObstacle")
        {
            controller.enabled = false;
            transform.position = respawnPoint;
            controller.enabled = true;
            respawn();

        }
        else if (other.gameObject.tag == "Finish")
        {
            stageTwoTargetPosition = new Vector3(-1.2f, transform.position.y, 104f);
            stage = stageStatus.transition;
        }
        else if (other.gameObject.tag == "RotatorStick")
        {
            // Calculating the angle between the stick, the rotator and the player on Y plane
            knockbackCounter = knockbackTime;
            knockback(other.transform.gameObject);

        }
    }

    public void respawn()
    {

        if (!isRespawning)
        {
            StartCoroutine("respawnCo");
            StartCoroutine("respawningEffectCo");
        }

    }

    public IEnumerator respawnCo()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnLength);
        isRespawning = false;
    }

    public IEnumerator respawningEffectCo()
    {
        for (int i = 0; i < 10; i++)
        {
            renderingObj1.GetComponent<Renderer>().enabled = false;
            renderingObj2.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(respawnEffectLength);
            renderingObj2.GetComponent<Renderer>().enabled = true;
            renderingObj2.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(respawnEffectLength);
        }
    }

        void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlatformLeft" || other.gameObject.tag == "PlatformRight")
        {
            dragStatus = DragStatus.no;

        }
    }

    // Executes camera and stage change
    void transitionCompletion()
    {
        // Activate planes
        gridManager.GetComponent<GridManager>().planeActivator();
        
        // Activate stage2 controls
        stage = stageStatus.stage2;

        // Switch to FPS camera
        cameraScript.SwitchToFPS();

        // Switch to Wall Painting UI
        GameObject.Find("UIGame").GetComponent<UIManager>().switchToStageTwo();
    }

    public void knockback(GameObject stick)
    {
        knockbackCounter = knockbackTime;

        Vector3 direction = stick.transform.forward;
        Vector3 rotatedDirection = new Vector3(direction.z, direction.y, -direction.x);
        rotatedDirection.y = 0.5f;

        moveDirection = rotatedDirection * knockbackForce;

    }
}
