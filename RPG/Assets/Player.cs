using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float xInput;
    private Rigidbody2D rb;
    private Animator anim;
    private int facingDir = 1;
    private bool facingRight = true;
    private bool isGrounded;

    [Header("Other")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;
    [Header("Collision info and Jump")]
    [SerializeField] private float groundCheckDistanse;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Dashes")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float speedDash;

    [Header("Atack info")]
    private bool isAtack;
    private int comboCounter;






    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        MoveControler();
        AnimationControler();
        FlipControler();

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistanse, whatIsGround);

        dashTime -= Time.deltaTime;
    }

    private void MoveControler()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded)
        {
            dashTime = dashDuration;
        }

        if(dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * speedDash, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAtack = true;
            comboCounter = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isAtack = true;
            comboCounter = 1;
        }
    }

    public void AtackOver()
    {
        isAtack = false;
    }

    private void Jump()
    {
        if(isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    private void AnimationControler()
    {
        bool isMoving = xInput != 0;
        anim.SetBool("isMove", isMoving);
        anim.SetBool("isGround", isGrounded);
        anim.SetBool("isDash", dashTime > 0);

        anim.SetBool("isAtack", isAtack);
        anim.SetInteger("comboCounter", comboCounter);




    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipControler()
    {
        if(rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistanse));
    }
}
