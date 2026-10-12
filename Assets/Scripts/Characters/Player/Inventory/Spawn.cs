using UnityEngine;

public class Spawn : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem(GameObject itemPrefab)
    {
        if (itemPrefab == null)
            return;

        if (_player == null)
            return;

        Vector2 playerPos = new Vector2(_player.position.x + 2, _player.position.y - 1);
        Instantiate(itemPrefab, playerPos, Quaternion.identity);
    }
}
