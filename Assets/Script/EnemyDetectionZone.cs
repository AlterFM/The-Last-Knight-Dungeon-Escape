using UnityEngine;

public class EnemyDetectionTrigger : MonoBehaviour
{
    // Variabel untuk menampung referensi ke otak AI utama di induknya
    public EnemyPatrol mainAI;

    private void OnTriggerEnter(Collider other)
    {
        // Jika yang masuk adalah pemain...
        if (other.CompareTag("Player"))
        {
            // ...lapor ke AI utama untuk mulai mengejar!
            if (mainAI != null)
            {
                mainAI.StartChasing(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Jika pemain keluar dari jangkauan...
        if (other.CompareTag("Player"))
        {
            // ...lapor ke AI utama untuk berhenti mengejar!
            if (mainAI != null)
            {
                mainAI.StopChasing();
            }
        }
    }
}