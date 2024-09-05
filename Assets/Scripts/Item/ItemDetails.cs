using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemDetails 
{
    public int itemCode;
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public short itemUseGridRadius;
    public bool canBePickedUp;
    public bool canBeDropped;
    public bool canBeEaten;
    public bool canBePlaced;
}
