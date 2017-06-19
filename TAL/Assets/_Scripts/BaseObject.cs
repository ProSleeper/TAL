using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OBJECTTYPE
{
    OT_NONE,
    OT_ACTOR,
    OT_ITEM,
    MAX
}

public class BaseObject : MonoBehaviour 
{
    protected float AttackPower = 0;
    protected float DefencePower = 0;

    protected OBJECTTYPE ObjectType = OBJECTTYPE.OT_NONE;

    public float AP
    {
        get
        {
            return AttackPower;
        }
        set
        {
            AttackPower = value;
        }
    }

    public float DP
    {
        get
        {
            return DefencePower;
        }
        set
        {
            DefencePower = value;
        }
    }

    public OBJECTTYPE OT
    {
        get
        {
            return ObjectType;
        }
    }
}
