using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBaseScript : MonoBehaviour
{

    public abstract void ActionNeutralA(Transform handTransform, Quaternion handQuaternion);
    public abstract void ActionNeutralB();

    public abstract void ActionUpA();
    public abstract void ActionUpB();

    public abstract void ActionDownA();
    public abstract void ActionDownB();

    public abstract void ActionDomA();
    public abstract void ActionDomB();

    public abstract void ActionNonDomA();
    public abstract void ActionNonDomB();





}
