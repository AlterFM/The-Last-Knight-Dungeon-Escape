using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player took damage! Sisa HP: " + health);

        if (health <= 0)
        {
            Debug.Log("Player mati! Memanggil fungsi GameOver di GameManager...");
 
            // 1. Panggil fungsi GameOver di GameManager
            // Script GameManager dengan pola Singleton (instance)
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }

            // 2. Sembunyikan objek pemain dan matikan semua scriptnya
            gameObject.SetActive(false);
        }
    }
}
