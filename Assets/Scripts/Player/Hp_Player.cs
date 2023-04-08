using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Player : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float maxHp;
    public float currentHp { get; private set; }
    private SpriteRenderer rend;
    private Color c;

    [SerializeField] private float IframesDuration;
    [SerializeField] private int numberOfFlashes;



    // Start is called before the first frame update
    void Awake()
    {
        currentHp = maxHp;
        rend = GetComponent<SpriteRenderer>();
        c = rend.material.color;
    }

    public void takeDamage(float damage)// saat player terkena damage
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);

        if(currentHp > 0){ 
            StartCoroutine(Invunerability());
        }else{
            // die animation
            GetComponent<PlayerMovement>().enabled = false;
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
            takeDamage(0.333f);
        }
    }

    private IEnumerator Invunerability(){
        Physics2D.IgnoreLayerCollision(10,11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            rend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(IframesDuration / numberOfFlashes);
            rend.color = Color.white;
            yield return new WaitForSeconds(IframesDuration / numberOfFlashes);
        }
        Physics2D.IgnoreLayerCollision(10,11, false);
    }
}
