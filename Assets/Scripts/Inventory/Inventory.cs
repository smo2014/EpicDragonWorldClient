using System.Collections;
using UnityEngine;

public class Inventory : ItemContainer
{
    public static Inventory Instance { get; private set; }

    public ItemDatabase itemDatabase;
    [SerializeField] protected Item[] startingItems;
    [SerializeField] Transform itemsParent;
    EquippableItem item;

    protected override void OnValidate()
    {
        if(itemsParent != null)
            itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);

        if (!Application.isPlaying)
        {
            SetStartingItems();
        }

    }

    protected override void Awake()
    {
        base.Awake();

        itemDatabase = Resources.Load<ItemDatabase>("Item Database");
        if (itemDatabase == null)
            Debug.Log("I dont find Item Database");
        
        SetStartingItems();
    }

    private void Start()
    {
        Instance = this;
    }

    public void SetStartingItems()
    {
        Clear();
//        AddEquippmentItemInventory(1);
    }

    public void ItemList(ArrayList itemList)
    {
        foreach (InventoryHolder inventoryItem in itemList)
        {
            int id = inventoryItem.GetItemId();
            //            Debug.Log("ItemID: " + inventoryItem.GetItemId() + " | Equiped: " + inventoryItem.GetEquiped() + " | Amount: " + inventoryItem.GetAmount() + " | Enchant: " + inventoryItem.GetEnchant());

        }
    }

/*    public void AddEquippmentItemInventory(int id)
    {
        item = new Item();
        item.ID = itemDatabase.Item[id].ID;
        item.ItemName = itemDatabase.Item[id].ItemName;
        item.Icon = itemDatabase.Item[id].Icon;
        item.MaximumStacksSize = 1;
        item.Strength = itemDatabase.Item[id].Strength;
        item.Agility = itemDatabase.Item[id].Agility;
        item.Intelligence = itemDatabase.Item[id].Intelligence;
        item.Vitality = itemDatabase.Item[id].Vitality;
        item.EquipmentType = itemDatabase.Item[id].Itemtype;

        AddItem(item);
    }
 */           /*
                // TODO: Add Inventory list from database
                public void CharacterItems(ArrayList itemList)
                {
                    foreach (InventoryHolder inventoryItem in itemList)
                    {
                        Debug.Log("ItemID: " + inventoryItem.GetItemId() + " | Equiped: " + inventoryItem.GetEquiped() + " | Amount: " + inventoryItem.GetAmount() + " | Enchant: " + inventoryItem.GetEnchant());

                        item.ItemID = inventoryItem.GetItemId();
                        item.ItemName = ItemData.GetItem(inventoryItem.GetItemId()).GetName();
                        item.Icon = sp;
                        item.MaximumStacks = 1;
                        item.EquipmentItemSlot = ItemData.GetItem(inventoryItem.GetItemId()).GetItemSlot();
                        item.AgilityBonus = 1;
                        item.IntelligenceBonus = 1;
                        item.VitalityBonus = 1;
                        item.StrengthBonus = 1;
                        AddItem(item);

                        inventoryList.Add(item);
                        //string name = ItemData.GetItem(inventoryItem.GetItemId()).GetName();
                        //int itemId = ItemData.GetItem(inventoryItem.GetItemId()).GetItemId();
                        //Sprite icon = Resources.Load<Sprite>("ItemIcons/" +ItemData.GetItem(inventoryItem.GetItemId()).GetRecipeFemale().ToString().Replace("_Recipe", ""));
                        //Debug.Log("Item to add in Game DB - Id: " + itemId + " Name: "+ name + " ,icon: " +icon.name);

                    }
                }
            */
    }
