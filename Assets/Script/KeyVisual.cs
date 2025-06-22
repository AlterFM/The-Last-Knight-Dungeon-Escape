using UnityEngine;

public class KeyVisual : MonoBehaviour
{
    public float spinSpeed = 180.0f; // How fast it spins
    public float floatSpeed = 0.5f;  // How fast it floats up and down
    public float floatHeight = 0.5f; // How high it floats

    private float startY;

    void Start()
    {
        // Remember the starting height of the key
        this.startY = transform.position.y;

        // IMPORTANT: This tells the key to destroy itself after 3 seconds.
        Destroy(gameObject, 4.0f);
    }

    void Update()
    {
        // Spin the key around the Y (up) axis
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

        // Make the key float up and down smoothly using a Sine wave
        Vector3 position = transform.position;
        position.y = startY + (Mathf.Sin(Time.time * floatSpeed) * floatHeight);
        transform.position = position;
    }
}