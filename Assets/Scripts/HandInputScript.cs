using HurricaneVR.Framework.ControllerInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputScript : MonoBehaviour
{
    [SerializeField] private bool isPerformingAction;

    //"Right" Dom
    public static event EventHandler onPerformingActionDomA;
    public static event EventHandler onPerformingActionDomB;

    //"Left" nonDom
    public static event EventHandler onPerformingActionNonDomA;
    public static event EventHandler onPerformingActionNonDomB;

    //Up
    public static event EventHandler onPerformingActionUpA;
    public static event EventHandler onPerformingActionUpB;

    //Down
    public static event EventHandler onPerformingActionDownA;
    public static event EventHandler onPerformingActionDownB;

    //Neutral
    public static event EventHandler onPerformingActionNeutralA;
    public static event EventHandler onPerformingActionNeutralB;

    //booleans
    [SerializeField] private bool rightTriggerIsActivated;
    [SerializeField] private bool leftTriggerIsActivated;

    [SerializeField] private bool rightGrabisActivated;
    [SerializeField] private bool leftGrabisActivated;


    //reference to the input Mangager
    [SerializeField] private HVRPlayerInputs playerInputScript;
    private void Start()
    {
      
    }

    void Update()
    {
        if (!isPerformingAction)
        {
            ReadInput();
        }
        //BInputs Special Attacks
        rightGrabisActivated = HVRPlayerInputs.Instance.IsRightGrabActivated;
        leftGrabisActivated = HVRPlayerInputs.Instance.IsLeftGrabActivated;
        //AInputs Smash Attacks
        rightTriggerIsActivated = HVRPlayerInputs.Instance.IsRightTriggerHoldActive;
        leftTriggerIsActivated = HVRPlayerInputs.Instance.IsLeftTriggerHoldActive;
    }

    private void ReadInput()
    {


    }

    public void SetIsNotPerformingAction()
    {
        isPerformingAction = false;
    }
}
