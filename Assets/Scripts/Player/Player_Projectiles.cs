using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectiles : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;

    private BoxCollider2D bx;
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        bx = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if(hit){
            return;
        }

        float moveSpeed = speed * Time.deltaTime * direction;
        transform.Translate(moveSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        hit = true;
        bx.enabled = false;
    }

    public void setDirection(float direction_){
        direction = direction_;
        gameObject.SetActive(true);
        hit = false;
        bx.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != direction_){
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
