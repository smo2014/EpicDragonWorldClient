using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }


    public Image _headSlot;
    public Image _chestSlot;
    public Image _glovesSlot;
    public Image _legsSlot;
    public Image _bootsSlot;

    CharacterDataHolder _player;

    void Start()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;

        _player = MainManager.Instance.selectedCharacterData;
        ShowPlayerEquipment(_player);
    }

    private void ShowPlayerEquipment(CharacterDataHolder player)
    {
        switch (player.GetRace())
        {
            case 0: // Male Equipment
                if (player.GetHeadItem() != 0)
                {
                    LoadSprite(_headSlot, ItemData.GetItem(player.GetHeadItem()).GetRecipeMale().ToString().Replace("_Recipe", ""));
                }
                if (player.GetChestItem() != 0)
                {
                    LoadSprite(_chestSlot, ItemData.GetItem(player.GetChestItem()).GetRecipeMale().ToString().Replace("_Recipe", ""));
                }
                if (player.GetHandsItem() != 0)
                {
                    LoadSprite(_glovesSlot, ItemData.GetItem(player.GetHandsItem()).GetRecipeMale().ToString().Replace("_Recipe", ""));
                }
                if (player.GetLegsItem() != 0)
                {
                    LoadSprite(_legsSlot, ItemData.GetItem(player.GetLegsItem()).GetRecipeMale().ToString().Replace("_Recipe", ""));
                }
                if (player.GetFeetItem() != 0)
                {
                    LoadSprite(_bootsSlot, ItemData.GetItem(player.GetFeetItem()).GetRecipeMale().ToString().Replace("_Recipe", ""));
                }
                break;

            case 1: // Female Equipment
                
                if(player.GetHeadItem() != 0)
                {
                    LoadSprite(_headSlot, ItemData.GetItem(player.GetHeadItem()).GetRecipeFemale().ToString().Replace("_Recipe", ""));
                }
                if(player.GetChestItem() != 0)
                {
                    LoadSprite(_chestSlot, ItemData.GetItem(player.GetChestItem()).GetRecipeFemale().ToString().Replace("_Recipe", ""));
                }
                if (player.GetHandsItem() != 0)
                {
                    LoadSprite(_glovesSlot, ItemData.GetItem(player.GetHandsItem()).GetRecipeFemale().ToString().Replace("_Recipe", ""));
                }
                if (player.GetLegsItem() != 0)
                {
                    LoadSprite(_legsSlot, ItemData.GetItem(player.GetLegsItem()).GetRecipeFemale().ToString().Replace("_Recipe", ""));
                }
                if (player.GetFeetItem() != 0)
                {
                    LoadSprite(_bootsSlot, ItemData.GetItem(player.GetFeetItem()).GetRecipeFemale().ToString().Replace("_Recipe", ""));
                }
                break;
        }
    }

    void LoadSprite(Image icon, string _iconName)
    {
        Texture2D newSprite = Resources.Load<Texture2D>("ItemIcons/" + _iconName);

        if (newSprite)
        {
            icon.sprite = Sprite.Create(newSprite, new Rect(0, 0, newSprite.width, newSprite.height), new Vector2());
        }
        else
        {
            Debug.Log("Sprite `" + _iconName + "` not found.");
        }
    }
}
