using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entety
{
    private float xInput;


    [Header("Other")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;
 

    [Header("Dashes")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float speedDash;

    [Header("Atack info")]
    private bool isAtack;
    private int comboCounter;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();

        MoveControler();
        AnimationControler();
        FlipControler();
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
}
