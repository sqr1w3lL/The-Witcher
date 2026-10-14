using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private GameObject questUI;
    [SerializeField] private GameObject questName;
    [SerializeField] private GameObject questDescription;
    [SerializeField] private GameObject questTask;

    [SerializeField] private VectorValue playerStorage;
    [SerializeField] private HealthValue healthStorage;
    [SerializeField] private QuestValue questStorage;

    [SerializeField] private HealthBarHero hpBarHero;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Player player;
    [SerializeField] private NextLevel nextLevel;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeToLevel(NextLevel nextLevel)
    {
        this.nextLevel = nextLevel;
        _animator.SetTrigger("fade");
    }

    public void OnFadeComplete()
    {
        playerStorage.initialValue = nextLevel.position;
        
        healthStorage.percentageHealth = hpBarHero.currentHealthAsPercentage;

        if (inventory != null)
            inventory.SaveToStorage();

        if (questUI.activeInHierarchy) {
            questStorage.questIsActive = true;
            questStorage.questName = questName.GetComponent<TMPro.TMP_Text>().text;
            questStorage.questDescription = questDescription.GetComponent<TMPro.TMP_Text>().text;
            questStorage.questTask = questTask.GetComponent<TMPro.TMP_Text>().text;
        }

        SceneManager.LoadScene(nextLevel.GetSceneName());
    }
}
