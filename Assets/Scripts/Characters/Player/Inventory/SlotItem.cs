using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject buttonPrefab;

    public void Init(GameObject sourceButtonPrefab)
    {
        buttonPrefab = sourceButtonPrefab;
    }
}
