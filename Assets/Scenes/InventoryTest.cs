using System.Collections;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public static InventoryTest instance {get;set;}
    public static int getItemChange { get; set; }
    public static int itemId { get; set; }
    public static int equipped { get; set; }
    public static int count { get; set; }
    public static int enchantLvl { get; set; }
    public ArrayList itemsList;
    Item[] items;

    private void Start()
    {
        instance = this;
        itemsList = new ArrayList();

    }
    
    public void LoadInventory(Character character)
    {
        character.Inventory.Clear();
        int _slot = 0;
        foreach(InventoryHolder inventoryHolder in itemsList)
        {
            ItemSlot itemSlot = character.Inventory.ItemSlots[_slot];
            itemSlot.Item = GetItemId(inventoryHolder.GetItemId());
            itemSlot.Amount = inventoryHolder.GetAmount();
            itemSlot.Enchant = inventoryHolder.GetEnchant();
        }
        _slot++;
    }


    // remove GetCopy in future...
    public Item GetItemId(int id)
    {
        foreach (Item item in items)
        {
            return item != null ? item.GetCopy() : null;
        }
        return null;
    }



    public void InventoryList(ArrayList invList)
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

    public void ItemAddTest()
    {
        getItemChange = 1;
        itemId = 555;
        equipped = 0;
        enchantLvl = 0;
        Debug.Log("Send packet");
        NetworkManager.ChannelSend(new InventoryUpdateRequest(MainManager.Instance.selectedCharacterData.GetName()));
    }
}

