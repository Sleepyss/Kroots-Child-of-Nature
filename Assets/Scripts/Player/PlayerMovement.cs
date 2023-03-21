using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float Speed = 4f;
    [SerializeField, Range(0f, 100f)] private float MaxSpeed = 100f;
    [SerializeField, Range(0f, 100f)] private float MaxSpeedAir = 15f;

    private Vector2 direction;
    private Vector2 desiredSpeed; 
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Ground gd;

    private float MaxSpeedChange;
    private float Accel;
    private bool onGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gd = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {   
        direction.x = Input.GetAxisRaw("Horizontal");
        desiredSpeed = new Vector2(direction.x, 0f) * Speed;
        
    }

    private void FixedUpdate() {
        onGround = gd.getOnGround();
        velocity = rb.velocity;

        if(onGround == true){
            Accel = MaxSpeed;
        }else{
            Accel = MaxSpeedAir;
        }

        MaxSpeedChange = Accel * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredSpeed.x,MaxSpeedChange);
        rb.velocity = velocity;
    }

}
