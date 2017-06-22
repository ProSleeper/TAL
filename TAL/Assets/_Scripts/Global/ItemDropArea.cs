using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AREA
{
	NONE,
	DETAIL,
	INVEN,
	INVENBACK,
	MAX
}

public class ItemDropArea : MonoBehaviour
{
	public AREA CurrentArea = AREA.NONE;

	public AREA CURRENTAREA
	{
		get
		{
			return CurrentArea;
		}
		set
		{
			CurrentArea = value;
		}
	}
}
