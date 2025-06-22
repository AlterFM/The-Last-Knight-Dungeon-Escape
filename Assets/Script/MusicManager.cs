using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; // Speaker khusus untuk Musik Latar (BGM)
    public AudioSource sfxSource;   // Speaker khusus untuk Efek Suara (SFX)

    [Header("Audio Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip labyrinthMusic;
    public AudioClip winMusic; 
    public AudioClip gameOverMusic; 
    public AudioClip playerAttackSound;
    public AudioClip bossAttackSound;
    public AudioClip playerRunSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi untuk memutar Musik Latar (BGM)
    public void PlayMusic(AudioClip musicClip)
    {
        // Hanya ganti musik jika klipnya berbeda untuk menghindari musik restart tanpa alasan
        if (musicSource.clip == musicClip && musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = musicClip;
        musicSource.loop = true; // Musik harus berulang
        musicSource.Play();
    }

    // Fungsi untuk memutar Efek Suara (SFX)
    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxSource != null && sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }
}