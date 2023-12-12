using HurricaneVR.Framework.ControllerInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputScript : MonoBehaviour
{
    
    [SerializeField] private bool isPerformingAction;

    //"Right" Dom
    public event EventHandler onPerformingActionDomA;
    public event EventHandler onPerformingActionDomB;

    //"Left" nonDom
    public event EventHandler onPerformingActionNonDomA;
    public event EventHandler onPerformingActionNonDomB;

    //Up
    public event EventHandler onPerformingActionUpA;
    public event EventHandler onPerformingActionUpB;

    //Down
    public event EventHandler onPerformingActionDownA;
    public event EventHandler onPerformingActionDownB;

    //Neutral
    public event EventHandler onPerformingActionNeutralA;
    public event EventHandler onPerformingActionNeutralB;

    //booleans
    [SerializeField] private bool thisHandTriggerIsActivated;
    [SerializeField] private bool otherDomHandTriggerIsActivated;

    [SerializeField] private bool thisHandGrabisActivated;
    [SerializeField] private bool otherDomHandGrabisActivated;

    private bool isRightHand;


    //reference to the input Mangager
    [SerializeField] private HVRPlayerInputs playerInputScript;

    //Hand Input Manager

    [SerializeField] private HandInputDirection handInputDirection;
    //InputDirection

    private HandInputDirection.InputDirection inputDirection;

    //HandInput Enum
    public enum HandInput
    {
        NeutralA,
        NeutralB,
        UpA,
        UpB,
        DownA,
        DownB,
        DomA,
        DomB,
        NonDomA,
        NonDomB
    }

    private HandInput handInput;

    //References

    [SerializeField] private HVRTrackedController HVRTrackedController;
    private void Start()
    {
       
        if(HVRTrackedController.HandSide == HurricaneVR.Framework.Shared.HVRHandSide.Right)
        {
            isRightHand = true;
        }
        else
        {
            isRightHand = false;    
        }

      handInputDirection = gameObject.GetComponent<HandInputDirection>();
    }

    void Update()
    {
        //if (!isPerformingAction)
        //{
        ReadInput();
        //}

    }

    private void ReadInput()
    {
        CheckIfIsPerformingAction();
        inputDirection = handInputDirection.GetInputDirectionOfHand();

        if(inputDirection == HandInputDirection.InputDirection.Neutral)
        {
            if(thisHandGrabisActivated && thisHandTriggerIsActivated)
            {
                Debug.Log("Shield");
            }
            if(thisHandTriggerIsActivated)
            {
                handInput = HandInput.NeutralA;
                onPerformingActionNeutralA?.Invoke(this, EventArgs.Empty);

            }
            else if(thisHandGrabisActivated)
            {
                handInput = HandInput.NeutralB;
            }
        }else if(inputDirection == HandInputDirection.InputDirection.Up)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.UpA;

            }
            else if (thisHandGrabisActivated)
            {
                handInput = HandInput.UpB;
            }
        }
        else if(inputDirection == HandInputDirection.InputDirection.Down)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.DownA;

            }
            else if (thisHandGrabisActivated)
            {
                handInput = HandInput.DownB;
            }
        }
        else if(inputDirection == HandInputDirection.InputDirection.DomSide)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.DomA;

            }
            else if (thisHandGrabisActivated)
            {
                handInput = HandInput.DomB;
            }
        }
        else if(inputDirection == HandInputDirection.InputDirection.NonDomSide)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.NonDomA;

            }
            else if (thisHandGrabisActivated)
            {
                handInput = HandInput.NonDomB;
            }
        }

        

    }

    public void SetIsNotPerformingAction()
    {
        isPerformingAction = false;
    }

    private void CheckIfIsPressingActionButtons()
    {

        if(isRightHand)
        {
            //BInputs Special Attacks
            thisHandGrabisActivated = HVRPlayerInputs.Instance.IsRightGrabActivated;
            
            //AInputs Smash Attacks
            thisHandTriggerIsActivated = HVRPlayerInputs.Instance.IsRightTriggerHoldActive;
            
        }
        else
        {
            //BInputs Special Attacks
            thisHandGrabisActivated = HVRPlayerInputs.Instance.IsLeftGrabActivated;
            
            //AInputs Smash Attacks
            thisHandTriggerIsActivated = HVRPlayerInputs.Instance.IsLeftTriggerHoldActive;
            
        }
        
    }

    private void CheckIfIsPerformingAction()
    {
        CheckIfIsPressingActionButtons();

        if(thisHandGrabisActivated || thisHandTriggerIsActivated)
        {
            isPerformingAction = true;
        }
    }
}
