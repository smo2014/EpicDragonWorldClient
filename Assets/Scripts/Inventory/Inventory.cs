using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] protected Item[] startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item> OnItemRightClickedEvent;

    public void Init()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        
        for (int i=0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }




        for (int i =0; i < startingItems.Length; i++)
        {
//            Debug.Log("Item : " + items[i].Icon.name);
        }
    }
    // TODO: Add Inventory list from database
    public void CharacterItems(ArrayList itemList)
    {
        
        foreach (InventoryHolder inventoryItem in itemList)
        {
           //Debug.Log("ItemID: " + inventoryItem.GetItemId() + " | Equiped: " + inventoryItem.GetEquiped() + " | Amount: " + inventoryItem.GetAmount() + " | Enchant: " + inventoryItem.GetEnchant());
            string name = ItemData.GetItem(inventoryItem.GetItemId()).GetName();
            //Sprite icon = Resources.Load<Sprite>("ItemIcons/" +ItemData.GetItem(inventoryItem.GetItemId()).GetRecipeFemale().ToString().Replace("_Recipe", ""));
            Debug.Log("Item to add in Game DB: " + name/* + " ,icon: " +icon.name*/);
            
        }


    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

        SetStartingItems();
    }

    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Length && i < itemSlots.Length; i++)
        {                                                      
            itemSlots[i].Item = Instantiate(startingItems[i]);
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        for(int i=0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }
    
    public bool RemoveItem(Item item)
    {
        for(int i=0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item)
            {
                itemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }

    public Item RemoveItem(string itemID)
    {
        for(int i=0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if(item != null && item.ID == itemID)
            {
                itemSlots[i].Item = null;
                return item;
            }
        }
        return null;
    }

    public bool IsFull()
    {
        for(int i=0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            { 
                return false;
            }
        }
        return true;
    }

    public int ItemCount(string itemID)
    {
        int number = 0;

        for (int i=0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item.ID == itemID)
            {
                number++;
            }
        }
        return number;
    }
}
