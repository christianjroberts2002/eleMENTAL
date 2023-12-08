using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    //Instance
    public static MovementManager instance;

    //GameObjects

    [SerializeField] private GameObject domHandGO;
    [SerializeField] private GameObject nonDomHandGO;

    [SerializeField] private GameObject body;

    //Events

    public event EventHandler onDomHandSet;


    // Enums

    [SerializeField] public enum Hands
    {
        RightHand,
        LeftHand
    }

    private Hands domHand;

    public enum InputDirection
    {
        Neutral,
        Up,
        Down,
        Left,
        Right
    }

    private InputDirection inputDirection;  

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
        return distance;
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

    public Vector2 GetVector2OfHand(Transform hand)
    {
        return hand.position;
    }

    public Vector2 GetVector2OfDomHand()
    {
        return GetVector2OfHand(domHandGO.transform);
    }

    public Vector2 GetVector2OfNonDomHand()
    {
        return GetVector2OfHand(nonDomHandGO.transform);
    }

    public InputDirection GetInputDirectionOfHand(InputDirection inputDirection)
    {
        return inputDirection;
    }

}
