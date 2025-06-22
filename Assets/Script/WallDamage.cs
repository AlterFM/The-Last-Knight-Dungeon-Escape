using UnityEngine;

public class WallDamage : MonoBehaviour
{
    public int damage = 999;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kalau PlayerHealth ada:
            PlayerHealth hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
            else
            {
                // Atau gunakan SendMessage jika belum punya PlayerHealth
                other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }

            Debug.Log("Player kena jebakan dinding!");
        }
    }
}
