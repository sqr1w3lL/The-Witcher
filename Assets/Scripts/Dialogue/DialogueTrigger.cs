using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    [SerializeField] private Dialogue dialogue;

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
