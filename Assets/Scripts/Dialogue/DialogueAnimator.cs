using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    private static readonly int StartOpenHash = Animator.StringToHash("startOpen");

    [SerializeField] private Animator startAnim;
    [SerializeField] private MonoBehaviour dialogueManager;

    public bool canTalk = true;

    private IDialogueManager _dialogueManager;

    private void Awake()
    {
        _dialogueManager = dialogueManager as IDialogueManager;
    }

    public void DisableTalk()
    {
        canTalk = false;
        startAnim.SetBool(StartOpenHash, false);

        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTalk) return;

        if (collision.transform.TryGetComponent(out Player _))
            startAnim.SetBool(StartOpenHash, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player _))
        {
            startAnim.SetBool(StartOpenHash, false);
            _dialogueManager?.EndDialogue();
        }
    }
}
