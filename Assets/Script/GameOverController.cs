using UnityEngine;

using UnityEngine.SceneManagement;



public class GameOverController : MonoBehaviour

{

    void Start()

    {

        // Pastikan kursor terlihat dan tidak terkunci saat berada di scene ini

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

    }

    // Fungsi untuk mengulang permainan

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

        SceneManager.LoadScene("MainGame");

    }



    // Fungsi untuk kembali ke menu utama

    public void GoToMainMenu()

    {

        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene menu Anda

    }

}