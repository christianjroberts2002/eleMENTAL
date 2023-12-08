using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandCanvasManager : MonoBehaviour
{
    //UI
    [SerializeField] private Canvas domHandCanvas;
    [SerializeField] private Canvas nonDomHandCanvas;

    [SerializeField] private TextMeshProUGUI domHandText;
    [SerializeField] private TextMeshProUGUI nonDomHandText;

    [SerializeField] private TextMeshProUGUI domHandLocalEulerAnglesText;
    [SerializeField] private TextMeshProUGUI nonDomHandLocalEulerAnglesText;

    [SerializeField] private TextMeshProUGUI domInputPositionText;
    [SerializeField] private TextMeshProUGUI nonDomInputPositionText;

    [SerializeField] private TextMeshProUGUI domHandVector2Text;
    [SerializeField] private TextMeshProUGUI nonDomHandVector2Text;

    [SerializeField] private TextMeshProUGUI domHandRBVelText;
    [SerializeField] private TextMeshProUGUI nonDomHandRBVelText;

    //PlayerGameObject
    [SerializeField] private GameObject domHand;
    [SerializeField] private GameObject nonDomHand;

    [SerializeField] private Rigidbody domRB;
    [SerializeField] private Rigidbody nonDomRB;



    private void Awake()
    {
        MovementManager.instance.onDomHandSet += MovementManager_onDomHandSet;

    }

    public void Update()
    {
        SetHandDistanceText(domHandText, domHand.transform);
        SetHandDistanceText(nonDomHandText, nonDomHand.transform);

        SetLocalEulerAnglesText(domHandLocalEulerAnglesText, domHand.transform);
        SetLocalEulerAnglesText(nonDomHandLocalEulerAnglesText, nonDomHand.transform);

        SetInputPositionText(domInputPositionText, domHand.transform);
        SetInputPositionText(nonDomInputPositionText, nonDomHand.transform);

        SetVector2Text(domHandVector2Text, domHand.transform);
        SetVector2Text(nonDomHandVector2Text, nonDomHand.transform);

        SetRBVelText(domHandRBVelText, domRB);
        SetRBVelText(nonDomHandRBVelText, nonDomRB);

    }

    private void SetHandDistanceText(TextMeshProUGUI distanceText, Transform hand)
    {
        distanceText.text = MovementManager.instance.GetHandDistanceFromBody(hand).ToString("F1");
    }

    private void SetLocalEulerAnglesText(TextMeshProUGUI localEulerText, Transform hand)
    {
        localEulerText.text = MovementManager.instance.GetLocalEulerAnglesOfHand(hand).ToString("000");
    }

    private void SetInputPositionText(TextMeshProUGUI inputPositiontext, Transform hand)
    {
        inputPositiontext.text = MovementManager.instance.GetInputDirectionOfHand(MovementManager.InputDirection.Left).ToString();
    }

    private void SetVector2Text(TextMeshProUGUI vector2PosText, Transform hand)
    {
        vector2PosText.text = MovementManager.instance.GetLocalVector2OfHand(hand.transform).ToString("00.0");
    }

    private void SetRBVelText(TextMeshProUGUI rbVelText, Rigidbody handRB)
    {
        rbVelText.text = MovementManager.instance.GetHandRigidbodyVelocity(handRB).ToString("0.0");
    }


    private void MovementManager_onDomHandSet(object sender, EventArgs e)
    {
        Debug.Log("2");
        SetHands();
        domRB = domHand.GetComponent<Rigidbody>();
        nonDomRB = nonDomHand.GetComponent<Rigidbody>();

        if (MovementManager.instance.GetDominantHandEnum() != MovementManager.Hands.RightHand)
        {
            Canvas tempCanvas = domHandCanvas;
            domHandCanvas = nonDomHandCanvas;
            nonDomHandCanvas = tempCanvas;
            SwitchCanvasToCorrectSide(nonDomHandCanvas, domHandCanvas);

        }

        



    }

    private void SetHands()
    {
        Debug.Log("3");
        domHand = MovementManager.instance.GetDominantHand();
        nonDomHand = MovementManager.instance.GetNonDominantHand();
    }

    private void SwitchCanvasToCorrectSide(Canvas nonDomHandCanvasA, Canvas domCanvasB)
    {
        Vector3 tempcanvasVector3 = nonDomHandCanvasA.transform.position;
        nonDomHandCanvasA.transform.position = domCanvasB.transform.position;
        domCanvasB.transform.position = tempcanvasVector3;
    }
}
