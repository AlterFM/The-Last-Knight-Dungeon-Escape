using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // Diperlukan untuk SceneManager

public class MainMenu : MonoBehaviour
{
    // --- Variabel untuk UI Panels ---
    // Pastikan untuk menyeret GameObject InfoPanel dari Hierarchy ke slot ini di Inspector
    public GameObject infoPanel;
    public GameObject menuPanel;

    // --- Fungsi untuk Navigasi Scene ---

    // Fungsi ini akan dipanggil saat ButtonStart diklik
    public void LoadMainGame()
    {
        // Panggil fungsi reset di GameManager SEBELUM memuat scene
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetPlayerStats();
        }

        // Kode Anda yang sudah ada untuk memuat scene
        Debug.Log("ButtonStart diklik! Mencoba memuat scene MainGame...");
        SceneManager.LoadScene("MainGame");
    }

    // Fungsi ini akan dipanggil saat tombol kembali ke MainMenu diklik (jika ada)
    public void LoadMainMenu()
    {
        // Memuat scene dengan nama "MainMenu"
        // Pastikan nama ini persis sama dengan nama file scene Anda di Build Settings
        SceneManager.LoadScene("MainMenu");
    }

    // --- Fungsi untuk Mengelola InfoPanel ---

    // Fungsi ini akan dipanggil saat ButtonInfo diklik
    public void ShowInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true); // Mengaktifkan InfoPanel
            Debug.Log("InfoPanel ditampilkan!");

            // SEMBUNYIKAN MENU PANEL
            if (menuPanel != null)
            {
                menuPanel.SetActive(false); // Menonaktifkan MenuPanel
                Debug.Log("MenuPanel disembunyikan.");
            }
            else
            {
                Debug.LogWarning("MenuPanel reference is not set in MainMenu script!");
            }
        }
        else
        {
            Debug.LogWarning("InfoPanel reference is not set in MainMenu script!");
        }
    }

    // Fungsi ini akan dipanggil saat tombol Close/Back pada InfoPanel diklik
    public void HideInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false); // Menonaktifkan InfoPanel
            Debug.Log("InfoPanel disembunyikan!");

            // TAMPILKAN KEMBALI MENU PANEL
            if (menuPanel != null)
            {
                menuPanel.SetActive(true); // Mengaktifkan MenuPanel kembali
                Debug.Log("MenuPanel ditampilkan kembali.");
            }
            else
            {
                Debug.LogWarning("MenuPanel reference is not set in MainMenu script!");
            }
        }
        else
        {
            Debug.LogWarning("InfoPanel reference is not set in MainMenu script!");
        }
    }

    // ... (fungsi QuitGame) ...
    public void QuitGame()
    {
        Debug.Log("Keluar dari Game...");

        // --- KODE UTAMA UNTUK KELUAR APLIKASI ---
        Application.Quit();

        // --- KODE TAMBAHAN KHUSUS UNTUK TESTING DI UNITY EDITOR ---
        // Ini akan menghentikan mode Play di Unity Editor.
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #endif
    }
}