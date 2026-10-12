using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("IsDie");
    private static readonly int Running = Animator.StringToHash("IsRunning");

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private FlashBlink _flashBlink;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Update()
    {
        _animator.SetBool(Running, Player.Instance.IsRunning());
        if(Player.Instance.IsAlive())
            AdjustPlayerFacingDirection();
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(Die, true);
        _flashBlink.StopBlinking();
    }

    private void AdjustPlayerFacingDirection ()
    {
        Vector3 mousePos = PlayerInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        _spriteRenderer.flipX = mousePos.x < playerPosition.x;

    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }
}
