using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private ChestController currentChest;
    // HAPUS VARIABEL INI: public GameObject interactionPromptUI;

    void Update()
    {
        if (currentChest != null && Input.GetKeyDown(KeyCode.E))
        {
            currentChest.OpenChest();
            // SURUH UIManager untuk menyembunyikan prompt
            UIManager.instance.HideInteractionPrompt();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            ChestController chest = other.GetComponent<ChestController>();
            if (chest != null && !chest.isOpened)
            {
                currentChest = chest;
                // SURUH UIManager untuk menampilkan prompt
                UIManager.instance.ShowInteractionPrompt();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            // Pastikan kita hanya menonaktifkan jika kita meninggalkan peti yang benar
            if (currentChest == other.GetComponent<ChestController>())
            {
                currentChest = null;
                // SURUH UIManager untuk menyembunyikan prompt
                UIManager.instance.HideInteractionPrompt();
            }
        }
    }
}