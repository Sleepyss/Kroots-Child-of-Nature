using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public int maxHp;
    public int currentHp;

    public Image[] hp;
    public Sprite HpFull;
    public Sprite HpKurang;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    public void takeDamage(int damage)// saat player terkena damage
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);

        if(currentHp == 0){
            return;
        }

    }

    void Update()
    {
        if (currentHp > maxHp){
            currentHp = maxHp;
        }

        for (int i = 0; i < hp.Length; i++){
            if (i < currentHp){
                hp[i].sprite = HpFull;
            }else {
                hp[i].sprite = HpKurang;
            }


            if (i < maxHp){
                hp[i].enabled = true;
            }else {
                hp[i].enabled = false;
            }

        }

    }
}
