using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour 
{
    protected string ItemName = string.Empty;
    protected float AttackPower = 0;
    protected float DefencePower = 0;

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

    public string NAME
    {
        get
        {
            return ItemName;
        }
        set
        {
            ItemName = value;
        }
    }

    public void ItemDesc()
    {
        ItemManager.Instance.Description(this);
    }
}
