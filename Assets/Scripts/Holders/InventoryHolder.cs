public class InventoryHolder
{
    private readonly int itemId;
    private readonly int equiped;
    private readonly int amount;
    private readonly int enchant;

    public InventoryHolder( int itemId, int equiped, int amount, int enchant)
    {
        this.itemId = itemId;
        this.equiped = equiped;
        this.amount = amount;
        this.enchant = enchant;
    }

    public int GetItemId()
    {
        return itemId;
    } 

    public int GetEquiped()
    {
        return equiped;
    }

    public int GetAmount()
    {
        return amount;
    }

    public int GetEnchant()
    {
        return enchant;
    }

}