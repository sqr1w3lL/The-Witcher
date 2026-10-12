using System;
using UnityEngine;
using UnityEngine.AI;
using static Utils.Utils;

public class AnimalAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private CapybaraVisual capybaraVisual;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private float runningTimerMax = 2f;
    [SerializeField] private float escapingSpeedMultiplier = 2f;
    [SerializeField] private float sleepingTimerMax = 5f;
    [SerializeField] private float movementTimerMax = 10f;
    [SerializeField] private float escapeDistance = 4f;
    [SerializeField] private bool isEscapingAnimal = true;

    private NavMeshAgent _navMeshAgent;
    private AnimalEntity _animalEntity;
    public event EventHandler OnAnimalFlashBlink;

    private readonly float _checkDirectionDuration = 0.1f;
    private float _nextCheckDirectionTime = 0f;
    private float _roamingTimer;
    private float _movementTimer;
    private float _sleepingTimer;
    private float _runningTimer;
    private float _roamingSpeed;
    private float _runningSpeed;
    private State _currentState;
    public bool onAttack;

    private Vector3 _lastPosition;
    private Vector3 _roamPosition;
    private Vector3 _startingPosition;

    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;

    private enum State
    {
        Idle,
        Roaming,
        Running,
        Sleeping,
        Death
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = startingState;

        _animalEntity = GetComponent<AnimalEntity>();

        _roamingSpeed = _navMeshAgent.speed;
        _runningSpeed = _navMeshAgent.speed * escapingSpeedMultiplier;
        _sleepingTimer = sleepingTimerMax;
        _movementTimer = movementTimerMax; 
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
    }
    
    public float GetRoamingAnimationSpeed()
    {
        return _navMeshAgent.speed / _roamingSpeed;
    }

    public void SetDeathState()
    {
        _navMeshAgent.ResetPath();
        _navMeshAgent.isStopped = true;
        _currentState = State.Death;
    }

    public void isFlashBlink()
    {
        OnAnimalFlashBlink?.Invoke(this, EventArgs.Empty);
    }

    private void StateHandler()
    {
        switch (_currentState)
        {
            case State.Roaming:
                _roamingTimer -= Time.deltaTime;
                if (_roamingTimer < 0)
                {
                    Roaming();
                    _roamingTimer = roamingTimerMax;
                }
                CheckCurrentState();
                break;
            case State.Running:
                _runningTimer -= Time.deltaTime;
                if (_runningTimer < 0)
                {
                    Roaming();
                    onAttack = false;
                    _runningTimer = runningTimerMax;
                }
                CheckCurrentState();
                break;
            case State.Sleeping:
                _sleepingTimer -= Time.deltaTime;
                if (_sleepingTimer <= 0f)
                {
                    capybaraVisual.StopSleep();
                    _sleepingTimer = sleepingTimerMax;
                    _currentState = State.Roaming;
                    _navMeshAgent.speed = _roamingSpeed;
                    _roamingTimer = 0f;
                }
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
        }
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Roaming;

        if (!onAttack)
        {
            _movementTimer -= Time.deltaTime;
            if (_movementTimer < 0)
            {
                _navMeshAgent.ResetPath();
                newState = State.Sleeping;
                _movementTimer = movementTimerMax;
            }
        }

        if (isEscapingAnimal && distanceToPlayer <= escapeDistance && onAttack)
            newState = State.Running;

        if (newState != _currentState)
        {
            if (newState == State.Running)
            {
                _navMeshAgent.speed = _runningSpeed;
                _runningTimer = runningTimerMax;
            }
            else if (newState == State.Sleeping)
            {
                _sleepingTimer = sleepingTimerMax;
                _animalEntity?.Sleeping();
            }
            else if (newState == State.Roaming)
            {
                _navMeshAgent.speed = _roamingSpeed;
                _roamingTimer = 0f;
            }
            
            _currentState = newState;
        }

    }

    private void MovementDirectionHandler ()
    {
        if (Time.time > _nextCheckDirectionTime)
        {
            if (IsRunning)
                ChangeFacingDirection(_lastPosition, transform.position);
            else if (_currentState == State.Running)
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);

            _lastPosition = transform.position;
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }

    }

    private void Roaming()
    {
        _startingPosition = transform.position;
        _roamPosition = GetRoamingPosition();
        _navMeshAgent.SetDestination(_roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return _startingPosition + GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        transform.rotation = sourcePosition.x > targetPosition.x ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
    }
}