using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseGameMenu;
    public bool pauseGame;

    public void PauseGame()
    {
        if (pauseGame)
            Resume();
        else
            Pause();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_MainMenu");
    }

    private void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    private void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseGame = true;
    }
}
