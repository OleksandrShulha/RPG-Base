using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entety : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    protected int facingDir = 1;
    protected bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] protected float groundCheckDistanse;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform groundCkeck;
    protected bool isGrounded;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    protected virtual void Update()
    {
        CollisionCheks();
    }

    protected virtual void CollisionCheks()
    {
        isGrounded = Physics2D.Raycast(groundCkeck.position, Vector2.down, groundCheckDistanse, whatIsGround);
    }

    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCkeck.position, new Vector3(groundCkeck.position.x, groundCkeck.position.y - groundCheckDistanse));
    }
}
