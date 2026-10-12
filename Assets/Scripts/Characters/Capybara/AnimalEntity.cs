using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(AnimalAI))]
public class AnimalEntity : MonoBehaviour
{
    [SerializeField] private AnimalSO animalSO;
    [SerializeField] private HealthBarCapybara healthBar;
    [SerializeField] private float deathDestroyDelay = 2f;

    public event EventHandler OnAnimalTakeHit;
    public event EventHandler OnAnimalDeath;
    public event EventHandler OnAnimalSleep;
    public event Action<float> HealthChanged;

    private BoxCollider2D _boxCollider2D;
    private AnimalAI _animalAI;
    private KnockBack _knockBack;

    private int _maxHealth;
    private int _currentHealth;
    private bool _isDead;
    private bool _hasShownHealthBar;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animalAI = GetComponent<AnimalAI>();
        _knockBack = GetComponent<KnockBack>();
        _maxHealth = animalSO.animalHealth;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack)
            return;
    }

    public void TakeDamage(Transform damageSource, int damage)
    {
        if (!_hasShownHealthBar)
        {
            healthBar?.Show();
            _hasShownHealthBar = true;
        }

        _currentHealth -= damage;
        OnAnimalTakeHit?.Invoke(this, EventArgs.Empty);

        _knockBack.GetKnockedBack(damageSource);
        _animalAI.isFlashBlink();

        DetectDeath();
        float _currentHealthAsPercentage = (float)_currentHealth / _maxHealth;
        HealthChanged?.Invoke(_currentHealthAsPercentage);
    }


    public void Sleeping()
    {
        OnAnimalSleep?.Invoke(this, EventArgs.Empty);
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0 && !_isDead)
        {
            _isDead = true;

            HealthChanged?.Invoke(0);
            _boxCollider2D.enabled = false;
            _animalAI.SetDeathState();
            healthBar?.HealthBarOnDeath();
            _knockBack.StopKnockBackMovement();
            OnAnimalDeath?.Invoke(this, EventArgs.Empty);

            Destroy(gameObject, deathDestroyDelay);
        }
    }
}
