using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputDirection : MonoBehaviour
{
    //Variables
    private float distanceMultiplyer = 10;
    private float localDistanceMultiplyer = 10;

    private float bodyHandOffset = .4f;

    //GameObjects
    [SerializeField] private Transform handTransform;
     
    [SerializeField] private GameObject body;

    //RigidBodies

    [SerializeField] private Rigidbody handRigidbody;

    [SerializeField]
    public enum InputDirection
    {
        Neutral,
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private InputDirection inputDirection;

    private void Start()
    {
        handRigidbody = GetComponent<Rigidbody>();  
        handTransform = gameObject.transform;
        body = MovementManager.instance.GetBodyObject();
    }


    public float GetHandDistanceFromBody()
    {

        float distance = Vector3.Distance(body.transform.position, handTransform.position);
        return distance * distanceMultiplyer;
    }

    public Vector3 GetLocalEulerAnglesOfHand()
    {
        return handTransform.localEulerAngles;
    }

    public Vector2 GetLocalVector2OfHand()
    {
        Vector2 localVector2 = new Vector2(handTransform.localPosition.x - body.transform.localPosition.x,
            (handTransform.localPosition.y - body.transform.position.y) + bodyHandOffset) * localDistanceMultiplyer;
        return localVector2;
    }

    public InputDirection GetInputDirectionOfHand()
    {
        return inputDirection;
    }

    public Vector3 GetHandRigidbodyVelocity()
    {
        return handRigidbody.velocity;
    }

}
