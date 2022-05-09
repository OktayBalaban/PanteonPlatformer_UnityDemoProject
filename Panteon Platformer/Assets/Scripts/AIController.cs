using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // Core Variables
    public NavMeshAgent agent;
    private Vector3 destination;

    // Respawn Variables
    private Vector3 respawnPoint;
    public float respawnLength;
    public float respawnEffectLength;
    private bool isRespawning;

    // Animation Variables
    public Animator anim;
    public bool isRunning;
    public bool isBackwards;
    private Vector3 movement;
    private Vector3 previousPos;
    private Vector3 newPos;
    private Vector3 forward;

    // Rendering Variables
    public GameObject renderingObj1;
    public GameObject renderingObj2;

    void Start()
    {
        // Set destination point
        destination = new Vector3(transform.position.x * 0.5f, 9.15f, 102f);

        // Set respawn point
        respawnPoint = transform.position;
        isRespawning = false;

        // Initialize previous pos and forward
        previousPos = transform.position;
        forward = transform.forward;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, destination) > 1)
        {
            // Move the AI
            agent.SetDestination(destination);

            // Determine if the AI character moves backwards
            determineBackwards();
        }
        
        // Animation Controls
        if (agent.velocity.magnitude > 1)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Update animation parameters
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isBackwards", isBackwards);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillerObstacle")
        {
            transform.position = respawnPoint;
            respawn();
        }
    }

    public void respawn()
    {
        if (!isRespawning)
        {
            Debug.Log("Coroutine Started");
            StartCoroutine("respawnCo");
            StartCoroutine("respawningEffectCo");
        }

        // Respawning Effect
    }

    public IEnumerator respawnCo()
    {
        Debug.Log("Coroutine respawnCo entered");
        // Player is disappeared

        isRespawning = true;

        yield return new WaitForSeconds(respawnLength);
        // Player is reappeared
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

    // Checks if the AI going backwards then return a bool value for it
    private void determineBackwards()
    {
        newPos = transform.position;
        movement = newPos - previousPos;

        if(Vector3.Dot(forward, movement) < 0)
        {
            isBackwards = true;
        }
        else
        {
            isBackwards = false;
        }

        // Update previous pos for next frame
        previousPos = newPos;
    }
}
