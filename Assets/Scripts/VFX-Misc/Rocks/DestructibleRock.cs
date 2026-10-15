using System;
using UnityEngine;

public class DestructibleRock : MonoBehaviour
{
    public event EventHandler OnDestructibleTakeDamage;
    private BushDestroyed _bushDestroyed;

    private void Awake()
    {
        _bushDestroyed = GetComponent<BushDestroyed>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Sword>(out _)) return;

        OnDestructibleTakeDamage?.Invoke(this, EventArgs.Empty);

        if (TryGetComponent<BushDestroyed>(out var bush) ||
            (bush = GetComponentInParent<BushDestroyed>()) != null)
            bush.BushDead();

        NavMeshSurfaceManagement.Instance?.RebakeNavMeshSurface();

        Destroy(gameObject);
    }

}
