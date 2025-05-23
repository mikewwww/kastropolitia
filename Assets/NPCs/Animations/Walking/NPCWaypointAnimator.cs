using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCWaypointSmartPause : MonoBehaviour
{
    public Transform[] waypoints;
    public int[] animationStates; // π.χ. 0 = Walk, 1 = Idle
    public float waitTime = 5f;

    private int currentIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (!isWaiting && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Αν το state είναι διαφορετικό από Walk, περιμένει.
            if (animationStates[currentIndex] != 0)
            {
                StartCoroutine(WaitAndAnimate());
            }
            else
            {
                // Αλλιώς, προχωράει αμέσως στο επόμενο.
                currentIndex = (currentIndex + 1) % waypoints.Length;
                MoveToNextWaypoint();
            }
        }
    }

    IEnumerator WaitAndAnimate()
    {
        isWaiting = true;

        agent.isStopped = true;
        animator.SetInteger("State", animationStates[currentIndex]);

        yield return new WaitForSeconds(waitTime);

        currentIndex = (currentIndex + 1) % waypoints.Length;
        MoveToNextWaypoint();

        isWaiting = false;
    }

    void MoveToNextWaypoint()
    {
        animator.SetInteger("State", 0); // 0 = Walk
        agent.SetDestination(waypoints[currentIndex].position);
        agent.isStopped = false;
    }
}
