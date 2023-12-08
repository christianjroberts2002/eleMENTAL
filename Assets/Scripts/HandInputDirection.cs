using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputDirection : MonoBehaviour
{


    //Variables
    private float distanceMultiplyer = 10;
    private float localDistanceMultiplyer = 10;

    private float bodyHandOffset = 0;
    //Neutral Bounds
    [SerializeField] private float xBound = 5f;
    [SerializeField] private float yBound = 5f;

    [SerializeField] private float test = 10f;

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
        //handTransform = gameObject.transform;
        body = MovementManager.instance.GetBodyObject();
    }

    private void Update()
    {
        SetInputDirection();
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Vector3 handPosition = handTransform.position - body.transform.position;
        //Vector3 bodyDirection = body.transform.forward + new Vector3(0, 45, 0);
        Vector3 bodyDirection = body.transform.up;




        // Project handPosition onto the plane defined by bodyDirection
        //Vector3 projection = Vector3.ProjectOnPlane(handPosition, bodyDirection);
        Vector3 projection = Vector3.Project(handPosition, body.transform.forward);





        Vector3 testPosition = handTransform.position - (body.transform.position + projection);
        Vector3 testPositionTwo = handTransform.position - (body.transform.position + projection);

        //Vector3 handprojection = Vector3.ProjectOnPlane(testPosition, Vector3.forward);

        // Draw a ray from body position in the direction of the projection
        Debug.DrawRay(body.transform.position, projection, Color.blue);
        Debug.DrawRay(body.transform.position + projection, testPosition, Color.magenta);
        Debug.DrawRay(body.transform.position + projection,  Vector3.up, Color.white);




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
