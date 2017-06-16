using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    private void Start()
    {
        CreateItem();
        CreateItem();
    }

    public void CreateItem()
    {
        GameObject item = Resources.Load("Item") as GameObject;
        GameObject newItem = NGUITools.AddChild(GameObject.Find("ItemDetail"), item);

        newItem.GetComponent<BaseItem>().NAME = "아이템" + Random.Range(0, 100).ToString();
        newItem.GetComponent<BaseItem>().AP = Random.Range(200, 300);
        newItem.GetComponent<BaseItem>().DP = Random.Range(300, 400);
        newItem.name = "Item";

        
    }


    public void Description(BaseItem pItem)
    {
        UIManager.Instance.PrintItemStat(pItem);
    }
}
