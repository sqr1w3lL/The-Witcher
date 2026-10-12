using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlayerSlotsSO inventoryStorage;
    public GameObject[] slots;
    public bool[] isFull;

    private void Awake()
    {
        if (slots == null)
            slots = new GameObject[0];

        if (isFull == null || isFull.Length != slots.Length)
            isFull = new bool[slots.Length];
    }

    private void Start()
    {
        RestoreFromStorage();
    }

    public void SaveToStorage()
    {
        GameObject[] items = new GameObject[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;

            SlotItem item = slots[i].GetComponentInChildren<SlotItem>();
            if (item != null)
                items[i] = item.buttonPrefab;
        }

        inventoryStorage.Save(items);
    }

    private void RestoreFromStorage()
    {
        if (inventoryStorage == null || !inventoryStorage.HasData)
            return;

        for (int i = 0; i < slots.Length; i++)
        {
            GameObject buttonPrefab = inventoryStorage.GetItem(i);
            if (buttonPrefab == null || slots[i] == null) continue;

            GameObject button = Instantiate(buttonPrefab, slots[i].transform);

            SlotItem slotItem = button.GetComponent<SlotItem>();
            if (slotItem == null)
                slotItem = button.AddComponent<SlotItem>();
            slotItem.Init(buttonPrefab);

            isFull[i] = true;
        }
    }
}
