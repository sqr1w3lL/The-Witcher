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
        if (collision.gameObject.GetComponent<Sword>())
        {
            OnDestructibleTakeDamage?.Invoke(this, EventArgs.Empty);

            if (NavMeshSurfaceManagement.Instance != null)
            {
                NavMeshSurfaceManagement.Instance.RebakeNavMeshSurface();
            }

            BushDestroyed bush = GetComponentInParent<BushDestroyed>();
            if (bush != null)
                _bushDestroyed.BushDead();

            Destroy(gameObject);
        }
    }


}
