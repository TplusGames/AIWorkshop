using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemInstance> _items = new List<ItemInstance>();
    private float _currentCarryWeight;

    [SerializeField] private float maxCarryWeight;

    public void AddItemToInventory(ItemInstance item)
    {
        _currentCarryWeight += item.ItemInfo.CarryWeight;
        _items.Add(item);
        Debug.Log($"{item.ItemInfo.ItemName} added to inventory. Current carry weight is {_currentCarryWeight}");
    }

    public void RemoveItemFromInventory(ItemInstance item)
    {
        _currentCarryWeight -= item.ItemInfo.CarryWeight;
        _items.Remove(item);
        Debug.Log($"{item.ItemInfo.ItemName} removed from inventory. Current carry weight is {_currentCarryWeight}");
    }

    public bool InventoryHasSpaceForItem(float itemWeight)
    {
        return itemWeight + _currentCarryWeight <= maxCarryWeight;
    }

    public List<ItemInstance> GetAllItemsInInventory()
    {
        return _items;
    }
}
