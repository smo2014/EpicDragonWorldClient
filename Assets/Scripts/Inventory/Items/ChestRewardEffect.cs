using UnityEngine;

[CreateAssetMenu(menuName = "Items/Effects/Chest Reward")]
public class ChestRewardEffect : UsableItemEffect
{


    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        ItemDatabase db = Inventory.Instance.itemDatabase;
        Item item = db.GetItemId(Random.Range(1, db.Count()));

        character.Inventory.AddItem(item);

/*                if (Random.Range(0,5) == 3)
                {
                    item = db.GetItemId(parentItem.itemId);
                    character.Inventory.AddItem(item);
                    Debug.Log("Reward: " + parentItem.ItemName);
                    Debug.Log("Second Item is: " + Random.Range(1, db.Count()));
                }
*/        
        character.dialogWindow.RewardWindow(400, 400, item.itemId);
    }

    public override string GetDescription()
    {
        return "Testing Chest contains diferent parts of armor.";
    }
}
