using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event EventHandler onDamageTaken;

    [SerializeField] private float health = 100;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Dieing");
        health -= .25f;
        onDamageTaken?.Invoke(this, EventArgs.Empty);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Fire")
        {
            health -= 25;
            onDamageTaken?.Invoke(this, EventArgs.Empty);

        }

    }

    private void Update()
    {
        if (health < 0)
            Destroy(gameObject);
    }

    public float GetEnemyHealthy()
    {
        return health;
    }
}
