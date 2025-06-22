using UnityEngine;
using UnityEngine.AI; // Pastikan ini ada

public class EnemyPatrol : MonoBehaviour
{
    // Mendefinisikan state yang bisa dimiliki oleh AI
    public enum AIState { Patrolling, Chasing }

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;

    [Header("AI Settings")]
    public AIState currentState = AIState.Patrolling; // State awal musuh

    private NavMeshAgent agent;
    private Animator animator;
    private Transform playerTarget; // Untuk menyimpan referensi ke transform pemain
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
        // Jika target pemain ada, kejar terus posisinya
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
    public int damageAmount = 200; // Angka 999 adalah cara mudah untuk 'instant kill'.

    // Fungsi ini akan berjalan secara otomatis saat collider objek ini
    // BERSENTUHAN FISIK dengan collider objek lain.
    private void OnCollisionEnter(Collision collision)
    {
        // Pertama, kita cek apakah objek yang kita tabrak memiliki tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " collided with the Player!");

            // Kedua, kita coba ambil komponen PlayerHealth dari objek Player yang kita tabrak.
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Ketiga, jika komponen PlayerHealth berhasil ditemukan...
            if (playerHealth != null)
            {
                // ...panggil fungsinya untuk memberikan damage.
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}