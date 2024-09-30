using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonobehavoiur<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDetailsDictionary;
    public List<InventoryItem>[] inventoryLists ;
    [HideInInspector] public int[] inventoryListCapacityIntArray;//index of list is the inventory list, value is the capacity of that list
    [SerializeField] private ItemListSO itemList = null;
    public Player player;


    protected override void Awake()
    {
        base.Awake();
        CreateInventoryLists();
        CreateItemDetailsDictionary();

    }

    private void CreateInventoryLists()
    {
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
        //generate a list for each potential place in the game where an inventory is needed
        for (int i =0; i < (int)InventoryLocation.count; i++)
        {
  
            inventoryLists[i] = new List<InventoryItem>();
        }
        //set the capacity of each list
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];
        //set the capacity of the player inventory, more can be added later just add to the enum.
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObject)
    {
        AddItem(inventoryLocation, item);
        Destroy(gameObject);    
    }
    public void AddItem(InventoryLocation inventoryLocation , Item item)
    {
        int itemCode = item.ItemCode;   
        //get player inventory
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        int itemPosition = FindItemInInventory(inventoryLocation,itemCode);
        if (itemPosition != -1)
        {
            //if it already exists in the inventory, increase the quantity
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            //if it doesn't exist in the inventory, add it
            AddItemAtPosition(inventoryList, itemCode);
        }
        player.inventoryUpdateEvent.CallInventoryUpdateEvent(inventoryLocation, inventoryList);
    }

    public int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
        }
        return -1;
    }
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity + 1;
        inventoryItem.itemQuantity = quantity;
        inventoryItem.itemCode = itemCode;
        inventoryList[position] = inventoryItem;
        Debug.ClearDeveloperConsole();
        DebugPrintInventoryList(inventoryList);

    }
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.itemCode = itemCode;
        inventoryItem.itemQuantity = 1;
        inventoryList.Add(inventoryItem);

        DebugPrintInventoryList(inventoryList);
    }


    private void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach (ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails);
        }

    }

    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;

        if(itemDetailsDictionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else
        {
            return null;
        }
    }

    public void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        foreach(InventoryItem inventoryItem in inventoryList)
        {
            Debug.Log("Item Description" + InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode).itemDescription + " Quantity: " + inventoryItem.itemQuantity);
        }
        Debug.Log("****************************************************************************************************");
    }

}

