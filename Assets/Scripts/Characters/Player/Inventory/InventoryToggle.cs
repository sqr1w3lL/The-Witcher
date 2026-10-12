using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryIn;

    private void Start()
    {
        inventoryIn.SetActive(false);
    }

    public void ToggleInventory()
    {
        inventoryIn.SetActive(!inventoryIn.activeSelf);
    }
}