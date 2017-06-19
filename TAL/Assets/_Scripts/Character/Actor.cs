using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BaseObject
{
    protected List<BaseItem> ItemList = new List<BaseItem>();
	protected List<GameObject> ItemObject = new List<GameObject>();
	float CurrentDamage = 0f;
    float CurrentDefence = 0f;

    public List<BaseItem> ITEMLIST
    {
        get
        {
            return ItemList;
        }
        set
        {
            ItemList = value;
        }
    }

    private void Awake()
    {
        ObjectType = OBJECTTYPE.OT_ACTOR;
    }

    private void Start()
    {
		AddItem(ItemManager.Instance.CreateItem());
		AddItem(ItemManager.Instance.CreateItem());
	}


    public void CalculateStatus()
    {
        CurrentDamage = this.AP;
        CurrentDefence = this.DP;

        foreach (BaseItem item in ItemList)
        {
            CurrentDamage += item.AP;
            CurrentDefence += item.DP;
        }
    }

    public float TotalDamage()
    {
        return CurrentDamage;
    }

    public float TotalDefence()
    {
        return CurrentDefence;
    }

	public void ThrowItem()
	{
		InventoryManager.Instance.PutItem(ItemObject);
	}

    public void AddItem(GameObject pItem)
    {
        pItem.transform.SetParent(this.transform);
		pItem.transform.localPosition = Vector3.zero;
		pItem.transform.localScale = Vector3.one;
		pItem.SetActive(false);
		ItemObject.Add(pItem);
    }
}