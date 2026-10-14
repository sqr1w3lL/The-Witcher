using System;
using UnityEngine;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private ActivatorQuest activatorQuest;
    [SerializeField] private TaskCheckerSO taskCheckerSO;
    [SerializeField] private Quest quest;

    private bool _questIsActive = true;
    public bool questIsCompleted;

    private void Awake()
    {
        var trigger = GetComponent<DialogueTrigger>();
        dialogueManager = trigger.dialogueManager as DialogueManager;
        OnEnable();
    }

    private void Update()
    {
        if (taskCheckerSO.questCompleted)
            questIsCompleted = true;
        else if (!taskCheckerSO.questCompleted)
            questIsCompleted = false;
    }

    private void OnQuestStarted(object sender, EventArgs e)
    {
        if (!questIsCompleted)
        {
            activatorQuest.ActivateQuest(_questIsActive);

            questManager.StartQuest(this.quest);
        }
    }

    private void OnEnable()
    {
        dialogueManager.questIsStarted += OnQuestStarted;
    }

    private void OnDisable()
    {
        dialogueManager.questIsStarted -= OnQuestStarted;
    }
}
