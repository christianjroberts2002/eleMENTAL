using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{

    //Player RB
    [SerializeField] Rigidbody playerRb;
    //Hands
    [SerializeField] GameObject domHandGo;
    [SerializeField] GameObject nonDomHandGo;

    //Hand Spawn Point
    [SerializeField] Transform domSpawn;
    [SerializeField] Transform nonDomSpawn;

    //Right and Left Reference
    [SerializeField] Transform rightSpawn;
    [SerializeField] Transform leftSpawn;

    //Hand Inputs
    private HandInputScript domHandInputScript;
    private HandInputScript nonDomHandInputScript;

    [SerializeField] ActionBaseScript[] actionBaseScripts;

    [SerializeField] ActionBaseScript currentActionScript;

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
        if(movementManager.GetDominantHandEnum() == MovementManager.Hands.RightHand)
        {
            domSpawn = rightSpawn;
            nonDomSpawn = leftSpawn;
        }else
        {
            domSpawn = leftSpawn;
            nonDomSpawn = rightSpawn;
        }
    }


    private void Start()
    {
        currentActionScript = actionBaseScripts[0];
    }
    private void Update()
    {
        if(domHandInputScript.GetCanPerformingAction() && speedDom > 0)
        {
            speedDom--;
        }

        if(!nonDomHandInputScript.GetCanPerformingAction() && speedNonDom > 0)
        {
            speedNonDom--;
        }
        
    }

    //Coroutines


    //EventListeners
    private void nonDomHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void nonDomHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        currentActionScript.ActionNeutralA(nonDomHandInputScript, nonDomSpawn);
        
    }

    private void domHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void domHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        currentActionScript.ActionNeutralA(domHandInputScript, domSpawn);
        
    }

    private void nonDomHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        currentActionScript.ActionDownB(nonDomHandInputScript, nonDomSpawn, playerRb, speedNonDom);
        speedNonDom++;
        if (playerRb.transform.position.y > maxFlyHeight)
        {
            nonDomHandInputScript.SetCanPerformAction(false);
        }
        else
        {
            nonDomHandInputScript.SetCanPerformAction(true);
        }

    }

    private void nonDomHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        currentActionScript.ActionDownB(nonDomHandInputScript, nonDomSpawn, playerRb, speedNonDom);
        speedNonDom++;
        if(playerRb.transform.position.y > maxFlyHeight)
        {
            nonDomHandInputScript.SetCanPerformAction(false);
        }
        else
        {
            nonDomHandInputScript.SetCanPerformAction(true);
        }
        
    }

    private void domHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        currentActionScript.ActionDownB(domHandInputScript, domSpawn, playerRb, speedDom);
        speedDom++;
        if (playerRb.transform.position.y > maxFlyHeight)
        {
            nonDomHandInputScript.SetCanPerformAction(false);
        }
        else
        {
            nonDomHandInputScript.SetCanPerformAction(true);
        }

    }

    private void domHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        currentActionScript.ActionDownB(domHandInputScript, domSpawn, playerRb, speedDom);
        speedDom++;
        if (playerRb.transform.position.y > maxFlyHeight)
        {
            nonDomHandInputScript.SetCanPerformAction(false);
        }
        else
        {
            nonDomHandInputScript.SetCanPerformAction(true);
        }

    }

    private void nonDomHandInputScript_onPerformingActionUpB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void nonDomHandInputScript_onPerformingActionUpA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void domHandInputScript_onPerformingActionUpB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void domHandInputScript_onPerformingActionUpA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void nonDomHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void nonDomHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void domHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void domHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void nonDomHandInputScript_onPerformingActionDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void nonDomHandInputScript_onPerformingActionDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void domHandInputScript_onPerformingActionDomB(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void domHandInputScript_onPerformingActionDomA(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }




}
