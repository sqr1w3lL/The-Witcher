using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public MonoBehaviour dialogueManager;
    [SerializeField] private Dialogue dialogue;

    private IStartDialogue _dialogueManager;

    private void Awake()
    {
        _dialogueManager = dialogueManager as IStartDialogue;
    }

    public void TriggerDialogue()
    {
        _dialogueManager?.StartDialogue(dialogue);
    }
}
