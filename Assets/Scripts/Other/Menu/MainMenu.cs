using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerSlotsSO inventoryStorage;
    [SerializeField] private VectorValue playerStorage;

    public void PlayGame()
    {
        if (inventoryStorage != null)
            inventoryStorage.Clear();

        playerStorage.initialValue.Set(-18, -8, 0);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
