using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    // --- TAMBAHKAN FUNGSI AWAKE() INI ---
    void Awake()
    {
        // Saat pintu ini "lahir" di sebuah scene, ia langsung mendaftarkan dirinya ke GameManager.
        // Pastikan GameManager sudah siap (dijalankan lebih dulu via Script Execution Order).
        if (GameManager.instance != null)
        {
            // 'this' merujuk pada komponen DoorController ini sendiri.
            GameManager.instance.RegisterMainGate(this);
        }
        else
        {
            Debug.LogError("DoorController could not find GameManager instance to register with!");
        }
    }
    // --- AKHIR DARI BAGIAN BARU ---


    // Variabel dan fungsi Anda yang lain tetap sama persis, tidak perlu diubah.
    public Vector3 openRotationAngle = new Vector3(0, 90f, 0);
    public float animationTime = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpening = false;
    private float timer = 0f;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(openRotationAngle);
    }

    void Update()
    {
        if (isOpening)
        {
            timer += Time.deltaTime;
            float percentageComplete = timer / animationTime;
            transform.rotation = Quaternion.Slerp(closedRotation, openRotation, percentageComplete);

            if (timer >= animationTime)
            {
                isOpening = false;
                transform.rotation = openRotation;
            }
        }
    }

    public void OpenDoor()
    {
        if (!isOpening && transform.rotation != openRotation)
        {
            timer = 0f;
            isOpening = true;
        }
    }
}