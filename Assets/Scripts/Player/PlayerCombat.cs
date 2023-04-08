using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] peluru;
    public Transform attackPoint;
    public float range = 0.5f;
    public LayerMask enemy;
    public int attackDamage = 1;

    public float attackRate = 1f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime){
            if(Input.GetKeyDown(KeyCode.J)){
                anim.SetTrigger("Attack");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if(Input.GetKeyDown(KeyCode.K)){
                anim.SetTrigger("Shoot");
                Shoot();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack(){
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemy);

        foreach(Collider2D enemy in hit){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void Shoot(){
        peluru[0].transform.position = firePoint.position;
        peluru[0].GetComponent<Player_Projectiles>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    void OnDrawGizmosSelected() {
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

}
