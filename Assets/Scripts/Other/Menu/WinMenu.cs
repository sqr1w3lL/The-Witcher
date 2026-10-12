using UnityEngine;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject _winGameMenu;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = FindAnyObjectByType<PlayerInput>();
    }

    public void WinGame()
    {
        Pause();
        _playerInput.WinGame();
    }

    private void Pause()
    {
        _winGameMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
