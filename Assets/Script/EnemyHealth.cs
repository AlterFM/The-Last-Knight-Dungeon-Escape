using UnityEngine;
using UnityEngine.AI; // Diperlukan untuk mengakses NavMeshAgent

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 500;
    public int xpToGive = 500; 

    private int currentHealth;
    private Animator animator;
    private NavMeshAgent agent;
    private bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damage)
    {
        // Jangan lakukan apa-apa jika musuh sudah mati
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health is now " + currentHealth);

        // Panggil trigger ketika terkena hit
        if (animator != null)
        {
            animator.SetTrigger("isHit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.LogWarning(gameObject.name + " has been defeated!");

        // Beri XP ke pemain melalui GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.GainXP(xpToGive);
        }

        // Panggil trigger animasi kematian di Animator musuh
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }

        // Matikan kemampuan AI untuk bergerak
        if (agent != null)
        {
            agent.enabled = false;
        }



        // Matikan collider agar tidak bisa diserang/menabrak lagi
        GetComponent<Collider>().enabled = false;

        // Hancurkan objek musuh setelah 3 detik untuk memberi waktu animasi kematian
        Destroy(gameObject, 3f);
    }
}