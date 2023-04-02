using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Move
    [SerializeField] private float moveSpeed = 5f;
    public Animator animator;
    private bool isFacingRight = true;
    private float inputMove = 0f;

    // Dash
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPow = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {   
        if(isDashing){
            return;
        }

        inputMove = Input.GetAxisRaw("Horizontal");
        Flip();

        animator.SetFloat("Speed", Mathf.Abs(inputMove));

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate() {
        if(isDashing){
            return;
        }
        rb.velocity = new Vector2(inputMove * moveSpeed, rb.velocity.y);
    }

    private void Flip(){
        if(isFacingRight && inputMove < 0f || !isFacingRight && inputMove > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float originGrav = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPow, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originGrav;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
