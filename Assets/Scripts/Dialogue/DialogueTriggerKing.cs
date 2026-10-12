using UnityEngine;

public class DialogueTriggerKing : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    public DialogueManagerKing dialogueManagerKing;

    public void TriggerDialogue()
    {
        dialogueManagerKing.StartDialogue(_dialogue);
    }
}
