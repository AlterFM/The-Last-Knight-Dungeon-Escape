// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // The Singleton instance
    public int keysFound = 0;
    public int[] keysNeededPerLevel = { 5, 4 };
    [HideInInspector] public int currentKeysToWin;

    [Header("Level Management")] // Header ini akan membuat judul di Inspector agar rapi
    public List<string> levelSceneNames; // Daftar untuk menyimpan nama-nama scene level Anda
    private int currentLevelIndex = 0;   // Untuk melacak di level mana pemain saat ini (dimulai dari 0)

    [Header("Player Stats")]
    public int playerLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 1000;
    public float moveSpeed = 8f; // Kecepatan awal
    public float jumpForce = 12f; // Kekuatan lompat awal

    [Header("Player Abilities")]
    public bool hasUnlockedDoubleJump = false;
    public bool hasUnlockedDash = false;

    public DoorController bigGateController; // Drag your Big Gate here in the Inspector


    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayMusic(MusicManager.instance.labyrinthMusic);
        }
    }

    public void AddKey()
    {
        keysFound++;
        Debug.Log("Keys: " + keysFound + "/" + currentKeysToWin);

        // Update the UI here (see next step)
        UIManager.instance.UpdateKeyCount(keysFound, currentKeysToWin);

        // Check if the player has won
        if (keysFound >= currentKeysToWin)
        {
            OpenTheGate();
        }
    }

    public void GainXP(int xpAmount)
    {
        currentXP += xpAmount;
        Debug.Log("Gained " + xpAmount + " XP. Total XP: " + currentXP + "/" + xpToNextLevel);

        // Panggil UIManager untuk memperbarui tampilan UI (XP Bar, dll)
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateXPUI(currentXP, xpToNextLevel);
        }

        // Cek apakah XP sudah cukup untuk naik level
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel = (int)(xpToNextLevel * 1.5f);
        moveSpeed *= 1.1f;
        jumpForce *= 1.05f;

        Debug.LogWarning("LEVEL UP! Level: " + playerLevel + ", New Speed: " + moveSpeed);

        if (playerLevel >= 3 && !hasUnlockedDoubleJump)
        {
            hasUnlockedDoubleJump = true;
            Debug.LogWarning("ABILITY UNLOCKED: DOUBLE JUMP!");
        }

        if (playerLevel >= 5 && !hasUnlockedDash)
        {
            hasUnlockedDash = true;
            Debug.LogWarning("ABILITY UNLOCKED: DASH!");
        }

        if (UIManager.instance != null)
        {
            // Perbarui teks level menjadi level yang baru
            UIManager.instance.UpdateLevelText(playerLevel);

            // Perbarui XP Bar agar sesuai dengan XP sisa dan target baru
            UIManager.instance.UpdateXPUI(currentXP, xpToNextLevel);
        }
    }

    public void ResetPlayerStats()
    {
        Debug.LogWarning("RESETTING PLAYER AND GAME STATS! Also clearing level-specific references.");

        // Reset statistik pemain
        playerLevel = 1;
        currentXP = 0;
        xpToNextLevel = 1000;
        keysFound = 0;
        currentLevelIndex = 0;
        moveSpeed = 8f;
        jumpForce = 12f;

        // Bersihkan referensi ke pintu dari level sebelumnya!
        bigGateController = null;

    }
    void OpenTheGate()
    {
        Debug.Log("The gate is opening!"); 
        // Make sure the controller is assigned before trying to use it
        if (bigGateController != null)
        {
            bigGateController.OpenDoor();
        }
    }

    public void LevelCompleted()
    {
        // Naikkan indeks untuk bersiap ke level berikutnya
        currentLevelIndex++;
        // Cek apakah masih ada level berikutnya di dalam daftar yang kita buat

        if (currentLevelIndex < levelSceneNames.Count)
        {
            // Jika masih ada, muat level berikutnya dari daftar
            string nextLevelName = levelSceneNames[currentLevelIndex];
            Debug.Log("Loading next level: " + nextLevelName);
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            // Jika tidak ada lagi level di daftar, berarti game telah tamat
            Debug.Log("All levels completed! Loading WinScene...");
            SceneManager.LoadScene("Win"); // Ganti jika nama Win Scene Anda berbeda
        }
    }

    public void SetupLevel()
    {
        // Reset jumlah kunci yang sudah ditemukan
        keysFound = 0;

        // Cek apakah indeks level saat ini valid
        if (currentLevelIndex < keysNeededPerLevel.Length)
        {
            // Atur syarat kemenangan untuk level saat ini
            currentKeysToWin = keysNeededPerLevel[currentLevelIndex];
        }
        else
        {
            // Fallback jika kita berada di level yang tidak terdefinisi
            Debug.LogWarning("No key requirement set for this level index. Defaulting to 1.");
            currentKeysToWin = 1;
        }

        Debug.Log("LEVEL SETUP: Level " + (currentLevelIndex + 1) + " requires " + currentKeysToWin + " keys.");

        // Langsung update UI saat level dimulai
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateKeyCount(keysFound, currentKeysToWin);
        }
    }

    public void RegisterMainGate(DoorController gate)
    {
        bigGateController = gate;
        Debug.Log("Gate '" + gate.name + "' has been registered.");
    }

    public void LoadWinSceneAfterGate()
    {
        Debug.Log("Gerbang terbuka, memuat scene Win!");
        SceneManager.LoadScene("Win");
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER! Loading GameOver scene...");
        // Ganti "GameOver" dengan nama scene Anda jika berbeda
        // Pastikan ejaannya sama persis dengan nama file scene Anda
        SceneManager.LoadScene("GameOver");

    }

}
