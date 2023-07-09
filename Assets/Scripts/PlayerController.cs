using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public Animator animator;

    private float moveX;
    private SpriteRenderer spriteRenderer;


    void Awake() {

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        
    }

   

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        if (moveX > 0) {
            spriteRenderer.flipX = false;
            
        } else if (moveX < 0) {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate() {

        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
    }
}
