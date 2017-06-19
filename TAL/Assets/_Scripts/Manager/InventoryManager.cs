using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InventoryManager : MonoSingleton<InventoryManager> 
{
	GameObject Grid = null;
	List<GameObject> Blank = new List<GameObject>();

	void Start ()
	{
		Grid = GameObject.Find("Grid");
		for (int i = 0; i < Grid.transform.childCount; i++)
		{
			Blank.Add(Grid.transform.GetChild(i).gameObject);
		}
	}

	public void PutItem(List<GameObject> pItem)
	{
		for (int i = 0; i < pItem.Count; i++)
		{
			pItem[i].transform.SetParent(Blank[i].transform);
			//pItem[i].transform.localPosition = Vector3.zero;
			pItem[i].SetActive(true);
			pItem[i].GetComponent<BaseItem>().LerfMove(pItem[i].transform.localPosition, Vector3.zero);
		}
	}

	public void ClearInventory()
	{
		foreach (GameObject item in Blank)
		{
			item.transform.DestroyChildren();
		}
	}

}
