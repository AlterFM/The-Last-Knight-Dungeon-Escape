using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossController : MonoBehaviour
{
    // Tambahkan 'Idle' ke dalam state AI
    public enum AIState { Idle, Patrolling, Chasing, Attacking }

    [Header("AI Settings")]
    public AIState currentState = AIState.Idle; // Mulai dari Idle agar lebih natural
    public Transform playerTarget;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public float patrolWaitTime = 3f; // Waktu diam di setiap titik patroli

    [Header("Combat Settings")]
    public float attackCooldown = 3f;
    // Hapus attackRange, kita akan gunakan stoppingDistance dari NavMeshAgent
    public GameObject weaponHitbox;

    [Header("Attack Timing")]
    public float attackWindUp = 0.5f;
    public float hitboxActiveDuration = 0.3f;

    // Variabel Privat
    private NavMeshAgent agent;
    private Animator animator;
    private float stateTimer; // Timer serbaguna untuk idle dan cooldown
    private int currentPointIndex = 0;
    private bool isCurrentlyAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateTimer = patrolWaitTime; // Set timer awal untuk memulai patroli pertama
        if (weaponHitbox != null) weaponHitbox.SetActive(false);
    }

    void Update()
    {
        // Jalankan logika untuk menentukan state apa yang seharusnya aktif
        HandleStateTransitions();
        // Jalankan fungsi berdasarkan state yang aktif
        ExecuteCurrentState();
        // Update animasi berdasarkan kecepatan NavMesh Agent
        UpdateAnimation();
    }

    void HandleStateTransitions()
    {
        if (isCurrentlyAttacking) return; // Jika sedang dalam sekuens serangan, jangan ubah state

        if (playerTarget != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            // Gunakan stoppingDistance dari NavMeshAgent sebagai referensi jarak serangan
            if (distanceToPlayer <= agent.stoppingDistance)
            {
                currentState = AIState.Attacking;
            }
            else
            {
                currentState = AIState.Chasing;
            }
        }
        else
        {
            // Jika tidak ada target, AI harus dalam kondisi Idle atau Patrolling
            if (currentState == AIState.Chasing || currentState == AIState.Attacking)
            {
                currentState = AIState.Idle;
                stateTimer = patrolWaitTime; // Mulai timer untuk patroli lagi
            }
        }
    }

    void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Patrolling:
                Patrol();
                break;
            case AIState.Chasing:
                Chase();
                break;
            case AIState.Attacking:
                Attack();
                break;
        }
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
    }

    // --- FUNGSI-FUNGSI LOGIKA STATE ---

    void Idle()
    {
        agent.isStopped = true; // Berhenti bergerak
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            GoToNextPoint();
            currentState = AIState.Patrolling;
        }
    }

    void Patrol()
    {
        agent.isStopped = false; // Pastikan bisa bergerak
        // Cek jika sudah dekat dengan tujuan patrolinya
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentState = AIState.Idle; // Berhenti sejenak di titik patroli
            stateTimer = patrolWaitTime;
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        if (playerTarget != null)
        {
            agent.SetDestination(playerTarget.position);
        }
    }

    void Attack()
    {
        if (isCurrentlyAttacking) return;
        StartCoroutine(AttackSequence());
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPointIndex].position);
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }

    IEnumerator AttackSequence()
    {
        isCurrentlyAttacking = true;
        agent.isStopped = true;
        transform.LookAt(playerTarget); // Hadap pemain

        int attackChoice = Random.Range(0, 2);
        if (attackChoice == 0) animator.SetTrigger("attack1");
        else animator.SetTrigger("attack2");

        yield return new WaitForSeconds(attackWindUp);
        if (weaponHitbox != null) weaponHitbox.SetActive(true);

        yield return new WaitForSeconds(hitboxActiveDuration);
        if (weaponHitbox != null) weaponHitbox.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        isCurrentlyAttacking = false;
    }

    // --- FUNGSI PUBLIK UNTUK MENERIMA PERINTAH DARI SENSOR ---
    public void OnPlayerDetected(Transform player)
    {
        playerTarget = player;
    }

    public void OnPlayerLost()
    {
        playerTarget = null;
    }
}