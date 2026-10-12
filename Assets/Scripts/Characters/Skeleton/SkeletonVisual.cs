using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SkeletonVisual : MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("TakeHit");
    private static readonly int Die = Animator.StringToHash("IsDie");
    private static readonly int Running = Animator.StringToHash("IsRunning");
    private static readonly int SpeedMultiplier = Animator.StringToHash("ChasingSpeedMultiplier");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private EnemyAI _enemyAI;
    private EnemyEntity _enemyEntity;
    private GameObject _enemyShadow;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _enemyAI = GetComponentInParent<EnemyAI>();
        _enemyEntity = GetComponentInParent<EnemyEntity>();
        _enemyShadow = transform.Find("SkeletonShadow").gameObject;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += _enemyEntity_OnDeath;
    }

    private void Update()
    {
        _animator.SetBool(Running, _enemyAI.IsRunning);
        _animator.SetFloat(SpeedMultiplier, _enemyAI.GetRoamingAnimationSpeed());
    }

    public void TriggerAttackAnimationTurnOff()
    {
        _enemyEntity.PolygonColliderTurnOff();
    }
    public void TriggerAttackAnimationTurnOn()
    {
        _enemyEntity.PolygonColliderTurnOn();
    }

    private void _enemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(Hit);
    }

    private void _enemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(Die, true);
        _spriteRenderer.sortingOrder = -1;
        _enemyShadow.SetActive(false);
    }

    private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(AttackHash);
    }

    private void OnDestroy()
    {
        _enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit -= _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath -= _enemyEntity_OnDeath;
    }

}
