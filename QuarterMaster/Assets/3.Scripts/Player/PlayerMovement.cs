using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private bool canMove;
    public Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }
    void Update()
    {
        if (canMove)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", directionX);
            animator.SetFloat("Vertical", directionY);

            playerDirection = new Vector2(directionX, directionY).normalized;
            animator.SetFloat("Speed", playerDirection.SqrMagnitude());
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
    public void SetCanMove(bool value)
    {
        canMove = value;
        return;
    }

}
