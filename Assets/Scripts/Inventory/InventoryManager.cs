using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlotSaveData
{
    public string ItemID;
    public int Amount;
    public int Enchant;

    public ItemSlotSaveData(string id, int amount, int enchant)
    {
        ItemID = id;
        Amount = amount;
        Enchant = enchant;
    }
}

[Serializable]
public class ItemContainerSaveData
{
    public ItemSlotSaveData[] SavedSlots;

    public ItemContainerSaveData(int numItems)
    {
        SavedSlots = new ItemSlotSaveData[numItems];
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public ItemDatabase itemDatabase;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";
    public ArrayList itemsList;

    private void Start()
    {
        Instance = this;
        itemsList = new ArrayList();

        itemDatabase = Resources.Load<ItemDatabase>("Item Database");
        if (itemDatabase == null)
            Debug.Log("I dont find Item Database in Resources folder.");


    }


    public void ItemsList(ArrayList invList)
    {
        foreach(InventoryHolder inventoryHolder in invList)
        {
            inventoryHolder.GetItemId();
            inventoryHolder.GetEquiped();
            inventoryHolder.GetAmount();
            inventoryHolder.GetEnchant();
            itemsList.Add(inventoryHolder);
        }
    }

    public void LoadInventory(Character character)
    {
        character.Inventory.Clear();

        int _slot = 0;
        foreach(InventoryHolder inventoryHolder in itemsList)
        {
            ItemSlot itemSlot = character.Inventory.ItemSlots[_slot];
            itemSlot.Item = itemDatabase.GetItemId(inventoryHolder.GetItemId());
            itemSlot.Amount = inventoryHolder.GetAmount();
            itemSlot.Enchant = inventoryHolder.GetEnchant();
            if(inventoryHolder.GetEquiped() == 1)
            {
                character.Equip((EquippableItem)itemSlot.Item);
            }
            _slot++;
        }

        // Add Chest Reward for Test 20 x ItemId = 1000;
        for (int i = 0; i < 20; i++)
        {
            character.Inventory.AddItem(itemDatabase.GetItemId(1000));
        }
    }

    public void LoadEquipment(Character character)
    {
        //        Item item1 = itemDatabase.GetItemId(2);
        //        character.Inventory.AddItem(itemDatabase.GetItemId(1));
        //        character.Inventory.AddItem(itemDatabase.GetItemId(2));
        //        character.Inventory.AddItem(itemDatabase.GetItemId(3));
        //        character.Inventory.AddItem(itemDatabase.GetItemId(4));
        //        character.Inventory.AddItem(itemDatabase.GetItemId(5));


        // TODO ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);
        ItemContainerSaveData savedSlots = null; //REMOVE after TODO
        if (savedSlots == null) return;

        foreach (ItemSlotSaveData savedSlot in savedSlots.SavedSlots)
        {
            if(savedSlot == null)
            {
                continue;
            }

            Item item = itemDatabase.GetItemCopy(savedSlot.ItemID);
            character.Inventory.AddItem(item);
            character.Equip((EquippableItem)item);
        }
    }

    // Save Inventory Items
    public void SaveInventory(Character character)
    {
        SaveItems(character.Inventory.ItemSlots, InventoryFileName);
    }

    // Save Equipment Items
    public void SaveEquipment(Character character)
    {
        SaveItems(character.EquipmentPanel.EquipmentSlots, EquipmentFileName);
    }

    private void SaveItems(IList<ItemSlot> itemSlots, string fileName)
    {
        var saveData = new ItemContainerSaveData(itemSlots.Count);
        for(int i=0; i < saveData.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = itemSlots[i];

            if(itemSlot.Item == null)
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID, itemSlot.Amount, 1); // TODO: Addin BaseItemSlot Enchant
                Debug.Log("Save to database: " + itemSlot.Item.itemId + " | " + itemSlot.Amount + " | equiped or not |" + " enchant lvl");
            }
        }
        Debug.Log("TODO: Add in BaseItemSlot Enchant");

        Debug.Log("Inventory Manager save data TODO");
    }
}
