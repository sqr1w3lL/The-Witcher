using UnityEngine;

public class ActivatorQuest : MonoBehaviour
{
    [SerializeField] private GameObject questUI;
    [SerializeField] private QuestValue questValue; 

    public bool questStarted;
    
    private void Awake()
    {
        ActivateQuest(questValue.questIsActive);
    }

    public void ActivateQuest(bool isActive)
    {
        Debug.Log(isActive);
        questUI.SetActive(isActive);
        questStarted = isActive;
    }

}
