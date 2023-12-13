using HurricaneVR.Framework.ControllerInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputScript : MonoBehaviour
{
    
    [SerializeField] private bool canPerformAction;

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

    [SerializeField] private bool thisHandHoldisActivated;
    [SerializeField] private bool otherDomHandHoldisActivated;

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
        if (canPerformAction)
        {
            ReadInput();
        }

    }

    private void ReadInput()
    {
        CheckIfIsPerformingAction();
        inputDirection = handInputDirection.GetInputDirectionOfHand();

        if(inputDirection == HandInputDirection.InputDirection.Neutral)
        {
            if(thisHandHoldisActivated && thisHandTriggerIsActivated)
            {
                Debug.Log("Shield");
            }
            if(thisHandTriggerIsActivated)
            {
                handInput = HandInput.NeutralA;
                onPerformingActionNeutralA?.Invoke(this, EventArgs.Empty);

            }
            else if(thisHandHoldisActivated)
            {
                handInput = HandInput.NeutralB;
                onPerformingActionNeutralB?.Invoke(this, EventArgs.Empty);
            }
        }else if(inputDirection == HandInputDirection.InputDirection.Up)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.UpA;
                onPerformingActionUpA?.Invoke(this, EventArgs.Empty);

            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.UpB;
                onPerformingActionUpB?.Invoke(this, EventArgs.Empty);
            }
        }
        else if(inputDirection == HandInputDirection.InputDirection.Down)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.DownA;
                onPerformingActionDownA?.Invoke(this, EventArgs.Empty);

            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.DownB;
                onPerformingActionDownB?.Invoke(this, EventArgs.Empty);
            }
        }
        else if(inputDirection == HandInputDirection.InputDirection.DomSide)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.DomA;
                onPerformingActionDomA?.Invoke(this, EventArgs.Empty);

            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.DomB;
                onPerformingActionDomB?.Invoke(this, EventArgs.Empty);
            }
        }
        else if(inputDirection == HandInputDirection.InputDirection.NonDomSide)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.NonDomA;
                onPerformingActionNonDomA?.Invoke(this, EventArgs.Empty);

            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.NonDomB;
                onPerformingActionNonDomB?.Invoke(this, EventArgs.Empty);
            }
        }

        //Debug.Log(handInput.ToString());

    }

    public void SetIsNotPerformingAction()
    {
        canPerformAction = false;
    }

    private void CheckIfIsPressingActionButtons()
    {

        if(isRightHand)
        {
            //BInputs Special Attacks
            thisHandHoldisActivated = HVRPlayerInputs.Instance.IsRightHoldActive;
            
            //AInputs Smash Attacks
            thisHandTriggerIsActivated = HVRPlayerInputs.Instance.IsRightTriggerHoldActive;
            
        }
        else
        {
            //BInputs Special Attacks
            thisHandHoldisActivated = HVRPlayerInputs.Instance.IsLeftHoldActive;

            //AInputs Smash Attacks
            thisHandTriggerIsActivated = HVRPlayerInputs.Instance.IsLeftTriggerHoldActive;
            
        }
        
    }

    private void CheckIfIsPerformingAction()
    {
        CheckIfIsPressingActionButtons();

        //if(thishandgrabisactivated || thishandtriggerisactivated)
        //{
        //    canperformaction = true;
        //}
    }

    public bool GetCanPerformingAction()
    {
        return canPerformAction;
    }

    public void SetCanPerformAction(bool canPerformAction)
    {
        this.canPerformAction = canPerformAction;
    }

    public bool GetThisHandTriggerIsActivated()
    {
        return thisHandTriggerIsActivated;
    }

    public bool GetThisHandHoldIsActivated()
    {
        return thisHandHoldisActivated;
    }
}
