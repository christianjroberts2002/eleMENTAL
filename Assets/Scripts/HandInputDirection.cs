using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandInputDirection : MonoBehaviour
{


    //Variables
    private float distanceMultiplyer = 10;
    private float localDistanceMultiplyer = 10;

    private float bodyHandOffset = 0;
    private float bodyHandOffsetY = .4f;
    //Neutral Bounds
    [SerializeField] private float xBound = 5f;
    [SerializeField] private float yBound = 5f;

    [SerializeField] private float test = 10f;

    //GameObjects
    [SerializeField] private Transform handTransform;
     
    [SerializeField] private GameObject body;

    private GameObject debugObjectA;
    private GameObject debugObjectB;
    private GameObject debugObjectC;

    [SerializeField] private GameObject debugObjectPrefab;

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
        //handTransform = gameObject.transform;
        body = MovementManager.instance.GetBodyObject();

        debugObjectA = Instantiate(debugObjectPrefab, transform);
        debugObjectB = Instantiate(debugObjectPrefab, transform);
        debugObjectC = Instantiate(debugObjectPrefab, transform);
    }

    private void Update()
    {
        SetInputDirection();
    }

   

    private void LateUpdate()
    {
        
        Vector3 newBodyPositionWithYOffset = new Vector3(body.transform.position.x, body.transform.position.y + bodyHandOffsetY, body.transform.position.z);

        Vector3 handPosition = handTransform.position - newBodyPositionWithYOffset;

        Vector3 bodyDirection = body.transform.forward;

        Vector3 projection = Vector3.Project(handPosition, bodyDirection);

        Vector3 DistanceToHandFromPlaneProjection = handTransform.position - (newBodyPositionWithYOffset + projection);

        Debug.DrawRay(newBodyPositionWithYOffset, projection, Color.blue);
        Debug.DrawRay(newBodyPositionWithYOffset + projection, DistanceToHandFromPlaneProjection, Color.magenta);
        Debug.DrawRay(newBodyPositionWithYOffset + projection, Vector3.up * bodyHandOffsetY, Color.white);


        debugObjectA.transform.position = newBodyPositionWithYOffset + projection;
        debugObjectB.transform.position = newBodyPositionWithYOffset + projection + newBodyPositionWithYOffset + projection;
        debugObjectC.transform.position = handTransform.position;


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
        Vector2 localVector2 = new Vector2(handTransform.position.x - body.transform.position.x,
            (handTransform.position.y - body.transform.position.y) + bodyHandOffset) * localDistanceMultiplyer;
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
        

        if (handPositionX < xBound && handPositionX > -xBound
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
