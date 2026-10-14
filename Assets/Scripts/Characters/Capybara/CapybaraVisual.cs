using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class CapybaraVisual : MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("TakeHit");
    private static readonly int Die = Animator.StringToHash("IsDie");
    private static readonly int Sleep = Animator.StringToHash("IsSleeping");
    private static readonly int Run = Animator.StringToHash("IsRunning");
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private AnimalAI animalAI;
    [SerializeField] private GameObject animalShadow;
    [SerializeField] private AnimalEntity animalEntity;
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
        animalEntity.OnAnimalTakeHit += animalEntity_OnTakeHit;
        animalEntity.OnAnimalDeath += animalEntity_OnDeath;
        animalEntity.OnAnimalSleep += animalEntity_OnSleep;
    }

    private void Update()
    {
        _animator.SetBool(Run, animalAI.IsRunning);
        _animator.SetFloat(Speed, animalAI.GetRoamingAnimationSpeed());
    }

    public void StopSleep()
    {
        _animator.SetBool(Sleep, false);
    }

    private void animalEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        animalAI.onAttack = true;
        _animator.SetTrigger(Hit);
        _animator.SetBool(Run, true);
    }

    private void animalEntity_OnDeath(object sender, System.EventArgs e)
    {
        _flashBlink.StopBlinking();
        _animator.SetBool(Die, true);
        _spriteRenderer.sortingOrder = -1;
        animalShadow.SetActive(false);
    }

    private void animalEntity_OnSleep(object sender, System.EventArgs e)
    {
        _animator.SetBool(Sleep, true);
    }

    private void OnDestroy()
    {
        animalEntity.OnAnimalTakeHit -= animalEntity_OnTakeHit;
        animalEntity.OnAnimalDeath -= animalEntity_OnDeath;
        animalEntity.OnAnimalSleep -= animalEntity_OnSleep;
    }

}
