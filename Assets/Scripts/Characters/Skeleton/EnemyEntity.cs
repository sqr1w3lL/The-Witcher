using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour, IHealthSource
{
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float deathDestroyDelay = 2f;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    public event Action<float> HealthChanged;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;

    private int _currentHealth;
    private bool _isDead;
    private bool _hasShownHealthBar;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
        enemySO.enemyHealth = maxHealth;
    }

    private void Start()
    {
        _currentHealth = enemySO.enemyHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!_hasShownHealthBar)
        {
            healthBar?.Show();
            _hasShownHealthBar = true;
        }       

        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
        float _currentHealthAsPercentage = (float)_currentHealth / maxHealth;
        HealthChanged?.Invoke(_currentHealthAsPercentage);
    }

    public void PolygonColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            player.TakeDamage(transform, enemySO.enemyDamageAmount);
    }
    
    private void DetectDeath()
    {
        if (_currentHealth <= 0 && !_isDead)
        {
            _isDead = true;

            HealthChanged?.Invoke(0);
            _boxCollider2D.enabled = false;
            _enemyAI.SetDeathState();
            _polygonCollider2D.enabled = false;
            healthBar?.HealthBarOnDeath();
            OnDeath?.Invoke(this, EventArgs.Empty);

            Destroy(gameObject, deathDestroyDelay);
        }
    }
}
