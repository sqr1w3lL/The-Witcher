using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventorySlotsSO")]
public class PlayerSlotsSO : ScriptableObject
{
    [NonSerialized] private GameObject[] _itemButtonPrefabs;

    public bool HasData => _itemButtonPrefabs != null;

    public void Save(GameObject[] itemButtonPrefabs)
    {
        _itemButtonPrefabs = itemButtonPrefabs != null ? (GameObject[])itemButtonPrefabs.Clone() : null;
    }

    public GameObject GetItem(int slotIndex)
    {
        if (_itemButtonPrefabs == null || slotIndex < 0 || slotIndex >= _itemButtonPrefabs.Length)
            return null;
        return _itemButtonPrefabs[slotIndex];
    }

    public void Clear()
    {
        _itemButtonPrefabs = null;
    }
}
