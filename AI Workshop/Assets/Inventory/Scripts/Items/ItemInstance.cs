using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemInfo ItemInfo;

    public void SetItemInfo(ItemInfo info)
    {
        ItemInfo = info;
    }
}
