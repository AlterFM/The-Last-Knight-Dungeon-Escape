using UnityEngine;
using TMPro;

public class GameDebugDisplay : MonoBehaviour
{
    public TextMeshProUGUI debugText; // Akan kita hubungkan ke teks UI

    // Update berjalan setiap frame, jadi kita bisa melihat perubahan secara real-time
    void Update()
    {
        if (debugText == null) return;

        string status = "--- LIVE DEBUG INFO ---\n"; // Judul

        // Cek status GameManager
        if (GameManager.instance == null)
        {
            status += "GameManager.instance is: <color=red>NULL!</color>\n";
        }
        else
        {
            status += "GameManager instance: " + GameManager.instance.gameObject.name + "\n";
            status += "Level: " + GameManager.instance.playerLevel + "\n";
            status += "XP: " + GameManager.instance.currentXP + "\n";
            status += "Keys: " + GameManager.instance.keysFound + "\n";
        }

        // Tampilkan statusnya di layar
        debugText.text = status;
    }
}