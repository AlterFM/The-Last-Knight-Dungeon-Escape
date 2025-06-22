using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Kita buat variabel untuk maxHealth agar fleksibel
    public int maxHealth = 100;

    // Jadikan currentHealth privat agar tidak bisa diubah dari luar secara tidak sengaja
    private int currentHealth;

    void Start()
    {
        // Atur nyawa saat ini menjadi maksimum saat game dimulai
        currentHealth = maxHealth;

        // Update UI saat game dimulai dengan nilai yang benar
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdatePlayerHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        // Kurangi nyawa saat ini
        currentHealth -= damage;

        // Pastikan nyawa tidak menjadi negatif
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log("Player took damage! Sisa HP: " + currentHealth);
        // Panggil UIManager dengan nilai yang benar dan dinamis
        if (UIManager.instance != null)
        {
            // Gunakan variabel maxHealth, bukan angka 100
            UIManager.instance.UpdatePlayerHealth(currentHealth, maxHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player mati! Memanggil fungsi GameOver di GameManager...");

        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
        }
        gameObject.SetActive(false);
    }
}