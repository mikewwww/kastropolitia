using UnityEngine;
using UnityEngine.AI;

public class SimpleMove : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("Agent is not on NavMesh!");
        }
    }
}
