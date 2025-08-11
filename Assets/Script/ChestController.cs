// ChestController.cs
using UnityEngine;

public class ChestController : MonoBehaviour
{
    // A simple 'memory' flag so this chest can only be opened once.
    public bool isOpened = false;

    // A variable to hold the chest's animator component, so we can tell it to play animations.
    // Animator Controller for this to work.
    public Animator animator;
    public GameObject keyVisualPrefab;
    public Transform keySpawnPoint; // Untuk menampung titik spawn kita

    // This is the main function that gets called by the player.
    public void OpenChest()
    {
        if (!isOpened)
        {
            isOpened = true;
            GameManager.instance.AddKey();
            GameManager.instance.GainXP(200);

            // --- Sound Effect saat kunci didapatkan ---
            if (MusicManager.instance != null && MusicManager.instance.keyCollectSound != null)
            {
                // Perintahkan MusicManager untuk memainkan SFX kunci
                MusicManager.instance.PlaySFX(MusicManager.instance.keyCollectSound);
            }

            if (keyVisualPrefab != null)
            {
                Instantiate(keyVisualPrefab, keySpawnPoint.position, keySpawnPoint.rotation);
            }
        }
    }
}