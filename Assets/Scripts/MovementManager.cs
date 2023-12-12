using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HandInputDirection;

public class MovementManager : MonoBehaviour
{
    
    //Instance
    public static MovementManager instance;

    //GameObjects

    [SerializeField] private GameObject domHandGO;
    [SerializeField] private GameObject nonDomHandGO;

    [SerializeField] private GameObject body;

    //RigidBodies

    [SerializeField] private Rigidbody domHandRB;
    [SerializeField] private Rigidbody nonDomHandRB;

    //Events

    public event EventHandler onDomHandSet;


    // Enums

    [SerializeField] public enum Hands
    {
        RightHand,
        LeftHand
    }

    [SerializeField] private Hands domHand;

    //HandInputDirectionVars

    [SerializeField] private HandInputDirection domHandInputDirectionManager;
    [SerializeField] private HandInputDirection nonDomHandInputDirectionManager;

     

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void Start()
    {
        

        if (domHand != Hands.RightHand)
        {
            GameObject tempHand = domHandGO;
            domHandGO = nonDomHandGO;
            nonDomHandGO = tempHand;
        }

        domHandRB = domHandGO.GetComponent<Rigidbody>();
        nonDomHandRB = domHandRB.GetComponent<Rigidbody>();

        domHandInputDirectionManager = domHandGO.GetComponent<HandInputDirection>();
        nonDomHandInputDirectionManager = nonDomHandGO.GetComponent<HandInputDirection>();
        Debug.Log("1");
        onDomHandSet?.Invoke(this, EventArgs.Empty);

        //Fixes a bug??
        body.transform.rotation = Quaternion.identity;


    }

    public GameObject GetBodyObject()
    {
        return body;
    }

    public Hands GetDominantHandEnum()
    {
        return domHand;
    }

    public GameObject GetDominantHand()
    {
        return domHandGO;
    }

    public GameObject GetNonDominantHand()
    {
        return nonDomHandGO;
    }

    

    public float GetDomHandDistanceFromBody()
    {
        return domHandInputDirectionManager.GetHandDistanceFromBody();
    }

    public float GetNonDomHandDistanceFromBody()
    {
        return nonDomHandInputDirectionManager.GetHandDistanceFromBody();
    }


    public Vector3 GetLocalEulerAnglesOfDomHand()
    {
        return domHandInputDirectionManager.GetLocalEulerAnglesOfHand();
    }


    public Vector3 GetLocalEulerAnglesOfNonDomHand()
    {
        return nonDomHandInputDirectionManager.GetLocalEulerAnglesOfHand();
    }

    

    public Vector2 GetLocalVector2OfDomHand()
    {
        return domHandInputDirectionManager.GetLocalVector2OfHand();
    }

    public Vector2 GetLocalVector2OfNonDomHand()
    {
        return nonDomHandInputDirectionManager.GetLocalVector2OfHand();
    }


    public InputDirection GetInputDirectionOfDomHand()
    {
        return domHandInputDirectionManager.GetInputDirectionOfHand();
    }

    public InputDirection GetInputDirectionOfNonDomHand()
    {
        return nonDomHandInputDirectionManager.GetInputDirectionOfHand();
    }


    public Vector3 GetDomHandRigidbodyVelocity()
    {
        return domHandInputDirectionManager.GetHandRigidbodyVelocity();
    }

    public Vector3 GetNonDomHandRigidbodyVelocity()
    {
        return nonDomHandInputDirectionManager.GetHandRigidbodyVelocity();
    }

    public float GetDomHandSideA()
    {
        return domHandInputDirectionManager.GetSideA();
    }

    public float GetNonDomHandSideA()
    {
        return nonDomHandInputDirectionManager.GetSideA();
    }

    public float GetDomHandSideB()
    {
        return domHandInputDirectionManager.GetSideB();
    }

    public float GetNonDomHandSideB()
    {
        return nonDomHandInputDirectionManager.GetSideB();
    }

    public float GetDomHandSideC()
    {
        return domHandInputDirectionManager.GetSideC();
    }

    public float GetNonDomHandSideC()
    {
        return nonDomHandInputDirectionManager.GetSideC();
    }

    public float GetDomHandAngleC()
    {
        return domHandInputDirectionManager.GetAngleC();
    }

    public float GetNonDomHandAngleC()
    {
        return nonDomHandInputDirectionManager.GetAngleC();
    }



}
