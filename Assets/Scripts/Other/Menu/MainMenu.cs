using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerSlotsSO inventoryStorage;
    [SerializeField] private VectorValue playerStorage;
    [SerializeField] private HealthValue healthSO;
    [SerializeField] private QuestValue questSO;
    [SerializeField] private TaskCheckerSO taskCheckerSO;

    private void Awake()
    {
        healthSO.currentHealth = 20;
        healthSO.percentageHealth = 1;

        questSO.questIsActive = false;

        taskCheckerSO.flowerCount = 0;
        taskCheckerSO.mushroomCount = 0;
        taskCheckerSO.bushDeathCount = 0;
        taskCheckerSO.skeletonCount = 0;
        taskCheckerSO.capybaraCount = 0;
        taskCheckerSO.sisterIsFoundCount = 0;
        taskCheckerSO.questCompleted = false;
        taskCheckerSO.tasksCompletedCount = 0;
    }

    public void PlayGame()
    {
        if (inventoryStorage != null)
            inventoryStorage.Clear();

        playerStorage.initialValue.Set(-18, -8, 0);

        SceneManager.LoadScene("FirstLevel");
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
