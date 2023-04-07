using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;

    private Vector3 initScale;

    private bool movingLeft;

    [SerializeField] private float idle;
    private float idleTimer;

    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        initScale = enemy.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingLeft){
            if(enemy.position.x >= leftEdge.position.x){
                MoveDirection(-1);
            }else{
                flip();
            }
        }else{
            if(enemy.position.x <= rightEdge.position.x){
                MoveDirection(1);
            }else{
                flip();
            }
        }
    }

    private void flip(){

        idleTimer += Time.deltaTime;

        if(idleTimer > idle){
            movingLeft = !movingLeft;

        }
    }

    private void MoveDirection(int _direction){
        idleTimer = 0;

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
