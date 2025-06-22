using UnityEngine;

public class WallTrap : MonoBehaviour
{
    public Transform wallLeft;
    public Transform wallRight;
    public float moveDistance = 2f;
    public float moveSpeed = 2f;
    public float activeTime = 1.5f;
    public float inactiveTime = 2f;

    private Vector3 leftStartPos;
    private Vector3 rightStartPos;
    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;

    private float timer;
    private bool isActive = false;

    void Start()
    {
        leftStartPos = wallLeft.position;
        rightStartPos = wallRight.position;

        leftClosedPos = leftStartPos + Vector3.forward * moveDistance;
        rightClosedPos = rightStartPos + Vector3.back * moveDistance;


        timer = inactiveTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            isActive = !isActive;
            timer = isActive ? activeTime : inactiveTime;
        }

        wallLeft.position = Vector3.Lerp(wallLeft.position, isActive ? leftClosedPos : leftStartPos, Time.deltaTime * moveSpeed);
        wallRight.position = Vector3.Lerp(wallRight.position, isActive ? rightClosedPos : rightStartPos, Time.deltaTime * moveSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            PlayerHealth hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(999); // Instant kill
            }
        }
    }
}