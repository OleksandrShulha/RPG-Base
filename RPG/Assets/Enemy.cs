using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entety
{

    [SerializeField] private float moveSpeed = 5f;


    public RaycastHit2D isPlayrDetected;
    [SerializeField] private float playrChekDistanse;
    [SerializeField] private LayerMask whatIsPlayr;

    private bool isAtack;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        MoveControler();
        anim.SetBool("isAtack", isAtack);


    }

    private void MoveControler()
    {

        if (isPlayrDetected)
        {
            Debug.Log("i see you");
            if (isPlayrDetected.distance > 1)
            {
                rb.velocity = new Vector2(facingDir * 5f * moveSpeed, rb.velocity.y);
                isAtack = false;
            }
            else
            {
                isAtack = true;
            }
        }
        else
        {
            Debug.Log("i not see");
            isAtack = false;
        }


        if(!isAtack && !isPlayrDetected)
            rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);



        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
    }

    protected override void CollisionCheks()
    {
        base.CollisionCheks();

        isPlayrDetected = Physics2D.Raycast(transform.position, Vector2.right, playrChekDistanse * facingDir, whatIsPlayr);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playrChekDistanse * facingDir, transform.position.y));
    }
}
