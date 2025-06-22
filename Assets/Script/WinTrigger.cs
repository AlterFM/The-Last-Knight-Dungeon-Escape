using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Cek jika yang masuk adalah pemain
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Completed! Notifying GameManager...");

            // Beri tahu GameManager bahwa level ini sudah selesai
            GameManager.instance.LevelCompleted();

            // Nonaktifkan trigger ini agar tidak terpicu berulang kali
            gameObject.SetActive(false);
        }
    }
}