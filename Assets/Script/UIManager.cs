using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // <-- PASTIKAN INI ADA

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI levelText;
    public Slider xpSlider;
    public GameObject interactionPrompt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Fungsi ini berjalan setiap kali objek UIManager diaktifkan
    private void OnEnable()
    {
        // Mulai mendengarkan event 'sceneLoaded' dari SceneManager
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Fungsi ini berjalan setiap kali objek UIManager dinonaktifkan/dihancurkan
    private void OnDisable()
    {
        // Berhenti mendengarkan untuk mencegah error dan kebocoran memori
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Fungsi ini akan dipanggil otomatis oleh SceneManager setiap kali scene baru selesai dimuat
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.LogWarning("UIManager detected scene loaded: " + scene.name);
        if (scene.name == "MainGame" || scene.name == "Arena2")
        {
            // Suruh GameManager untuk setup level baru
            GameManager.instance.SetupLevel();
        }
        // Panggil fungsi untuk me-refresh semua elemen UI dengan data terbaru
        UpdateAllUI();
    }

    public void UpdateAllUI()
    {
        if (GameManager.instance != null)
        {
            UpdateLevelText(GameManager.instance.playerLevel);
            UpdateXPUI(GameManager.instance.currentXP, GameManager.instance.xpToNextLevel);
            UpdateKeyCount(GameManager.instance.keysFound, GameManager.instance.currentKeysToWin);
        }
    }

    public void UpdateKeyCount(int currentKeys, int maxKeys)
    {
        if (keyText != null)
            keyText.text = "Keys: " + currentKeys.ToString() + " / " + maxKeys.ToString();
    }

    public void UpdateLevelText(int level)
    {
        if (levelText != null)
            levelText.text = "LVL: " + level;
    }

    public void UpdateXPUI(int currentXP, int xpToNext)
    {
        if (xpSlider != null)
        {
            if (xpToNext > 0)
            {
                xpSlider.value = (float)currentXP / xpToNext;
            }
            else
            {
                xpSlider.value = 0;
            }
        }
    }

    public void ShowInteractionPrompt()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);
    }

    public void HideInteractionPrompt()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }
}