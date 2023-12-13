using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InputManager : MonoBehaviour
{

    public abstract void nonDomHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionNeutralB(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionNeutralA(object sender, EventArgs e);


    public abstract void nonDomHandInputScript_onPerformingActionDownB(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionDownA(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionDownB(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionDownA(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionUpB(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionUpA(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionUpB(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionUpA(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionNonDomB(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionNonDomA(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionDomB(object sender, EventArgs e);

    public abstract void nonDomHandInputScript_onPerformingActionDomA(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionDomB(object sender, EventArgs e);

    public abstract void domHandInputScript_onPerformingActionDomA(object sender, EventArgs e);




}
