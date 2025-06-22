using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    // Jumlah damage yang akan diberikan saat bersentuhan.
    // Kita bisa mengaturnya di Inspector untuk tiap jebakan/musuh yang berbeda.
    public int damageAmount = 999; // Angka 999 adalah cara mudah untuk 'instant kill'.

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