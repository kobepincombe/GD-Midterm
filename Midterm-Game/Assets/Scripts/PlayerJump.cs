using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    //public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public bool canJump = false;
    public int jumpTimes = 0;
    public bool isAlive = true;
    public string input = "Player 1 Jump";
    //public AudioSource JumpSFX;

    private bool wasGrounded = false;

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        
        if (isGrounded) {
            jumpTimes = 0;
        }
        
        if (isGrounded && !wasGrounded){
            canJump = true;
        } else if (jumpTimes > 1) {
            canJump = false;
        }

        if ((Input.GetButtonDown(input)) && (canJump) && (isAlive == true)) {
            Jump();
        }
        
        wasGrounded = isGrounded;
    }

    public void Jump()
    {
        jumpTimes += 1;
        rb.velocity = Vector2.up * jumpForce;
        // anim.SetTrigger("Jump");
        // JumpSFX.Play();

        // Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        // rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
        if ((groundCheck != null) || (enemyCheck != null)) {
            Debug.Log("I am touching ground!");
            return true;
        }
        return false;
    }
}
