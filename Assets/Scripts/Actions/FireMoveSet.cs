using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core.Grabbers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireMoveSet : InputManager
{

    //Haptics
    HVRHandGrabber domHandHaptics;
    HVRHandGrabber nonDomHandHaptics;

    //Haptic Setting
    //(float amplitude, float duration = 1f, float frequency = 1f)

    [SerializeField] private float amplitude;
    [SerializeField] private float duration;
    [SerializeField] private float frequency;


    //GameObject

    [SerializeField] private GameObject fireProjectilePrefab;

    //ParticleSystem
    [SerializeField] private ParticleSystem domDownBFirePorjectile;
    [SerializeField] private ParticleSystem nonDomDownBFirePorjectile;

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
    private float domSpeed;
    private float nonDomSpeed;

    private float flySpeedMultiplyer = 5f;

    //Flying

    [SerializeField] private float maxFlyHeight;
    [SerializeField] private float maxFlySpeed;


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

        domHandHaptics = domHandGo.GetComponent<HVRHandGrabber>();
        nonDomHandHaptics = nonDomHandGo.GetComponent<HVRHandGrabber>();

        domDownBFirePorjectile = domHandGo.GetComponentInChildren<ParticleSystem>();
        nonDomDownBFirePorjectile = nonDomHandGo.GetComponentInChildren<ParticleSystem>();

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

    private void Update()
    {
        if(domHandInputScript.GetThisHandHoldIsActivated())
        {
            domSpeed += 0.25f;
        }
        else
        {
            if(domSpeed > 0)
            {
                domSpeed--;

            }
        }
        domDownBFirePorjectile.startSpeed = domSpeed;
        if(nonDomHandInputScript.GetThisHandHoldIsActivated())
        {
            nonDomSpeed += 0.25f;
        }
        else
        {
            if(nonDomSpeed > 0)
            {
                nonDomSpeed--;
            }
            
        }
        nonDomDownBFirePorjectile.startSpeed = nonDomSpeed;

        domDownBFirePorjectile.Play(domHandInputScript.GetThisHandHoldIsActivated());
        nonDomDownBFirePorjectile.Play(nonDomHandInputScript.GetThisHandHoldIsActivated());
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




    public void ActionDomA(Transform handTransform)
    {

        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        
        Destroy(fireProjectile, 2f);

    }

    public void ActionDomB(Transform handTransform)
    {
        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        Destroy(fireProjectile, 2f);
    }

    public void ActionDownA(Transform handTransform, Rigidbody playerRb, float flySpeed)
    {
        if (playerRb.gameObject.transform.position.y < maxFlyHeight)
        {
            Debug.Log("Fly");
            GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
            Destroy(fireProjectile, 2f);
            Vector3 flyDir =  (handTransform.right  * 20)  + transform.up;
            if (playerRb.velocity.magnitude < maxFlySpeed)
            {
                playerRb.AddForce(flyDir * flySpeed * flySpeedMultiplyer);
            }
        }
    }

    public void ActionDownB(Transform handTransform, Rigidbody playerRb, float flySpeed, HVRHandGrabber handHaptic, ParticleSystem downBParticleSystem)
    {
        

        if (playerRb.gameObject.transform.position.y < maxFlyHeight)
        {
            
            
            Debug.Log("Fly");
            amplitude = flySpeed * .05f;
            handHaptic.Controller.Vibrate(amplitude, duration, frequency);

            //GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
            //Rigidbody fireRb = fireProjectile.GetComponent<Rigidbody>();
            //fireRb.AddForce(playerRb.velocity + (fireRb.transform.right * -500));
            //Destroy(fireProjectile, .25f);
            Vector3 flyDir = (handTransform.right * 20) + transform.up;
            if (playerRb.velocity.magnitude < maxFlySpeed)
            {
                playerRb.AddForce(flyDir * flySpeed * flySpeedMultiplyer, ForceMode.Force);
            }
        }
    }

    public void ActionNeutralA(Transform handTransform)
    {
       GameObject fireProjectile =  Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
       Destroy(fireProjectile, 2f);
    }
    public void ActionNeutralB(Transform handTransform)
    {
        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        Destroy(fireProjectile, 2f);
    }

    public void ActionNonDomA(Transform handTransform)
    {
        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        Destroy(fireProjectile, 2f);
    }

    public void ActionNonDomB(Transform handTransform)
    {
        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        Destroy(fireProjectile, 2f);
    }

    public void ActionUpA(Transform handTransform)
    {
        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        Destroy(fireProjectile, 2f);
    }

    public void ActionUpB(Transform handTransform)
    {
        GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        Destroy(fireProjectile, 2f);
    }

    //Event Listeners

    public override void domHandInputScript_onPerformingActionDomA(object sender, EventArgs e)
    {
        ActionDomA(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionDomB(object sender, EventArgs e)
    {
        ActionDomB(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        ActionDownA(domHandSpawn, playerRb, domSpeed);
    }

    public override void domHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        ActionDownB(domHandSpawn, playerRb, domSpeed, domHandHaptics, domDownBFirePorjectile);
    }

    public override void domHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        ActionNeutralA(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        ActionNeutralB(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
       ActionNonDomA(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        ActionNonDomB(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionUpA(object sender, EventArgs e)
    {
        ActionUpA(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionUpB(object sender, EventArgs e)
    {
        ActionUpB(domHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionDomA(object sender, EventArgs e)
    {
        ActionDomA(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionDomB(object sender, EventArgs e)
    {
        ActionDomB(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        ActionDownA(nonDomHandSpawn, playerRb, nonDomSpeed);
    }

    public override void nonDomHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        ActionDownB(nonDomHandSpawn, playerRb, nonDomSpeed, nonDomHandHaptics, nonDomDownBFirePorjectile);
    }

    public override void nonDomHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        ActionNeutralA(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        ActionNeutralB(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
        ActionNonDomA(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        ActionNonDomB(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionUpA(object sender, EventArgs e)
    {
        ActionUpA(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionUpB(object sender, EventArgs e)
    {
        ActionUpB(nonDomHandSpawn);
    }
}
