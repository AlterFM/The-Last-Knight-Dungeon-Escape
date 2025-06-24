using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Membuat kursor terlihat/muncul
        Cursor.visible = true;

        // Membuka kunci kursor agar bisa bergerak bebas
        Cursor.lockState = CursorLockMode.None;
    }
}