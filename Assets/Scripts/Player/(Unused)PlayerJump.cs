using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpVelo = 7f;
    [SerializeField] private LayerMask plaform;

    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private BoxCollider2D bx;
    // Start is called before the first frame update
    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        bx = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private bool OnGround(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(bx.bounds.center, bx.bounds.size, 0f, Vector2.down, .1f, plaform);
        return raycastHit.collider != null;
    }
        
}
