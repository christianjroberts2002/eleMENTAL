using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RayFire.RayfireBomb;

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
    // Projection Triangle

    //Vectors
    private Vector3 angleAVector;
    private Vector3 angleBVector;
    private Vector3 angleCVector;

    //Angle
    private float angleCFloat;

    //Sides
    private float sideA;
    private float sideB;
    private float sideC;

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
        //SetInputDirection();
    }

   

    private void LateUpdate()
    {

        MakeTriangleForInputCalculation();
        GetAngleFromProjectionTriangle();


    }

    private void MakeTriangleForInputCalculation()
    {
        Vector3 newBodyPositionWithYOffset = new Vector3(body.transform.position.x, body.transform.position.y + bodyHandOffsetY, body.transform.position.z);

        Vector3 handPosition = handTransform.position - newBodyPositionWithYOffset;

        Vector3 bodyDirection = body.transform.forward;

        Vector3 projection = Vector3.Project(handPosition, bodyDirection);

        Vector3 DistanceToHandFromPlaneProjection = handTransform.position - (newBodyPositionWithYOffset + projection);

        Debug.DrawRay(newBodyPositionWithYOffset, projection, Color.blue);
        Debug.DrawRay(newBodyPositionWithYOffset + projection, DistanceToHandFromPlaneProjection, Color.magenta);
        Debug.DrawRay(newBodyPositionWithYOffset + projection, Vector3.up * bodyHandOffsetY, Color.white);

        //GetAngleVectors
        angleAVector = handTransform.position;
        angleBVector = newBodyPositionWithYOffset + projection + new Vector3(0, bodyHandOffsetY, 0);
        angleCVector = newBodyPositionWithYOffset + projection;

        //DebugObjects
        debugObjectA.transform.position = angleAVector;
        debugObjectB.transform.position = angleBVector;
        debugObjectC.transform.position = angleCVector;
    }

    private void GetAngleFromProjectionTriangle()
    {
        sideA = Vector3.Distance(angleCVector, angleBVector);
        //Debug.Log(sideA);
        sideB = Vector3.Distance(angleCVector, angleAVector);
        //Debug.Log(sideB);
        sideC = Vector3.Distance(angleAVector, angleBVector);
        //Debug.Log(sideC);

        float cosC = (Mathf.Pow(sideA, 2) + Mathf.Pow(sideB, 2) - Mathf.Pow(sideC, 2)) / (2 * sideA * sideB);
        angleCFloat = Mathf.Acos(cosC) * 180/Mathf.PI;




        Debug.Log(gameObject.name + " angle is: " + angleCFloat);
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

    public float GetSideA()
    {
        return sideA;
    }

    public float GetSideB()
    {
        return sideB;
    }

    public float GetSideC()
    {
        return sideC;
    }

    public float GetAngleC()
    {
        return angleCFloat;
    }

    private void SetInputDirection()
    {

        float inputAngle = angleCFloat;
        

        if (sideB < 2)
        {
            inputDirection = InputDirection.Neutral;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());
            return;
        }

        if(0 == 0)
        {
            inputDirection = InputDirection.Up;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());

        }
        else if(0 == 0)
        {
            inputDirection = InputDirection.Down;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());

        }
        else if(0 == 0)
        {
            inputDirection = InputDirection.Left;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());
        }
        else if(0 == 0)
        {
            inputDirection = InputDirection.Right;
            Debug.Log(gameObject.name + " is " + inputDirection.ToString());
        }
        
    }

}
