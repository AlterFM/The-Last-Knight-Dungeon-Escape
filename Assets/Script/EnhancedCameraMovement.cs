using UnityEngine;

public class EnhancedCameraMovement : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 700.0f;
    public float ySpeed = 700.0f;

    // --- VARIABEL BARU UNTUK KETINGGIAN KAMERA ---
    public float heightOffset = 1.5f; // Atur ketinggian kamera dari titik pusat target

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null) return; // Pengaman jika target tidak ada

        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        y = Mathf.Clamp(y, -80, 80);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        // --- PERBAIKAN PADA PERHITUNGAN POSISI ---
        // 1. Buat titik target yang sudah dinaikkan sesuai heightOffset
        Vector3 targetPositionWithOffset = target.position + new Vector3(0, heightOffset, 0);

        // 2. Hitung posisi kamera dari titik target yang baru tersebut
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + targetPositionWithOffset;

        transform.rotation = rotation;
        transform.position = position;
    }
}