using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    //Instance
    public static MovementManager instance;

    //Events

    public event EventHandler onDomHandSet;


    // Enums

    public enum Hand
    {
        Right,
        Left
    }

    private Hand hand;

    public enum InputDirection
    {
        Neutral,
        Up,
        Down,
        Left,
        Right
    }

    private InputDirection inputDirection;  

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void Start()
    {
        


        onDomHandSet?.Invoke(this, EventArgs.Empty);
    }

}
