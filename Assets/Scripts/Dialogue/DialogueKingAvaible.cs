using UnityEngine;

public class DialogueKingAvaible : MonoBehaviour
{
    [SerializeField] private TaskCheckerSO _taskCheckerSO;
    [SerializeField] private DialogueAnimatorKing _dialogueAnimatorKing;

    private void Update()
    {
        AvaibleToSpeakWithKing();
    }

    private void AvaibleToSpeakWithKing()
    {
        if (_taskCheckerSO.tasksCompletedCount != 6)
            _dialogueAnimatorKing.canTalk = false;
        else if (_taskCheckerSO.tasksCompletedCount == 6)
            _dialogueAnimatorKing.canTalk = true;
    }
}
