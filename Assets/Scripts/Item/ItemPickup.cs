using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
///attached to the player and when the player collides with an item, do stuff
///each item prefab has a boxCollider set to trigger, it then will call the OnTriggerEnter2D method once collided
///and then we can get the collided item, then we can get the itemDetails using the itemCode
/// </summary>
/// 
public class ItemPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if (item != null)
        {
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);
            if (itemDetails.canBePickedUp)
            {
                InventoryManager.Instance.AddItem(InventoryLocation.player, item, collision.gameObject);

            }
        }
    }


}
