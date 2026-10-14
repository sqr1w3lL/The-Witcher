using UnityEngine; 
using System;

public class PickUp : MonoBehaviour 
{ 
    [SerializeField] private GameObject slotButton;
    [SerializeField] private GameObject itemPrefab;
    private TaskManagement _taskManagement;
    private Inventory inventory;
    private bool isSubscribed = false;
    private bool picked = false;

    private void Awake()
    {
        _taskManagement = FindAnyObjectByType<TaskManagement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (inventory == null)
            inventory = other.GetComponent<Inventory>();

        if (PlayerInput.Instance != null && !isSubscribed)
        {
            PlayerInput.Instance.OnPlayerPickItem += OnPickItem;
            isSubscribed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (PlayerInput.Instance != null && isSubscribed)
        {
            PlayerInput.Instance.OnPlayerPickItem -= OnPickItem;
            isSubscribed = false;
        }
    }

    private void OnPickItem(object sender, EventArgs e)
    {
        if (picked || inventory == null || inventory.slots == null || inventory.isFull == null)
            return;

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (i < inventory.isFull.Length && !inventory.isFull[i])
            {
                inventory.isFull[i] = true;

                GameObject newSlotButton = Instantiate(slotButton, inventory.slots[i].transform);
                SlotItem slotItem = newSlotButton.GetComponent<SlotItem>();

                if (slotItem != null)
                {
                    slotItem.itemPrefab = itemPrefab;
                    slotItem.Init(slotButton);
                }
                else
                {
                    slotItem = newSlotButton.AddComponent<SlotItem>();
                    slotItem.itemPrefab = itemPrefab;
                    slotItem.Init(slotButton);
                }

                picked = true;

                string itemName = slotItem.itemPrefab.name;

                if (itemName.StartsWith("Flower"))
                    _taskManagement.FlowerIsPicked();
                else if (itemName.StartsWith("Mushroom"))
                    _taskManagement.MushroomIsPicked();

                PlayerInput.Instance.OnPlayerPickItem -= OnPickItem;
                isSubscribed = false;

                Destroy(gameObject);
                break;
            }
        }
    }
}