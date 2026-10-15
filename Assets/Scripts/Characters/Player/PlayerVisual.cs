using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("IsDie");
    private static readonly int Running = Animator.StringToHash("IsRunning");

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private FlashBlink _flashBlink;
    private Player _player;
    private PlayerInput _input;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        _player = Player.Instance;
        _input = PlayerInput.Instance;

        _player.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Update()
    {
        if (_player == null) return;

        _animator.SetBool(Running, _player.IsRunning());

        if (_player.IsAlive())
            AdjustPlayerFacingDirection();
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(Die, true);
        _flashBlink.StopBlinking();
    }

    private void AdjustPlayerFacingDirection()
    {
        if (_input == null) return;

        Vector3 mousePos = _input.GetMousePosition();
        Vector3 playerPosition = _player.GetPlayerScreenPosition();

        _spriteRenderer.flipX = mousePos.x < playerPosition.x;
    }

    private void OnDestroy()
    {
        if (_player != null)
            _player.OnPlayerDeath -= Player_OnPlayerDeath;
    }
}