using UnityEngine;

public class EnemyHitReceiver : MonoBehaviour
{
    // Hubungkan ke script EnemyHealth yang ada di objek yang sama
    private EnemyHealth myHealth;

    void Start()
    {
        myHealth = GetComponent<EnemyHealth>();
    }

    // Fungsi ini akan berjalan saat ada TRIGGER LAIN yang memasukinya
    private void OnTriggerEnter(Collider other)
    {
        // Kita cek apakah yang menyentuh kita adalah SwordHitbox dari pemain
        if (other.CompareTag("PlayerWeapon")) // Kita akan menggunakan Tag baru
        {
            Debug.LogWarning("Enemy has been hit by Player's Weapon!");

            // Ambil script DamageHitbox dari hitbox tersebut untuk mengetahui damage-nya
            DamageHitbox hitbox = other.GetComponent<DamageHitbox>();
            if (myHealth != null && hitbox != null)
            {
                // Suruh diri sendiri untuk menerima damage
                myHealth.TakeDamage(hitbox.damage);
            }
        }
    }
}