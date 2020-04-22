using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    [Space]
    public int itemId; // server Item Id

    public string ItemName;
    public Sprite Icon; // TODO Change Icon by sex
    [Range(1,999)]
    public int MaximumStacks = 1;
    protected static readonly StringBuilder sb = new StringBuilder();

    #if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
    #endif


    public virtual Item GetCopy()
    {
        return this;
    }
    
    public virtual void Destroy()
    {

    }
    public virtual string GetItemType()
    {
        return "";
    }

    public virtual string GetDescription()
    {
        return "";
    }

    public virtual int GetEnchant()
    {
        return 0;
    }
}
