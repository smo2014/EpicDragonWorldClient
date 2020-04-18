using System;
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
    public ItemDatabase itemDatabase;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";

    private void Start()
    {
        itemDatabase = Resources.Load<ItemDatabase>("Item Database");
        if (itemDatabase == null)
            Debug.Log("I dont find Item Database in Resources folder.");
    }

    public void LoadInventory(Character character)
    {
 //       ItemSlot itemSlot1 = character.Inventory.ItemSlots[0];
//        itemSlot1.Item = itemDatabase.GetItemId(1);
//       itemSlot1.Amount = 1;





        // TODO: ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
        ItemContainerSaveData savedSlots = null; // REMOVE after TODO
        if (savedSlots == null) return;

        character.Inventory.Clear();

        for (int i = 0; i < savedSlots.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = character.Inventory.ItemSlots[i];
            ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

            if(savedSlot == null)
            {
                itemSlot.Item = null;
                itemSlot.Amount = 0;
            }
            else
            {
                itemSlot.Item = itemDatabase.GetItemCopy(savedSlot.ItemID);
                itemSlot.Amount = savedSlot.Amount;
            }
        }
    }

    public void LoadEquipment(Character character)
    {
//        Item item1 = itemDatabase.GetItemId(2);
        
        character.Inventory.AddItem(itemDatabase.GetItemId(1));
        character.Inventory.AddItem(itemDatabase.GetItemId(2));
        character.Inventory.AddItem(itemDatabase.GetItemId(3));
        character.Inventory.AddItem(itemDatabase.GetItemId(4));
        character.Inventory.AddItem(itemDatabase.GetItemId(5));


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
