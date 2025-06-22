using UnityEngine;
using UnityEngine.SceneManagement; // Penting untuk SceneManager

public class WinSceneManager : MonoBehaviour
{
    void Start()
    {
        // Tampilkan kursor mouse
        Cursor.visible = true;

        // Lepaskan kursor dari tengah layar (tidak terkunci)
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("Scene Win dimuat, kursor seharusnya terlihat dan tidak terkunci.");
    }

    // Fungsi untuk ButtonRestart
    public void RestartGame()
    {
        // RESET STATS DI SINI JUGA!
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetPlayerStats();
        }

        // Un-pause game sebelum pindah scene
        Time.timeScale = 1f;

        // Muat ulang game dengan kondisi bersih
        Debug.Log("Restarting game..."); 
        SceneManager.LoadScene("MainGame");
    }

    // Fungsi untuk ButtonMainMenu
    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu...");
        // Memuat scene MainMenu
        SceneManager.LoadScene("MainMenu");
    }
}