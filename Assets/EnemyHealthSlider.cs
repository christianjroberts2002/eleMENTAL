using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    [SerializeField] EnemyHealth thisEnemyHealth;
    Slider enemyHealthSlider;

    private void Awake()
    {
        thisEnemyHealth.onDamageTaken += thisEnemyHealth_onDamageTaken;
    }

    

    private void Start()
    {
        enemyHealthSlider = GetComponent<Slider>();
    }

    private void thisEnemyHealth_onDamageTaken(object sender, EventArgs e)
    {
        enemyHealthSlider.value = thisEnemyHealth.GetEnemyHealthy();
    }


}
