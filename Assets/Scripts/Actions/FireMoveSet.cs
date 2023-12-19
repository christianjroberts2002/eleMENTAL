using HexabodyVR.PlayerController;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Shared;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class FireMoveSet : InputManager
{
    //FireVisuals
    [SerializeField] private float maxSpeed;

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

    private bool domIsActing;
    private bool nonDomIsActing;

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

    //Timers
    private float flySpeedMultiplyer = 5f;
    [SerializeField] private float domBTimer = 2f;

    //Flying

    [SerializeField] private float maxFlyHeight;
    [SerializeField] private float maxFlySpeed;

    [SerializeField] private float flyTimeMultiplier;

    [SerializeField] private float flyTimer;
    [SerializeField] private float flyTime;

    //Movement Manager
    private MovementManager movementManager;

    //Hexa Body Player 4
    [SerializeField] HexaBodyPlayer4 hexaBodyPlayer;

    //Enums
    public enum NatureType
    {
        Water,
        Fire,
        Ground,
        Air
    }

    //DomB
    [SerializeField] private Vector3 domBStartPos;
    [SerializeField] public Vector3 domBEndPos;

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
        ////Make a function
        //if(domHandInputScript.GetThisHandHoldIsActivated() && flyTimer > 0 && domSpeed < maxSpeed)
        //{
        //    domSpeed += Time.deltaTime * flyTimeMultiplier;
            
        //    domDownBFirePorjectile.Play();
        //}
        //else
        //{
        //    if(domSpeed > 0)
        //    {
        //        domSpeed -= flyTimeMultiplier * Time.deltaTime;
        //        domDownBFirePorjectile.Stop();

        //    }
        //}


        

        ////Make A funtion
        //if(nonDomHandInputScript.GetThisHandHoldIsActivated() && flyTimer > 0 && nonDomSpeed < maxSpeed)
        //{
        //    nonDomSpeed += Time.deltaTime * flyTimeMultiplier;
        //    nonDomDownBFirePorjectile.Play();
        //}
        //else
        //{

        //    if(nonDomSpeed > 0)
        //    {
        //        nonDomSpeed -= flyTimeMultiplier * Time.deltaTime;
        //        nonDomDownBFirePorjectile.Stop();
        //    }
            
        //}

        

       
        //Make a Function
        nonDomDownBFirePorjectile.startSpeed = nonDomSpeed;
        ParticleSystem.ShapeModule nonDomNewShape = nonDomDownBFirePorjectile.shape;
        nonDomNewShape.radius = nonDomSpeed / 30;
        ParticleSystem.EmissionModule nonDomEmission = nonDomDownBFirePorjectile.emission;
        nonDomEmission.rateOverTime = nonDomSpeed * 20;

        domDownBFirePorjectile.startSpeed = domSpeed;
        ParticleSystem.ShapeModule domNewShape = domDownBFirePorjectile.shape;
        domNewShape.radius = domSpeed / 30;
        ParticleSystem.EmissionModule domEmission = domDownBFirePorjectile.emission;
        domEmission.rateOverTime = domSpeed * 20;



        if (hexaBodyPlayer.IsGrounded)
        {
            flyTimer = flyTime;
        }




    }

    private void ActionTimer(ref float timer, float decreaseAmount)
    {
            timer -= decreaseAmount * Time.deltaTime;
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

    public void ActionDomB(Transform handTransform, HandInputScript handInputScript)
    {
        
        //Start
        if(!handInputScript.GetActionIsEnding())
        {

            GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
            Debug.Log("why");
            HVRHandGrabber handGrabber = handInputScript.gameObject.GetComponent<HVRHandGrabber>();
            HVRHandGrabOnStart grabOnStart = fireProjectile.GetComponent<HVRHandGrabOnStart>();
            grabOnStart.Grabbable = fireProjectile.GetComponent<HVRGrabbable>();
            grabOnStart.Grabber = handGrabber;
            
            handInputScript.SetHandIsActing(true);
            Destroy(fireProjectile, 3f);

        }
        //End
        if (handInputScript.GetActionIsEnding())
        {
            HVRHandGrabber handGrabber = handInputScript.gameObject.GetComponent<HVRHandGrabber>();
            handInputScript.SetHandIsActing(false);
            handGrabber.ForceRelease();
            
        }


    }

    public void ActionDownA(Transform handTransform, Rigidbody playerRb, float flySpeed)
    {
        //if (playerRb.gameObject.transform.position.y < maxFlyHeight)
        //{
        //    Debug.Log("Fly");
        //    GameObject fireProjectile = Instantiate(fireProjectilePrefab, handTransform.position, handTransform.rotation);
        //    Destroy(fireProjectile, 2f);
        //    Vector3 flyDir =  (handTransform.right  * 20)  + transform.up;
        //    if (playerRb.velocity.magnitude < maxFlySpeed)
        //    {
        //        playerRb.AddForce(flyDir * flySpeed * flySpeedMultiplyer);
        //    }
        //}
    }

    public void ActionDownB(HandInputScript handInputScript, Transform handTransform, Rigidbody playerRb, ref float flySpeed, HVRHandGrabber handHaptic, ParticleSystem downBParticleSystem)
    {
        if (flyTimer < 0)
        {
            handInputScript.SetNeedsToReleaseButton(true);
            downBParticleSystem.Stop();
        }

        if (!handInputScript.GetActionIsEnding())
        {
          
        ActionTimer(ref flyTimer, 1);
        if (playerRb.gameObject.transform.position.y < maxFlyHeight && flyTimer > 0)
        {
                //Make a function
                if (handInputScript.GetThisHandHoldIsActivated() && flyTimer > 0 && flySpeed < maxSpeed)
                {
                    flySpeed += Time.deltaTime * flyTimeMultiplier;

                    downBParticleSystem.Play();
                }


                Debug.Log("Fly");
            amplitude = flySpeed * .05f;
            handHaptic.Controller.Vibrate(amplitude, duration, frequency);
            Vector3 flyDir = (handTransform.right * 20) + transform.up;
            if (playerRb.velocity.magnitude < maxFlySpeed)
            {
                playerRb.AddForce(flyDir * flySpeed * flySpeedMultiplyer, ForceMode.Force);
            }
        }
        
        }
        else if(handInputScript.GetActionIsEnding())
        {
            handInputScript.SetNeedsToReleaseButton(false);
            flySpeed -= flyTimeMultiplier * Time.deltaTime;
            downBParticleSystem.Stop();
        }

    }

    public void ActionNeutralA(Transform handTransform)
    {
       
    }
    public void ActionNeutralB(HandInputScript handInputScript ,Transform handTransform)
    {
        
        if(!handInputScript.GetActionIsEnding())
        {
            if(domBStartPos == Vector3.zero)
            {
                domBStartPos = handTransform.position;
            }

        }
        else if(handInputScript.GetActionIsEnding())
        {
            domBEndPos = handTransform.position;
            Vector3 middlepoint = Vector3.Lerp(domBEndPos, domBStartPos, 0.5f);
            GameObject fireSlash = Instantiate(fireProjectilePrefab, middlepoint, Quaternion.identity);
            fireSlash.transform.LookAt(domBEndPos, fireSlash.transform.up);
            fireSlash.transform.localScale = new Vector3(.15f, Vector3.Distance(domBStartPos, domBEndPos), .15f);
            fireSlash.gameObject.GetComponent<MeshRenderer>().enabled = true;
            fireSlash.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(fireSlash, 5f);
            domBStartPos = Vector3.zero;
        }
        
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
        ActionDomB(domHandSpawn, domHandInputScript);
    }

    public override void domHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        ActionDownA(domHandSpawn, playerRb, domSpeed);
    }

    public override void domHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        ActionDownB(domHandInputScript, domHandSpawn, playerRb, ref domSpeed, domHandHaptics, domDownBFirePorjectile);
    }

    public override void domHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        ActionNeutralA(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        ActionNeutralB(domHandInputScript, domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
       ActionNonDomA(domHandSpawn);
    }

    public override void domHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        ActionDomB(domHandSpawn, domHandInputScript);
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
        ActionDomB(nonDomHandSpawn, nonDomHandInputScript);
    }

    public override void nonDomHandInputScript_onPerformingActionDownA(object sender, EventArgs e)
    {
        ActionDownA(nonDomHandSpawn, playerRb, nonDomSpeed);
    }

    public override void nonDomHandInputScript_onPerformingActionDownB(object sender, EventArgs e)
    {
        ActionDownB(nonDomHandInputScript,  nonDomHandSpawn, playerRb, ref nonDomSpeed, nonDomHandHaptics, nonDomDownBFirePorjectile);
    }

    public override void nonDomHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e)
    {
        ActionNeutralA(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e)
    {
        ActionNeutralB(nonDomHandInputScript, nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e)
    {
        ActionNonDomA(nonDomHandSpawn);
    }

    public override void nonDomHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e)
    {
        ActionDomB(nonDomHandSpawn, nonDomHandInputScript);
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
