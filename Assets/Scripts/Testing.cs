using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private float health = 100;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Dieing");
        health -= .25f;
        
    }

    private void Update()
    {
        if (health < 0)
            Destroy(gameObject);
    }
}
