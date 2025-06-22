using UnityEngine;
using System.Collections.Generic; // Wajib ada untuk menggunakan List<>

public class DamageHitbox : MonoBehaviour
{
    public int damage = 50;
    public LayerMask hittableLayers;

    // Daftar untuk mencatat siapa saja yang sudah dipukul dalam ayunan ini
    private List<Collider> collidersHit;

    void OnEnable()
    {
        // Setiap kali hitbox diaktifkan, buat daftar baru yang kosong
        // Ini me-reset catatan untuk setiap ayunan baru
        collidersHit = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika layer objek yang ditabrak ada di dalam daftar hittableLayers
        if ((hittableLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            // --- PENGECEKAN BARU ---
            // Cek apakah kita sudah pernah memukul collider ini dalam ayunan yang sama
            if (collidersHit.Contains(other))
            {
                // Jika sudah, abaikan dan jangan lakukan apa-apa
                return;
            }
            // --- AKHIR DARI PENGECEKAN BARU ---

            // Jika belum pernah dipukul, tambahkan ke daftar
            collidersHit.Add(other);

            Debug.Log("Hitbox collided with a hittable object: " + other.name);

            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                // Kita tidak lagi menonaktifkan hitbox di sini, biarkan animasi yang mengaturnya
                // gameObject.SetActive(false); // HAPUS BARIS INI JIKA ADA
            }
        }
    }
}