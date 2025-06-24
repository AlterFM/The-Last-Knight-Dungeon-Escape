using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB ditambahkan untuk mengelola scene

public class SceneLoader : MonoBehaviour
{
    // Fungsi ini kita buat PUBLIC agar bisa diakses dari luar (oleh Button)
    public void LoadMainMenu()
    {
        // Pastikan nama "MainMenu" sama persis dengan nama file scene menu utama Anda
        // Perhatikan huruf besar dan kecilnya!
        SceneManager.LoadScene("MainMenu");
    }

    // Opsional: Jika Anda ingin kembali ke scene sebelumnya, bukan menu utama.
    // public void LoadPreviousScene()
    // {
    //     int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //     SceneManager.LoadScene(currentSceneIndex - 1);
    // }
}