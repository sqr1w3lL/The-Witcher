using System;
using UnityEngine;

public class DialogueManager : DialogueManagerBase
{
    [SerializeField] private ActivatorQuest activatorQuest;
    [SerializeField] private QuestBlocker questBlocker;
    [SerializeField] private QuestChecker questChecker;
    [SerializeField] private DialogueAnimator dialogueAnimator;
    [SerializeField] private TaskCheckerSO taskCheckerSO;

    public event EventHandler questIsStarted;

    private bool _endQuest;
    private bool _questStarted;

    protected override void PrepareDialogue(Dialogue dialogue)
    {
        if (!activatorQuest.questStarted) return;

        dialogue.sentences = questChecker.questIsCompleted
            ? questBlocker.finalSentenceQuest
            : questBlocker.sentenceQuest;
    }

    protected override void OnSentencesFinished()
    {
        bool shouldStartQuest = !activatorQuest.questStarted && !_endQuest && !_questStarted;
        EndDialogue();

        if (shouldStartQuest)
        {
            questIsStarted?.Invoke(this, EventArgs.Empty);
            _questStarted = true;
        }
    }

    protected override void OnSentenceShown()
    {
        if (!_endQuest)
            _questStarted = false;

        if (questChecker.questIsCompleted)
            EndQuest();
    }

    public override void EndDialogue()
    {
        base.EndDialogue();

        if (_endQuest)
        {
            questChecker.enabled = false;
            activatorQuest.ActivateQuest(false);
            dialogueAnimator.DisableTalk();
            _endQuest = false;
        }
    }

    public void EndQuest()
    {
        Debug.Log("Quest ended");
        taskCheckerSO.questCompleted = false;
        questChecker.questIsCompleted = false;
        _endQuest = true;
    }
}