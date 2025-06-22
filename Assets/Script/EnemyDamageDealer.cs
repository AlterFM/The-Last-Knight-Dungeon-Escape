using UnityEngine;
using System.Collections.Generic;

public class EnemyDamageDealer : MonoBehaviour
{
    public int damage = 25; // Damage dari serangan boss
    private List<Collider> collidersHit;
    public LayerMask hittableLayers;

    void OnEnable()
    {
        // Reset daftar setiap kali hitbox diaktifkan
        collidersHit = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Boss Hitbox Touched: " + other.gameObject.name + " | Tag: " + other.tag + " | Layer: " + LayerMask.LayerToName(other.gameObject.layer));
        if (collidersHit.Contains(other)) return; // Sudah kena dalam ayunan ini, abaikan

        // Cek jika yang disentuh adalah pemain
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Beri damage ke pemain
                playerHealth.TakeDamage(damage);
                collidersHit.Add(other); // Catat agar tidak kena double hit
            }
        }
    }

}