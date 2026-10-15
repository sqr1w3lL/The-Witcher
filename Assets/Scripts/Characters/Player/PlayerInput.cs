using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerDash;
    public event EventHandler OnPlayerPause;
    public event EventHandler OnPlayerPickItem;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _playerInputActions.Combat.Attack.started += PlayerAttack_started;
        _playerInputActions.Player.Dash.performed += PlayerDashPerformed;
        _playerInputActions.Player.Pause.performed += PlayerPausePerformed;
        _playerInputActions.Player.PickItem.performed += PlayerPickItemPerformed;
    }

    private void PlayerPickItemPerformed(InputAction.CallbackContext obj) 
    {
        OnPlayerPickItem?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerDashPerformed(InputAction.CallbackContext obj)
    {
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }

    public void DisableMovement()
    {
        _playerInputActions.Disable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector2 GetMousePosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    public void WinGame()
    {
        _playerInputActions.Disable();
        UnsubscribeActions();
    }

    private void PlayerPausePerformed(InputAction.CallbackContext obj)
    {
        OnPlayerPause?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    private void UnsubscribeActions()
    {
        if (_playerInputActions == null) return;

        _playerInputActions.Combat.Attack.started -= PlayerAttack_started;
        _playerInputActions.Player.Dash.performed -= PlayerDashPerformed;
        _playerInputActions.Player.Pause.performed -= PlayerPausePerformed;
        _playerInputActions.Player.PickItem.performed -= PlayerPickItemPerformed;
    }

    private void OnDestroy()
    {
        UnsubscribeActions();

        if (_playerInputActions != null)
        {
            _playerInputActions.Disable();
            _playerInputActions.Dispose();
            _playerInputActions = null;
        }

        if (Instance == this)
            Instance = null;
    }

}
