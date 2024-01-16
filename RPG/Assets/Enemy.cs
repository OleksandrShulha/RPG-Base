using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entety
{

    [SerializeField] private float moveSpeed = 5f;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        MoveControler();
        Debug.Log("Земля у врага: " + isGrounded);
    }

    private void MoveControler()
    {
        rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);
        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
    }
}
