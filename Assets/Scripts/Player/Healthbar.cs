using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Hp_Player playerHealth;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;
    // Start is called before the first frame update
    void Start()
    {
        totalhealthbar.fillAmount = playerHealth.currentHp;
    }

    // Update is called once per frame
    void Update()
    {
        currenthealthbar.fillAmount = playerHealth.currentHp;
    }
}
