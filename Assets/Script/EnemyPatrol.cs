using UnityEngine;
using UnityEngine.AI; 

public class EnemyPatrol : MonoBehaviour
{
    public enum AIState { Patrolling, Chasing }

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;

    [Header("AI Settings")]
    public AIState currentState = AIState.Patrolling; 

    private NavMeshAgent agent;
    private Animator animator;
    private Transform playerTarget; 
    private int currentPointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (patrolPoints == null || patrolPoints.Length < 2)
        {
            Debug.LogError("Patrol points not set up correctly!");
            this.enabled = false;
        }
    }

    void Update()
    {
        // Switch statement untuk menjalankan logika berdasarkan state saat ini
        switch (currentState)
        {
            case AIState.Patrolling:
                Patrol();
                break;
            case AIState.Chasing:
                ChasePlayer();
                break;
        }

        // Update animasi berdasarkan kecepatan agent
        if (animator != null)
        {
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
    }

    void Patrol()
    {
        // Atur tujuan agent ke titik patroli saat ini
        agent.SetDestination(patrolPoints[currentPointIndex].position);

        // Cek jika sudah dekat dengan tujuan, lalu pindah ke titik berikutnya
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        // Mengejar ke lokasi pemain yang ditargetkan
        if (playerTarget != null)
        {
            agent.SetDestination(playerTarget.position);
        }
    }

    // --- LOGIKA PENDETEKSI PEMAIN ---
    public void StartChasing(Transform player)
    {
        Debug.LogWarning("AI Brain received command to CHASE " + player.name);
        playerTarget = player;
        currentState = AIState.Chasing;
    }

    public void StopChasing()
    {
        Debug.LogWarning("AI Brain received command to RETURN TO PATROL");
        playerTarget = null;
        currentState = AIState.Patrolling;
    }
    public int damageAmount = 200; 

    // Collider bertabrakan dengan pemain
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " collided with the Player!");

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // ...panggil fungsinya untuk memberikan damage.
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}