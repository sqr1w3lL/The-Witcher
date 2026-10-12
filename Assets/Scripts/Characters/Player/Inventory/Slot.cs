using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private TaskManagement taskManagement;
    public int i;


    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _inventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (_inventory == null || i >= _inventory.isFull.Length)
            return;

        if (transform.childCount <= 0)
            _inventory.isFull[i] = false;
    }

    public void DropItem()
    {
        for (int c = transform.childCount - 1; c >= 0; c--)
        {
            Transform child = transform.GetChild(c);

            SlotItem slotItem = child.GetComponent<SlotItem>();
            Spawn spawn = child.GetComponent<Spawn>();

            if (slotItem != null && slotItem.itemPrefab != null)
            {
                if (spawn != null)
                    spawn.SpawnDroppedItem(slotItem.itemPrefab);

                string itemName = slotItem.itemPrefab.name;

                if (itemName.StartsWith("Flower"))
                    taskManagement.FlowerIsDropped();
                else if (itemName.StartsWith("Mushroom"))
                    taskManagement.MushroomIsDropped();
            }

            Destroy(child.gameObject);
        }
    }
}
