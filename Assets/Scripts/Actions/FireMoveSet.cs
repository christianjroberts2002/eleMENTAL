using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireMoveSet : ActionBaseScript
{
    [SerializeField] private GameObject fireProjectilePrefab;
    [SerializeField] private Transform fireProjectileSpawn;

    [SerializeField] Rigidbody playerRb;


    

    public override void ActionDomA()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionDomB()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionDownA()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionDownB(HandInputScript handInput, Transform handTransform, Rigidbody playerRb, float flySpeed)
    {
        Debug.Log("Fly");
        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        Destroy(fireProjectile , 2f);
        Vector3 flyDir = playerRb.gameObject.transform.position - handTransform.position + transform.up;
        if(playerRb.velocity.magnitude < 20)
        {
            playerRb.AddForce(flyDir * flySpeed * 5);
        }
        
    }

    public override void ActionNeutralA(HandInputScript handInputScript, Transform handTransform)
    {
       
       GameObject fireProjectile =  Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
       Destroy(fireProjectile, 2f);
    }
    public override void ActionNeutralB()
    {
        throw new System.NotImplementedException();

    }

    public override void ActionNonDomA()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionNonDomB()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionUpA()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionUpB()
    {
        throw new System.NotImplementedException();
    }
}
