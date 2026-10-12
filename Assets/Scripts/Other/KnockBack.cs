using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackForce = 3f;
    [SerializeField] private float knockBackMovingTimerMax = 0.3f;

    private Rigidbody2D _rigidBody2D;
    private float _knockBackMovingTimer;
    public bool IsGettingKnockedBack { get; private set; }

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _knockBackMovingTimer -= Time.deltaTime;
        if (_knockBackMovingTimer < 0)
            StopKnockBackMovement();
    }

    public void StopKnockBackMovement()
    {
        _rigidBody2D.linearVelocity = Vector3.zero;
        IsGettingKnockedBack = false;
    }

    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingKnockedBack = true;
        _knockBackMovingTimer = knockBackMovingTimerMax;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackForce / _rigidBody2D.mass;
        _rigidBody2D.AddForce(difference, ForceMode2D.Impulse);
    }
}
