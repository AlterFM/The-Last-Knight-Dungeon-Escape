using UnityEngine;

public class XPCollectible : MonoBehaviour
{
    public int xpValue = 25; // Jumlah XP yang diberikan
    public GameObject pickupEffect; // Opsional: Untuk efek partikel saat diambil

    // Fungsi ini berjalan otomatis saat sesuatu memasuki zona trigger
    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang masuk adalah objek dengan tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player picked up XP crystal!");
            // Cari GameManager dan panggil fungsinya untuk menambah XP
            if (GameManager.instance != null)
            {
                GameManager.instance.GainXP(xpValue);
            }
            // Jika ada efek visual, munculkan di posisi kristal ini
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }
            // Hancurkan objek kristal ini agar tidak bisa diambil lagi
            Destroy(gameObject);
        }
    }
}