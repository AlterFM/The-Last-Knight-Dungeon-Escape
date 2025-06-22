using UnityEngine;

public class SimpleSFXPlayer : MonoBehaviour
{
    // Fungsi ini akan kita panggil dari Animation Event.
    // Ia akan mencari MusicManager yang abadi dan memerintahkannya untuk memutar suara.
    public void PlaySound(AudioClip clip)
    {
        if (MusicManager.instance != null && clip != null)
        {
            MusicManager.instance.PlaySFX(clip);
        }
        else
        {
            Debug.LogWarning("MusicManager instance or AudioClip is missing!");
        }
    }
}