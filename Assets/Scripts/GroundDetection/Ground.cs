using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private float friction;
    private void OnCollisionEnter2D(Collision2D other) {
        CollisionDetection(other);
        FrictionDetection(other);
    }

    private void OnCollisionStay2D(Collision2D other) {
        CollisionDetection(other);
        FrictionDetection(other);
    }

    private void OnCollisionExit2D(Collision2D other) {
        onGround = false;
        friction = 0;
    }

    private void CollisionDetection(Collision2D collision){
        for(int i = 0; i < collision.contactCount; i++){
            Vector2 norm = collision.GetContact(i).normal;
            if(norm.y >= 0.9f){
                onGround = true;
            }else{
                onGround = false;
            }
        }
    }

    private void FrictionDetection(Collision2D collision){
        PhysicsMaterial2D mats = collision.rigidbody.sharedMaterial;

        friction = 0;

        if(mats != null){
            friction = mats.friction;
        }
    }

    public bool getOnGround(){
        return onGround;
    }

    public float getFriction(){
        return friction;
    }

}
