using System.Collections;
using UnityEngine;

public class CharacterInventoryResult
{

    public static void Notify(ReceivablePacket packet)
    {
        // Read Data
        int listSize = packet.ReadInt();
        ArrayList itemList = new ArrayList(listSize);

        for(int i=0; i<listSize; i++)
        {
            int itemID = packet.ReadInt();
            int equiped = packet.ReadInt();
            int amount = packet.ReadInt();
            int enchant = packet.ReadInt();

            InventoryHolder itemData = new InventoryHolder(itemID, equiped, amount,enchant);
            itemList.Add(itemData);

            // Inventory.Instance.AddItemInventory(itemID);
        }
//        Inventory.Instance.ItemList(itemList);
    }

}


