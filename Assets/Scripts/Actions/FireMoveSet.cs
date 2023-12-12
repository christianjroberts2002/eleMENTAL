using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMoveSet : ActionBaseScript
{
    [SerializeField] private GameObject fireProjectilePrefab;
    [SerializeField] private Transform fireProjectileSpawn;

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

    public override void ActionDownB()
    {
        throw new System.NotImplementedException();
    }

    public override void ActionNeutralA(Transform handTransform)
    {
       GameObject fireProjectile =  Instantiate(fireProjectilePrefab, handTransform.position, Quaternion.Euler(handTransform.transform.forward));
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
