using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UITYPE
{
    ITEMDESC,
    MAX
}

public class UIManager : MonoSingleton<UIManager>
{
    Transform ItemDescLabel = null;
    UILabel ItemName = null;
    UILabel ItemAP = null;
    UILabel ItemDP = null;

    private void Awake()
    {
        ItemDescLabel = GameObject.Find("ItemDescription").transform;
        ItemName = ItemDescLabel.FindChild("Name").GetComponent<UILabel>();
        ItemAP = ItemDescLabel.FindChild("Attack").GetComponent<UILabel>();
        ItemDP = ItemDescLabel.FindChild("Defence").GetComponent<UILabel>();

        Debug.Log("실행횟수");

        if (ItemDescLabel == null)
        {
            Debug.Log("ItemDescLabel 없음");
        }
        if (ItemName == null)
        {
            Debug.Log("ItemName 없음");
        }
        if (ItemAP == null)
        {
            Debug.Log("ItemAP 없음");
        }
        if (ItemDP == null)
        {
            Debug.Log("ItemDP 없음");
        }
    }

    public void PrintItemStat(BaseItem pItem)
    {
        ItemName.text = pItem.NAME;
        ItemAP.text = pItem.AP.ToString();
        ItemDP.text = pItem.DP.ToString();
    }    

}
