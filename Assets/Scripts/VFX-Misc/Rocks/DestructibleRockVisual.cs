using UnityEngine;

public class DestructibleRockVisual : MonoBehaviour
{
    [SerializeField] private GameObject rockDeathVFXPrefab;
    private DestructibleRock _destructibleRock;

    private void Awake()
    {
        _destructibleRock = GetComponentInParent<DestructibleRock>();
    }

    private void Start()
    {
        _destructibleRock.OnDestructibleTakeDamage += DestructibleRockOnDestructibleTakeDamage;
    }

    private void DestructibleRockOnDestructibleTakeDamage(object sender, System.EventArgs e)
    {
        ShowDeathVFX();
    }

    private void ShowDeathVFX()
    {
        Instantiate(rockDeathVFXPrefab, _destructibleRock.transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        _destructibleRock.OnDestructibleTakeDamage -= DestructibleRockOnDestructibleTakeDamage;
    }

}
