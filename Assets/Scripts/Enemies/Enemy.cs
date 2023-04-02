using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float distance;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D bx;
    [SerializeField] private LayerMask player;
    private int currentHealth;
    public Animator animator;

    private EnemyPatrol enemyPatrol;

    private float cooldownTimer = Mathf.Infinity;
    // Start is called before the first frame update

    void Awake(){
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update() {
        cooldownTimer += Time.deltaTime;

        if(PlayerInsight()){
            if(cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                Debug.Log("You got hit");
            }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInsight();
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy Died");
    }

    private bool PlayerInsight(){
        RaycastHit2D hit = Physics2D.BoxCast(bx.bounds.center + transform.right * range * transform.localScale.x * distance, new Vector3(bx.bounds.size.x * range, bx.bounds.size.y, bx.bounds.size.z), 0, Vector2.left, 0, player);
        
        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bx.bounds.center + transform.right * range * transform.localScale.x * distance, new Vector3(bx.bounds.size.x * range, bx.bounds.size.y, bx.bounds.size.z));
    }

    // private void DamagePlayer(){

    // }

}
