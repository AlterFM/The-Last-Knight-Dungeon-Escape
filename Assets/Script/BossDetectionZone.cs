using UnityEngine;

public class BossDetectionTrigger : MonoBehaviour
{

    public BossController mainAI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Panggil fungsi deteksi di BossController
            if (mainAI != null) mainAI.OnPlayerDetected(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Panggil fungsi kehilangan jejak di BossController
            if (mainAI != null) mainAI.OnPlayerLost();
        }
    }
}