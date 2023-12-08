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

    //PlayerGameObject
    [SerializeField] private GameObject domHand;
    [SerializeField] private GameObject nonDomHand;



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
        inputPositiontext.text = "Right";
    }

    private void SetVector2Text(TextMeshProUGUI vector2PosText, Transform hand)
    {
        vector2PosText.text = "00,00,00";
    }


    private void MovementManager_onDomHandSet(object sender, EventArgs e)
    {
        Debug.Log("2");
        SetHands();

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
