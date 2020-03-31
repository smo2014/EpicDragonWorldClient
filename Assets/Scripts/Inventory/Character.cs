using edw.CharacterStats;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Vitality;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        if(statPanel != null)
        {
            statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
            statPanel.UpdateStatValues();
        }

        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;

        inventory.Init();
        equipmentPanel.Init();
    }

    private void EquipFromInventory(Item item)
    {
        if(item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Item item)
    {
        if(item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    if(statPanel != null)
                        statPanel.UpdateStatValues();
                }
                item.Equip(this);
                if(statPanel != null)
                    statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            if(statPanel != null)
                statPanel.UpdateStatValues();

            inventory.AddItem(item);
        }
    }
}
