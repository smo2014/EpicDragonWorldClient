public class EquipmentSlot : ItemSlot
{
    public EquipmentItemSlot EquipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null) return true;

        EquippableItem equippableItem = item as EquippableItem;
//        equippableItem.EnchantLvl = item.GetEnchant();
        return equippableItem != null && equippableItem.EquipmentType == EquipmentType;
    }
}
