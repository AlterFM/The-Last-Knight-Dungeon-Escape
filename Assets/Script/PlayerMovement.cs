using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded = true;
    private float score = 0f;

    public float moveSpeed;
    public float jumpForce;
    public Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        scoreText.text = "Score: " + score;
    }

    void FixedUpdate()
    {
        //arah belok -1 ke kiri 1 ke kanan
        float moveX = 0;

        //arah maju -1 mundur 1 maju
        float moveZ = 0;

        //membuat kamera menyesuaikan arah karakter
        Camera cam = FindObjectOfType<Camera>();
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jumpTrigger");
            isGrounded = false;
        }

        //menormalisasi arah kamera dengan arah karakter
        Vector3 moveDirection = (camRight * moveX + camForward * moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            //merotasi karakter ketika berbelok
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        //menggerakkan karakter
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        //mengubah kondisi isMoving supaya animasi lari bisa auto play
        bool isMoving = moveX != 0 || moveZ != 0;
        animator.SetBool("isMoving", isMoving);

        if (score == 12)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ketika karakter menyentuh object dengan collider yang mempunyai tag Ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            //ubah kondisi isGrounded menjadi true supaya karakter bisa melompat
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            Destroy(other.gameObject);
            addScore();
        }
    }

    void addScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}
