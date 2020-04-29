using UnityEngine;

public class InventoryUpdateRequest : SendablePacket
{
	public InventoryUpdateRequest(string charName)
	{
		WriteShort(15); // Packet Id.
		WriteString(charName);
		WriteInt(InventoryTest.getItemChange); // Update type : 01-add, 02-modify, 03-remove

		switch (InventoryTest.getItemChange)
		{
			case 1:
				Debug.Log("Add Item.");
				WriteInt(InventoryTest.itemId);
				WriteInt(InventoryTest.equipped);
				WriteInt(InventoryTest.count);
				WriteInt(InventoryTest.enchantLvl);
				break;
			case 2: Debug.Log("Modify Item.");
				WriteInt(InventoryTest.itemId);
				WriteInt(InventoryTest.equipped);
				WriteInt(InventoryTest.count);
				WriteInt(InventoryTest.enchantLvl);
				break;
			case 3: Debug.Log("Remove Item.");
				WriteInt(InventoryTest.itemId);
				WriteInt(InventoryTest.equipped);
				WriteInt(InventoryTest.count);
				WriteInt(InventoryTest.enchantLvl);
				break;
		}
	}
}