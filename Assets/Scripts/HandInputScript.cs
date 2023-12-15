using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.ControllerInput.InputEvents;
using HurricaneVR.Framework.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandInputScript : MonoBehaviour
{
    //HVR Controllers
    private HVRController thisHVRController;


    [SerializeField] private bool canPerformSmash = true;
    [SerializeField] private bool canPerformSpecial = true;

    [SerializeField] private bool actionIsEnding = false;

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

    private void Awake()
    {






    }
    private void Start()
    {

        if (HVRTrackedController.HandSide == HurricaneVR.Framework.Shared.HVRHandSide.Right)
        {
            isRightHand = true;
            thisHVRController = HVRInputManager.Instance.RightController;

        }
        else
        {
            isRightHand = false;
            thisHVRController = HVRInputManager.Instance.LeftController;
        }

        handInputDirection = gameObject.GetComponent<HandInputDirection>();
    }

    void Update()
    {

        CheckIfIsPerformingAction();





        SetRealeasedInput();

        SetInput();


        if (canPerformSmash)
        {
            ReadInput();
        }

    }



    private void ReadInput()
    {

        inputDirection = handInputDirection.GetInputDirectionOfHand();

        if (inputDirection == HandInputDirection.InputDirection.Neutral)
        {
            if (thisHandHoldisActivated && thisHandTriggerIsActivated)
            {
                Debug.Log("Shield");
            }
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.NeutralA;
                canPerformSmash = false;

            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.NeutralB;
                canPerformSmash = false;

            }
        }
        else if (inputDirection == HandInputDirection.InputDirection.Up)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.UpA;
                canPerformSmash = false;


            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.UpB;
                canPerformSmash = false;

            }
        }
        else if (inputDirection == HandInputDirection.InputDirection.Down)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.DownA;
                canPerformSmash = false;


            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.DownB;
                canPerformSmash = false;

            }
        }
        else if (inputDirection == HandInputDirection.InputDirection.DomSide)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.DomA;
                canPerformSmash = false;


            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.DomB;
                canPerformSmash = false;

            }
        }
        else if (inputDirection == HandInputDirection.InputDirection.NonDomSide)
        {
            if (thisHandTriggerIsActivated)
            {
                handInput = HandInput.NonDomA;
                canPerformSmash = false;


            }
            else if (thisHandHoldisActivated)
            {
                handInput = HandInput.NonDomB;
                canPerformSmash = false;

            }
        }

        //Debug.Log(handInput.ToString());

    }

    public void SetRealeasedInput()
    {

        if (thisHVRController.GripButtonState.JustDeactivated)
        {
            actionIsEnding = true;
            if (handInput == HandInput.NeutralA)
            {
                onPerformingActionNeutralA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.DownA)
            {
                onPerformingActionDownA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.UpA)
            {
                onPerformingActionUpA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.DomA)
            {
                onPerformingActionDomA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.NonDomA)
            {
                onPerformingActionNonDomA?.Invoke(this, EventArgs.Empty);
            }
        }

        if(thisHVRController.TriggerButtonState.JustDeactivated)
            {
                actionIsEnding = true;
                if (handInput == HandInput.NonDomB)
                {
                    onPerformingActionNonDomB?.Invoke(this, EventArgs.Empty);
                }
                if (handInput == HandInput.DomB)
                {
                    onPerformingActionDomB?.Invoke(this, EventArgs.Empty);
                }
                if (handInput == HandInput.UpB)
                {
                    onPerformingActionUpB?.Invoke(this, EventArgs.Empty);
                }
                if (handInput == HandInput.DownB)
                {
                    Debug.Log(handInput);
                    onPerformingActionDownB?.Invoke(this, EventArgs.Empty);
                }
                if (handInput == HandInput.NeutralB)
                {
                    onPerformingActionNeutralB?.Invoke(this, EventArgs.Empty);
                }
                Debug.Log(handInput);
            }
            

    }

    private void SetInput()
    {
        if (thisHandTriggerIsActivated)
        {
            actionIsEnding = false;
            if (handInput == HandInput.NeutralA)
            {
                onPerformingActionNeutralA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.DownA)
            {
                onPerformingActionDownA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.UpA)
            {
                onPerformingActionUpA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.DomA)
            {
                onPerformingActionDomA?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.NonDomA)
            {
                onPerformingActionNonDomA?.Invoke(this, EventArgs.Empty);
            }

        }
        if (thisHandHoldisActivated)
        {
            actionIsEnding = false;
            if (handInput == HandInput.NonDomB)
            {
                onPerformingActionNonDomB?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.DomB)
            {
                onPerformingActionDomB?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.UpB)
            {
                onPerformingActionUpB?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.DownB)
            {
                onPerformingActionDownB?.Invoke(this, EventArgs.Empty);
            }
            if (handInput == HandInput.NeutralB)
            {
                onPerformingActionNeutralB?.Invoke(this, EventArgs.Empty);
            }
        }


    }

    public void SetIsNotPerformingAction()
    {
        canPerformSmash = false;
    }

    private void CheckIfIsPressingActionButtons()
    {

        if (isRightHand)
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

        if (!thisHandHoldisActivated && !thisHandTriggerIsActivated)
        {
            canPerformSmash = true;


        }


    }

    public bool GetCanPerformingAction()
    {
        return canPerformSmash;
    }

    public void SetCanPerformAction(bool canPerformAction)
    {
        this.canPerformSmash = canPerformAction;
    }
    public void SetActionIsEnding(bool actionIsEnding)
    {
        this.actionIsEnding = this.actionIsEnding;
    }

    public bool GetActionIsEnding()
    {
        return actionIsEnding;
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
