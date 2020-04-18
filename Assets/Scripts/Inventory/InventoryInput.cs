using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject craftPanel;
    [SerializeField] KeyCode[] toggleInventoryKeys;

    private void Start()
    {
        inventoryPanel.SetActive(false);
        craftPanel.SetActive(false);
    }

    void Update()
    {
        for(int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);
                break;
            }
        }
    }

    public void ToggleCraftPanel()
    {
        craftPanel.SetActive(!craftPanel.activeSelf);
    }
}
