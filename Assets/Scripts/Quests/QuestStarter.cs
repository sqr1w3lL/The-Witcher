using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    [SerializeField] private QuestValue questValue;
    [SerializeField] private TMPro.TMP_Text questName;
    [SerializeField] private TMPro.TMP_Text questDescription;
    [SerializeField] private TMPro.TMP_Text questTask;

    private void Awake()
    {
        questName.text = questValue.questName;
        questDescription.text = questValue.questDescription;
        questTask.text = questValue.questTask;
    }
}
