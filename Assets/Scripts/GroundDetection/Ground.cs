using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private void OnCollisionEnter2D(Collision2D other) {
        CollisionDetection(other);
    }

    private void OnCollisionStay2D(Collision2D other) {
        CollisionDetection(other);
    }

    private void OnCollisionExit2D(Collision2D other) {
        onGround = false;
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

    public bool getOnGround(){
        return onGround;
    }

}
