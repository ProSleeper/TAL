using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : BaseObject
{
	const float MOVETIME = 0.5f;

    protected string ItemName = string.Empty;

	bool IsMove = false;
	float dTime = 0f;
	Vector3 StartPos = Vector3.zero;
	Vector3 EndPos = Vector3.zero;

	private void Awake()
    {
        ObjectType = OBJECTTYPE.OT_ITEM;
    }

	private void Update()
	{
		if (!IsMove) return;

		dTime += Time.deltaTime;
		this.transform.localPosition = Vector3.Lerp(StartPos, EndPos, dTime / MOVETIME);
		if (dTime > MOVETIME)
		{
			IsMove = false;
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

	public void LerfMove(Vector3 pStart, Vector3 pEnd)
	{
		StartPos = pStart;
		EndPos = pEnd;
		IsMove = true;
	}
}