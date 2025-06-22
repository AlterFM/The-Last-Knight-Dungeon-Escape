using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 15f;

    [Header("Dash Settings")]
    public float dashForce = 25f;
    public float dashCooldown = 2f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.2f;

    [Header("Attack Settings")]
    public GameObject swordHitbox;
    public float attackDelay = 0.3f;     
    public float attackActiveTime = 0.2f;

    // Komponen dan Variabel Privat
    private Rigidbody rb;
    private Animator animator;
    private Camera mainCamera;
    private Vector3 moveDirection;

    // State variables
    private bool isGrounded;
    private bool jumpRequested;
    private bool hasDoubleJumped;
    private bool isDashRequested;
    private float dashCooldownTimer;
    private bool isAttacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded)
        {
            hasDoubleJumped = false;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        moveDirection = (camRight.normalized * moveX + camForward.normalized * moveZ).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                jumpRequested = true;
            }

            else if (GameManager.instance != null && GameManager.instance.hasUnlockedDoubleJump && !hasDoubleJumped)
            {
                jumpRequested = true;
                hasDoubleJumped = true;
                animator.SetTrigger("doubleJumpTrigger");
            }
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && GameManager.instance != null && GameManager.instance.hasUnlockedDash)
        {
            isDashRequested = true;
        }
        if (Input.GetMouseButtonDown(0) && isGrounded && !isAttacking)
        {
            // Mulai sekuens serangan
            StartCoroutine(AttackSequence());
        }

        animator.SetFloat("speed", moveDirection.magnitude);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        rb.linearVelocity = targetVelocity;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jumpTrigger");
            jumpRequested = false;

        }

        if (isDashRequested)
        {
            Vector3 dashDirection = moveDirection;
            if (dashDirection == Vector3.zero)
            {
                dashDirection = transform.forward;
            }

            rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            dashCooldownTimer = dashCooldown;
            isDashRequested = false;
            animator.SetTrigger("dashTrigger");
        }
    }

    private IEnumerator AttackSequence()
    {
        // 1. Tandai bahwa kita sedang menyerang untuk mencegah serangan beruntun (spam)
        //    dan untuk mengunci gerakan jika Anda mau.
        isAttacking = true;

        // 2. Mainkan animasi serangan seperti biasa
        animator.SetTrigger("attackTrigger");

        // 3. Tunggu sebentar sesuai attackDelay (memberi waktu untuk animasi memulai ayunan)
        yield return new WaitForSeconds(attackDelay);

        // 4. Aktifkan hitbox di tengah ayunan
        Debug.Log("Hitbox Enabled!");
        if (swordHitbox != null) swordHitbox.SetActive(true);

        // 5. Biarkan hitbox aktif selama durasi serangan
        yield return new WaitForSeconds(attackActiveTime);

        // 6. Matikan kembali hitbox setelah ayunan selesai
        Debug.Log("Hitbox Disabled!");
        if (swordHitbox != null) swordHitbox.SetActive(false);

        // 7. Tandai bahwa kita sudah selesai menyerang dan bisa menyerang lagi/bergerak lagi
        isAttacking = false;
    }





}
