using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    private static readonly int StartOpenHash = Animator.StringToHash("startOpen");

    [SerializeField] private Animator startAnim;
    [SerializeField] private MonoBehaviour dialogueManager;
    [SerializeField] private NpcTalkStateSO talkState;
    [SerializeField] private string npcId;

    public bool canTalk = true;

    public string NpcId => npcId;

    private IDialogueManager _dialogueManager;

    private void Awake()
    {
        _dialogueManager = dialogueManager as IDialogueManager;
    }

    private void Start()
    {
        if (talkState != null && talkState.IsDisabled(npcId))
            ApplyDisabled();
    }

    public void DisableTalk()
    {
        talkState?.Disable(npcId);
        ApplyDisabled();
    }

    private void ApplyDisabled()
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
