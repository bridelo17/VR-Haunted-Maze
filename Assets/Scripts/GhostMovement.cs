using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Player transform (for position)
    public Transform playerCamera; // Camera transform (for viewing direction)
    private NavMeshAgent navMeshAgent;

    // --- New Speed Variable ---
    [Tooltip("The movement speed of the ghost.")]
    public float ghostSpeed = 3.5f; 

    [Range(-1f, 1f)]
    public float visibilityDotThreshold = 0.5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        // --- Apply Speed to NavMeshAgent ---
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = ghostSpeed;
        }
    }

    void Update()
    {
        if (player == null || playerCamera == null) return;

        Vector3 toEnemy = (transform.position - player.position).normalized;
        Vector3 playerForward = playerCamera.forward;

        float dot = Vector3.Dot(playerForward, toEnemy);

        if (dot < visibilityDotThreshold)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            navMeshAgent.ResetPath();
        }
    }
}


