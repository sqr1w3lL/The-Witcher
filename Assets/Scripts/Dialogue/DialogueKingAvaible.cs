using UnityEngine;

public class DialogueKingAvaible : MonoBehaviour
{
    [SerializeField] private TaskCheckerSO taskCheckerSO;
    [SerializeField] private DialogueAnimatorKing dialogueAnimatorKing;

    private void Update()
    {
        AvaibleToSpeakWithKing();
    }

    private void AvaibleToSpeakWithKing()
    {
        if (taskCheckerSO.tasksCompletedCount == 6)
            dialogueAnimatorKing.canTalk = true;
        else
            dialogueAnimatorKing.canTalk = false;
    }
}
