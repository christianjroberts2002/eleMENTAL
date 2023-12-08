using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputDirection : MonoBehaviour
{


    //Variables
    private float distanceMultiplyer = 10;
    private float localDistanceMultiplyer = 10;

    private float bodyHandOffset = .4f;
    //Neutral Bounds
    [SerializeField] private float xBound = 2f;
    [SerializeField] private float yBound = 2f;  

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

    private void Update()
    {
        SetInputDirection();
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

    private void SetInputDirection()
    {
        Vector2 handVector2Position = GetLocalVector2OfHand();
        float handPositionX = handVector2Position.x;
        float handPositionY = handVector2Position.y;

        if(handPositionX < xBound && handPositionX > -xBound
            && handPositionY < yBound && handPositionY > -yBound)
        {
            inputDirection = InputDirection.Neutral;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());
            return;
        }

        if(handPositionY > Mathf.Abs(handPositionX) & handPositionY > 0)
        {
            inputDirection = InputDirection.Up;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());

        }
        else if(handPositionY > -Mathf.Abs(handPositionX) && handPositionY < 0)
        {
            inputDirection = InputDirection.Down;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());

        }
        else if(handPositionX < 0 && handPositionY <= handPositionX)
        {
            inputDirection = InputDirection.Left;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());
        }
        else if(handPositionX > 0 && handPositionY <= handPositionX)
        {
            inputDirection = InputDirection.Right;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());
        }
        
    }

}
