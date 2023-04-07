using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private bool isFacingRight = true;
    private float inputMove = 0f;

    [Header("Jump")]
    [SerializeField] private float jumpVelo = 7f;
    [SerializeField] private LayerMask plaform;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPow = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 velocity;
    private BoxCollider2D bx;
    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        bx = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Move
        inputMove = Input.GetAxisRaw("Horizontal");
        Flip();
        animator.SetFloat("Speed", Mathf.Abs(inputMove));

        // Dash
        if(isDashing){
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && OnGround()){
            StartCoroutine(Dash());
        }

        // Jump
        if(OnGround() && Input.GetButtonDown("Jump")){
            rb.velocity = Vector2.up * jumpVelo;
            animator.SetBool("Jump", true);
            animator.SetFloat("JumpVelo", rb.velocity.y);
        }

        if(rb.velocity.y <= 0){
            animator.SetBool("Jump", false);
            animator.SetFloat("JumpVelo", rb.velocity.y);
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
        animator.SetTrigger("Dash");
        Physics2D.IgnoreLayerCollision(3,7, true);
        rb.velocity = new Vector2(transform.localScale.x * dashingPow, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originGrav;
        Physics2D.IgnoreLayerCollision(3,7, false);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private bool OnGround(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(bx.bounds.center, bx.bounds.size, 0f, Vector2.down, .1f, plaform);
        return raycastHit.collider != null;
    }
}
