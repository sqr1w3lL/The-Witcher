using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class CapybaraVisual : MonoBehaviour
{
    private const string TakeHit = "TakeHit";
    private static readonly int Hit = Animator.StringToHash(TakeHit);
    private const string IsDie = "IsDie";
    private static readonly int Die = Animator.StringToHash(IsDie);
    private const string IsSleeping = "IsSleeping";
    private static readonly int Sleep = Animator.StringToHash(IsSleeping);
    private const string IsRunning = "IsRunning";
    private static readonly int Run = Animator.StringToHash(IsRunning);
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private AnimalAI _animalAI;
    [SerializeField] private GameObject _animalShadow;
    [SerializeField] private AnimalEntity _animalEntity;
    private Animator _animator;
    private FlashBlink _flashBlink;
    SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        _animalEntity.OnAnimalTakeHit += animalEntity_OnTakeHit;
        _animalEntity.OnAnimalDeath += animalEntity_OnDeath;
        _animalEntity.OnAnimalSleep += animalEntity_OnSleep;
    }

    private void animalEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animalAI.onAttack = true;
        _animator.SetTrigger(Hit);
        _animator.SetBool(Run, true);
    }

    private void animalEntity_OnDeath(object sender, System.EventArgs e)
    {
        _flashBlink.StopBlinking();
        _animator.SetBool(Die, true);
        _spriteRenderer.sortingOrder = -1;
        _animalShadow.SetActive(false);
    }

    private void animalEntity_OnSleep(object sender, System.EventArgs e)
    {
        _animator.SetBool(Sleep, true);
    }

    public void StopSleep()
    {
        _animator.SetBool(Sleep, false);
    }

    private void Update()
    {
        _animator.SetBool(Run, _animalAI.IsRunning);
        _animator.SetFloat(Speed, _animalAI.GetRoamingAnimationSpeed());
    }

    private void OnDestroy()
    {
        _animalEntity.OnAnimalTakeHit -= animalEntity_OnTakeHit;
        _animalEntity.OnAnimalDeath -= animalEntity_OnDeath;
        _animalEntity.OnAnimalSleep -= animalEntity_OnSleep;
    }

}
