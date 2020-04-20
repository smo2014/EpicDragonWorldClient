using System.Collections;
using UnityEngine;

public class Inventory : ItemContainer
{
    public static Inventory Instance { get; private set; }

    public ItemDatabase itemDatabase;
    [SerializeField] Transform itemsParent;

    protected override void OnValidate()
    {
        if(itemsParent != null)
            itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);

        if (!Application.isPlaying)
        {
        }

    }

    protected override void Awake()
    {
        base.Awake();

        itemDatabase = Resources.Load<ItemDatabase>("Item Database");

        if (itemDatabase == null)
            Debug.Log("I dont find Item Database");
    }

    private void Start()
    {
        Instance = this;
    }

    }
