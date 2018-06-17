using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 6f;
    public float jumpForce = 700f;
    public Transform groundCheck;
    public LayerMask whatIsGround;          // Determine where the character can actually stand

    Camera camera;
    Animator animator;

    bool facingRight = true;
    bool grounded = true;
    //bool gameOver, restart;
    float groundRadius = 0.2f;
    float move;
    float idleTime = 0f;

	void Start () {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        //gameOver = false;
        //restart = false;
	}

    void Update()
    {
        CheckIdleness();

        // jump code
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (transform.position.y < -10)
        {
            camera.GetComponent<HUD>().GameOver();
        } 
    }

    void FixedUpdate () {
        // Player is grounded if a circlecast to groundCheck.position hits anything marked as ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Ground", grounded);
        animator.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);    // vertical speed

        if (!grounded) return;      // stop movement while jumping

        move = Input.GetAxis("Horizontal");
        if (move != 0) idleTime = 0f;

        animator.SetFloat("Speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight) ChangeDirection();
        else if (move < 0 && facingRight) ChangeDirection();
	}

    void CheckIdleness()
    {
        if (move != 0 || !grounded) idleTime = 0f;

        idleTime += Time.deltaTime;
        animator.SetFloat("Idle Time", idleTime);

        if (idleTime > 9f)
        {
            animator.SetTrigger("IdleTooLong");
            idleTime = 0f;
        }
    }

    void ChangeDirection()
    {
        facingRight = !facingRight;
        Vector3 locScale = transform.localScale;
        locScale.x *= -1;
        transform.localScale = locScale;
    }
}
