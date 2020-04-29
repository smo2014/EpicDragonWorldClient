using System.Collections;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public static int getItemChange { get; set; }
    public static int itemId { get; set; }
    public static int equipped { get; set; }
    public static int count { get; set; }
    public static int enchantLvl { get; set; }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            getItemChange = 1;
            itemId = 555;
            equipped = 0;
            enchantLvl = 0;
            NetworkManager.ChannelSend(new InventoryUpdateRequest(MainManager.Instance.selectedCharacterData.GetName()));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            getItemChange = 2;
            itemId = 555;
            equipped = 0;
            enchantLvl = 0;
            NetworkManager.ChannelSend(new InventoryUpdateRequest(MainManager.Instance.selectedCharacterData.GetName()));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            getItemChange = 3;
            itemId = 555;
            equipped = 0;
            enchantLvl = 0;
            NetworkManager.ChannelSend(new InventoryUpdateRequest(MainManager.Instance.selectedCharacterData.GetName()));
        }
    }
}

