using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUpdateEvent : MonoBehaviour
{

    public event Action<InventoryLocation, List<InventoryItem>> OnInventoryUpdate;

    public void CallInventoryUpdateEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        OnInventoryUpdate?.Invoke(inventoryLocation, inventoryList);
    }

}
