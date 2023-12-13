using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireMoveSet : InputManager
{
    [SerializeField] private GameObject fireProjectilePrefab;

    //Player RB
    [SerializeField] Rigidbody playerRb;
    //Hands
    [SerializeField] GameObject domHandGo;
    [SerializeField] GameObject nonDomHandGo;

    //Hand Spawn Point
    [SerializeField] Transform domHandSpawn;
    [SerializeField] Transform nonDomHandSpawn;

    //Right and Left Reference
    [SerializeField] Transform rightSpawn;
    [SerializeField] Transform leftSpawn;

    //Hand Inputs
    private HandInputScript domHandInputScript;
    private HandInputScript nonDomHandInputScript;


    //speed
    private float speedDom;
    private float speedNonDom;

    //Flying

    [SerializeField] private float maxFlyHeight;


    //Movement Manager
    private MovementManager movementManager;
    public enum NatureType
    {
        Water,
        Fire,
        Ground,
        Air
    }

    private void Awake()
    {
        movementManager = MovementManager.instance;
        domHandGo = movementManager.GetDominantHand();
        nonDomHandGo = movementManager.GetNonDominantHand();

        //SetTheSpawnPoints
        SetSpawnPoints();

        domHandInputScript = domHandGo.GetComponent<HandInputScript>();
        nonDomHandInputScript = nonDomHandGo.GetComponent<HandInputScript>();



        //DomA
        domHandInputScript.onPerformingActionDomA += domHandInputScript_onPerformingActionDomA;
        domHandInputScript.onPerformingActionDomB += domHandInputScript_onPerformingActionDomB;

        nonDomHandInputScript.onPerformingActionDomA += nonDomHandInputScript_onPerformingActionDomA;
        nonDomHandInputScript.onPerformingActionDomB += nonDomHandInputScript_onPerformingActionDomB;
        //Non Dom
        domHandInputScript.onPerformingActionNonDomA += domHandInputScript_onPerformingActionNonDomA;
        domHandInputScript.onPerformingActionNonDomB += domHandInputScript_onPerformingActionNonDomB;

        nonDomHandInputScript.onPerformingActionNonDomA += nonDomHandInputScript_onPerformingActionNonDomA;
        nonDomHandInputScript.onPerformingActionNonDomB += nonDomHandInputScript_onPerformingActionNonDomB;
        //UpA
        domHandInputScript.onPerformingActionUpA += domHandInputScript_onPerformingActionUpA;
        domHandInputScript.onPerformingActionUpB += domHandInputScript_onPerformingActionUpB;

        nonDomHandInputScript.onPerformingActionUpA += nonDomHandInputScript_onPerformingActionUpA;
        nonDomHandInputScript.onPerformingActionUpB += nonDomHandInputScript_onPerformingActionUpB;
        //Down
        domHandInputScript.onPerformingActionDownA += domHandInputScript_onPerformingActionDownA;
        domHandInputScript.onPerformingActionDownB += domHandInputScript_onPerformingActionDownB;

        nonDomHandInputScript.onPerformingActionDownA += nonDomHandInputScript_onPerformingActionDownA;
        nonDomHandInputScript.onPerformingActionDownB += nonDomHandInputScript_onPerformingActionDownB;
        //Neutral
        domHandInputScript.onPerformingActionNeutralA += domHandInputScript_onPerformingActionNeutralA;
        domHandInputScript.onPerformingActionNeutralB += domHandInputScript_onPerformingActionNeutralB;

        nonDomHandInputScript.onPerformingActionNeutralA += nonDomHandInputScript_onPerformingActionNeutralA;
        nonDomHandInputScript.onPerformingActionNeutralB += nonDomHandInputScript_onPerformingActionNeutralB;

    }

    private void SetSpawnPoints()
    {
        if (movementManager.GetDominantHandEnum() == MovementManager.Hands.RightHand)
        {
            domHandSpawn = rightSpawn;
            nonDomHandSpawn = leftSpawn;
        }
        else
        {
            domHandSpawn = leftSpawn;
            nonDomHandSpawn = rightSpawn;
        }
    }




    public void ActionDomA()
    {
        
        throw new System.NotImplementedException();
        
    }

    public void ActionDomB()
    {
        throw new System.NotImplementedException();
    }

    public void ActionDownA()
    {
        throw new System.NotImplementedException();
    }

    public void ActionDownB(HandInputScript handInput, Transform handTransform, Rigidbody playerRb, float flySpeed)
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

    public void ActionNeutralA(HandInputScript handInputScript, Transform handTransform)
    {
       
       GameObject fireProjectile =  Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
       Destroy(fireProjectile, 2f);
    }
    public void ActionNeutralB()
    {
        throw new System.NotImplementedException();

    }

    public void ActionNonDomA()
    {
        throw new System.NotImplementedException();
    }

    public void ActionNonDomB()
    {
        throw new System.NotImplementedException();
    }

    public void ActionUpA()
    {
        throw new System.NotImplementedException();
    }

    public void ActionUpB()
    {
        throw new System.NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionUpA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void domHandInputScript_onPerformingActionUpB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionUpA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public override void nonDomHandInputScript_onPerformingActionUpB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
