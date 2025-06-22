using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Slider hpSlider;
    private Transform cameraToLookAt;

    void Start()
    {
        // Cari kamera utama secara otomatis saat objek ini dibuat
        if (Camera.main != null)
        {
            cameraToLookAt = Camera.main.transform;
        }
    }

    // LateUpdate digunakan agar posisi UI diperbarui setelah semua gerakan karakter selesai
    void LateUpdate()
    {
        if (cameraToLookAt != null)
        {
            // Membuat HP bar selalu menghadap ke kamera
            transform.LookAt(transform.position + cameraToLookAt.forward);
        }
    }

    // Fungsi publik ini akan dipanggil oleh EnemyHealth untuk mengupdate bar
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
    }
}