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

    [SerializeField] private TextMeshProUGUI domSideAText;
    [SerializeField] private TextMeshProUGUI nonDomSideAText;

    [SerializeField] private TextMeshProUGUI domHandSideBText;
    [SerializeField] private TextMeshProUGUI nonDomHandSideBText;

    [SerializeField] private TextMeshProUGUI domHandSideCText;
    [SerializeField] private TextMeshProUGUI nonDomHandSideCText;

    [SerializeField] private TextMeshProUGUI domHandAngleCText;
    [SerializeField] private TextMeshProUGUI nonDomHandAngleCText;

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
        SetHandDistanceText(domHandText, nonDomHandText);

        SetLocalEulerAnglesText(domHandLocalEulerAnglesText, nonDomHandLocalEulerAnglesText);

        SetInputPositionText(domInputPositionText, nonDomInputPositionText);

        SetVector2Text(domHandVector2Text, nonDomHandVector2Text);

        SetRBVelText(domHandRBVelText, nonDomHandRBVelText);

        SetSideAText(domSideAText, nonDomSideAText);

        SetSideBText(domHandSideBText, nonDomHandSideBText);

        SetSideCText(domHandSideCText, nonDomHandSideCText);

        SetAngleCText(domHandAngleCText, nonDomHandAngleCText);


    }

    private void SetHandDistanceText(TextMeshProUGUI domDistanceText, TextMeshProUGUI nonDomDistanceText)
    {
        domDistanceText.text = MovementManager.instance.GetDomHandDistanceFromBody().ToString("F1");
        nonDomDistanceText.text = MovementManager.instance.GetNonDomHandDistanceFromBody().ToString("F1");
    }

    private void SetLocalEulerAnglesText(TextMeshProUGUI domLocalEulerText, TextMeshProUGUI nonDomLocalEulerText)
    {
        domLocalEulerText.text =  MovementManager.instance.GetLocalEulerAnglesOfDomHand().ToString("000");
        nonDomLocalEulerText.text =  MovementManager.instance.GetLocalEulerAnglesOfNonDomHand().ToString("000");
    }

    private void SetInputPositionText(TextMeshProUGUI domInputPositionText, TextMeshProUGUI nonDomInputPositionText)
    {
        domInputPositionText.text = MovementManager.instance.GetInputDirectionOfDomHand().ToString();
        nonDomInputPositionText.text = MovementManager.instance.GetInputDirectionOfNonDomHand().ToString();
    }

    private void SetVector2Text(TextMeshProUGUI domVector2PosText, TextMeshProUGUI nonDomVector2PosText)
    {
        domVector2PosText.text = MovementManager.instance.GetLocalVector2OfDomHand().ToString("F1");
        nonDomVector2PosText.text = MovementManager.instance.GetLocalVector2OfNonDomHand().ToString("F1");
    }

    private void SetRBVelText(TextMeshProUGUI domRbVelText, TextMeshProUGUI nonDomRbVelText)
    {
        domRbVelText.text = MovementManager.instance.GetDomHandRigidbodyVelocity().ToString("0.0");
        nonDomRbVelText.text = MovementManager.instance.GetNonDomHandRigidbodyVelocity().ToString("0.0");
    }

    private void SetSideAText(TextMeshProUGUI domSideAText, TextMeshProUGUI nonDomSideAText)
    {

        domSideAText.text = MovementManager.instance.GetDomHandSideA().ToString();
        nonDomSideAText.text = MovementManager.instance.GetNonDomHandSideA().ToString();
    }

    private void SetSideBText(TextMeshProUGUI domSideBText, TextMeshProUGUI nonDomSideBText)
    {
        domSideBText.text = MovementManager.instance.GetDomHandSideB().ToString();
        nonDomSideBText.text = MovementManager.instance.GetNonDomHandSideB().ToString();
    }

    private void SetSideCText(TextMeshProUGUI domSideCText, TextMeshProUGUI nonDomSideCText)
    {
        domSideCText.text = MovementManager.instance.GetDomHandSideC().ToString();
        nonDomSideCText.text = MovementManager.instance.GetNonDomHandSideC().ToString();
    }

    private void SetAngleCText(TextMeshProUGUI domAngleCText, TextMeshProUGUI nonDomAngleCText)
    {
        domAngleCText.text = MovementManager.instance.GetDomHandAngleC().ToString();
        nonDomAngleCText.text = MovementManager.instance.GetNonDomHandAngleC().ToString();
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
