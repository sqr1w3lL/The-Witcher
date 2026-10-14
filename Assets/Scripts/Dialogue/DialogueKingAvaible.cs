using UnityEngine;

public class DialogueKingAvaible : MonoBehaviour
{
    [SerializeField] private TaskCheckerSO taskCheckerSO;
    [SerializeField] private DialogueAnimator dialogueAnimator;

    private void Update()
    {
        AvaibleToSpeakWithKing();
    }

    private void AvaibleToSpeakWithKing()
    {
        if (taskCheckerSO.tasksCompletedCount == 6)
            dialogueAnimator.canTalk = true;
        else
            dialogueAnimator.canTalk = false;
    }
}
