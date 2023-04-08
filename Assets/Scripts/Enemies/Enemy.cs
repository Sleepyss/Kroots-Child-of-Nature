using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float distance;

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider2D bx;
    [SerializeField] private LayerMask player;
    private int currentHealth;
    public Animator animator;
    private SpriteRenderer rend;
    private Hp_Player playerHealth;
    private EnemyPatrol enemyPatrol;

    private float cooldownTimer = Mathf.Infinity;
    // Start is called before the first frame update

    void Awake(){
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
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
                animator.SetTrigger("MeleeAttack");
            }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInsight();
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        StartCoroutine(Damaged());

        if(currentHealth <= 0){
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet"){
            StartCoroutine(Damaged());
            TakeDamage(1);
        }
    }
    IEnumerator Damaged(){
        rend.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.color = Color.white;
    }

    void Die(){
        rend.color = new Color(1f,1f,1f,0f);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    private bool PlayerInsight(){
        RaycastHit2D hit = Physics2D.BoxCast(bx.bounds.center + transform.right * range * transform.localScale.x * distance, new Vector3(bx.bounds.size.x * range, bx.bounds.size.y, bx.bounds.size.z), 0, Vector2.left, 0, player);
        
        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<Hp_Player>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bx.bounds.center + transform.right * range * transform.localScale.x * distance, new Vector3(bx.bounds.size.x * range, bx.bounds.size.y, bx.bounds.size.z));
    }

    private void DamagePlayer(){
        if(PlayerInsight()){
            playerHealth.takeDamage(damage);
        }
    }

}
