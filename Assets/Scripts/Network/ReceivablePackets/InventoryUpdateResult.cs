using System.Collections;
using UnityEngine;

public class InventoryUpdateResult
{
    public static void Notify(ReceivablePacket packet)
    {
        // Read Data
        int listSize = packet.ReadInt();

        ArrayList itemList = new ArrayList(listSize);

        for (int i = 0; i < listSize; i++)
        {
            int change = packet.ReadInt();
            int itemId = packet.ReadInt();
            int equipped = packet.ReadInt();
            int amount = packet.ReadInt();
            int enchant = packet.ReadInt();

            Debug.Log("Change: " + change + " | Item Id: " + itemId);
        }
    }
}
