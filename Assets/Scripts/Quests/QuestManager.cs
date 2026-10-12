using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _questName;
    [SerializeField] private TMPro.TMP_Text _questDescription;
    [SerializeField] private TMPro.TMP_Text _questTask;

    public void StartQuest(Quest quest)
    {
        _questName.text = quest.name;
        _questDescription.text = quest.description;
        _questTask.text = quest.task;
    }

}
