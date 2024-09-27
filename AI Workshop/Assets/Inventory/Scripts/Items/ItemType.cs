using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Item Type")]
public class ItemType : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public float CarryWeight;
    public float Value;

    public GameObject DropPrefab;

    private void Awake()
    {
        if (ItemMasterList.ItemTypes.Contains(this))
        {
            return;
        }
        ItemMasterList.ItemTypes.Add(this);
    }
    
    public virtual ItemInstance CreateItemInstance()
    {
        var itemInfo = new ItemInfo();
        itemInfo.InitializeItemInfo(this);
        var itemInstance = new ItemInstance();
        itemInstance.SetItemInfo(itemInfo);
        return itemInstance;
    }
}

[System.Serializable]
public class ItemInfo
{
    public string ItemName;
    public string ItemDescription;
    public float CarryWeight;
    public float Value;
    public int ItemTypeIndex;

    public void InitializeItemInfo(ItemType itemType)
    {
        ItemName = itemType.ItemName;
        ItemDescription = itemType.ItemDescription;
        CarryWeight = itemType.CarryWeight;
        Value = itemType.Value;
        ItemTypeIndex = ItemMasterList.ItemTypes.IndexOf(itemType);
    }
}
