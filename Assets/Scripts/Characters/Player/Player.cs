using System;
using System.Collections;
using UnityEngine;

[SelectionBase]

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(KnockBack))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;
    public event Action<float> HeroHealthChanged;

    [Header("Main")]
    [Space(3)]
    [SerializeField] private float movingSpeed = 5f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float damageRecoveryTime = 0.5f;
    [SerializeField] private int dashSpeed = 4;

    [Header("Settings")]
    [Space(3)]
    [SerializeField] private DeathScreen deathScreen;
    [SerializeField] private VectorValue playerPosition;
    [SerializeField] private HealthValue playerHealthPoint;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private DashSound dashSound;

    private Rigidbody2D _rigidBody2D;
    private KnockBack _knockBack;
    private Camera _mainCamera;

    private readonly float _minMovingSpeed = 0.1f;
    private float _initialMovingSpeed;
    private float _dashTime = 0.2f;
    private float _dashCDTime = 0.25f;
    private bool _ifRunning;
    private bool _canTakeDamage;
    private bool _isAlive;
    private bool _isDashing;
    public bool isPickingItem;
    public int playerCurrentHealth;

    Vector2 _inputVector;

    private void Awake()
    {
        Instance = this;

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
        _mainCamera = Camera.main;
        
        _initialMovingSpeed = movingSpeed;
    }

    private void Start()
    {
        transform.position = playerPosition.initialValue;
        maxHealth = playerHealthPoint.currentHealth;
        playerCurrentHealth = Mathf.RoundToInt(playerHealthPoint.percentageHealth * maxHealth);

        _canTakeDamage = true;
        _isAlive = true;

        PlayerInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
        PlayerInput.Instance.OnPlayerDash += Player_OnPlayerDash;
        PlayerInput.Instance.OnPlayerPause += Player_OnPlayerPause;
        PlayerInput.Instance.OnPlayerPickItem += Player_OnPlayerPickItem;
    }

    private void Update()
    {
        _inputVector = PlayerInput.Instance.GetMovementVector();
    }

    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack)
            return;

        HandleMovement();
    }

    public bool IsAlive() => _isAlive;
    public bool IsRunning()
    {
        return _ifRunning;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    public void TakeDamage(Transform damageSource, int damage)
    {
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;

            playerCurrentHealth = Mathf.Max(0, playerCurrentHealth - damage);

            float _currentHealthAsPercentage = (float)playerCurrentHealth / maxHealth;
            playerHealthPoint.percentageHealth = _currentHealthAsPercentage;

            HeroHealthChanged?.Invoke(_currentHealthAsPercentage);

            _knockBack.GetKnockedBack(damageSource);

            OnFlashBlink?.Invoke(this, EventArgs.Empty);

            StartCoroutine(DamageRecoveryRoutine());

        }

        DetectDeath();
    }
    private void DetectDeath()
    {
        if (playerCurrentHealth == 0 && _isAlive)
        {
            _isAlive = false;
            _knockBack.StopKnockBackMovement();
            PlayerInput.Instance.DisableMovement();
            HeroHealthChanged?.Invoke(0);
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            deathScreen.SetDeathScreen();
        }
    }
    private void Dash()
    {
        if(!_isDashing)
            StartCoroutine(DashRoutine());
    }
    private void HandleMovement()
    {
        _rigidBody2D.MovePosition(_rigidBody2D.position + _inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(_inputVector.x) > _minMovingSpeed || Mathf.Abs(_inputVector.y) > _minMovingSpeed)
            _ifRunning = true;
        else
            _ifRunning = false;
    }

    private IEnumerator DashRoutine()
    {
        _isDashing = true;
        movingSpeed *= dashSpeed;
        trailRenderer.emitting = true;

        if (dashSound != null) dashSound.Play();

        yield return new WaitForSeconds(_dashTime);
        
        trailRenderer.emitting = false;
        movingSpeed = _initialMovingSpeed;
        
        yield return new WaitForSeconds(_dashCDTime);
        _isDashing = false;
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        _canTakeDamage = true;
    }

    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void Player_OnPlayerDash(object sender, System.EventArgs e)
    {
        Dash();
    }

    private void Player_OnPlayerPause(object sender, System.EventArgs e)
    {
        if (pauseMenu != null)
            pauseMenu.PauseGame();
    }

    private void Player_OnPlayerPickItem(object sender, System.EventArgs e)
    {
        isPickingItem = true;
    }

}


