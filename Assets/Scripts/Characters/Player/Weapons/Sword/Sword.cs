using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damageAmount = 2;
    private PolygonCollider2D _polygonCollider2D;
    public event EventHandler OnSwordSwing;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        AttackColliderTurnOff();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
            return;
        }

        if (collision.transform.TryGetComponent(out AnimalEntity animalEntity))
            animalEntity.TakeDamage(transform, damageAmount);
    }

    public void Attack()
    {
        AttackColliderTurnOffOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    public void AttackColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    private void AttackColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }

    private void AttackColliderTurnOffOn() {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }
}
