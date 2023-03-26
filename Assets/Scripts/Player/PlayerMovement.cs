using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
       if(Input.GetKey(KeyCode.D)){
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
       }else if(Input.GetKey(KeyCode.A)){
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
       }else{
            rb.velocity = new Vector2(0, rb.velocity.y);
       }
    }

}
