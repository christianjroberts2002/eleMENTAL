using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    //Variables
    private float distanceMultiplyer = 10;
    private float localDistanceMultiplyer = 10;

    private float bodyHandOffset = .4f;
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

    [SerializeField] public enum InputDirection
    {
        Neutral,
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private InputDirection inputDirection;  

    public void Awake()
    {
        if(instance == null)
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
        if(domHand != Hands.RightHand)
        {
            GameObject tempHand = domHandGO;
            domHandGO = nonDomHandGO;
            nonDomHandGO = tempHand;
        }

        domHandRB = domHandGO.GetComponent<Rigidbody>();
        nonDomHandRB = domHandRB.GetComponent<Rigidbody>();

        onDomHandSet?.Invoke(this, EventArgs.Empty);
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

    public float GetHandDistanceFromBody(Transform hand)
    {
        
        float distance = Vector3.Distance(body.transform.position, hand.position);
        return distance * distanceMultiplyer;
    }

    public float GetDomHandDistanceFromBody()
    {
        return  GetHandDistanceFromBody(domHandGO.transform);
    }

    public float GetNonDomHandDistanceFromBody()
    {
        return GetHandDistanceFromBody(nonDomHandGO.transform);
    }

    public Vector3 GetLocalEulerAnglesOfHand(Transform hand)
    {
        return hand.localEulerAngles;
    }

    public Vector3 GetLocalEulerAnglesOfDomHand()
    {
        return GetLocalEulerAnglesOfHand(domHandGO.transform);
    }

    public Vector3 GetLocalEulerAnglesOfNonDomHand()
    {
        return GetLocalEulerAnglesOfHand(nonDomHandGO.transform);
    }

    public Vector2 GetLocalVector2OfHand(Transform hand)
    {
        Vector2 localVector2 = new Vector2(hand.localPosition.x - body.transform.localPosition.x,
            (hand.localPosition.y - body.transform.position.y) + bodyHandOffset) * localDistanceMultiplyer;
        return localVector2;
    }

    public Vector2 GetLocalVector2OfDomHand()
    {
        return GetLocalVector2OfHand(domHandGO.transform);
    }

    public Vector2 GetLocalVector2OfNonDomHand()
    {
        return GetLocalVector2OfHand(nonDomHandGO.transform);
    }

    public InputDirection GetInputDirectionOfHand(InputDirection inputDirection)
    {
        return inputDirection;
    }

    public Vector3 GetHandRigidbodyVelocity(Rigidbody handRB)
    {
        return handRB.velocity;
    }

    public Vector3 GetDomHandRigidbodyVelocity()
    {
        return GetHandRigidbodyVelocity(domHandRB);
    }

    public Vector3 GetNonDomHandRigidbodyVelocity()
    {
        return GetHandRigidbodyVelocity(nonDomHandRB);
    }



}
