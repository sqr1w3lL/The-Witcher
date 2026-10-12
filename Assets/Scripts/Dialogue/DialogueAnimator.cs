using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    [SerializeField] private Animator startAnim;
    [SerializeField] private DialogueManager dialogueManager;

    private bool _canTalk = true;

    public void DisableTalk()
    {
        _canTalk = false;
        startAnim.SetBool("startOpen", false);

        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canTalk) return;

        if (collision.transform.TryGetComponent(out Player player))
            startAnim.SetBool("startOpen", true);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            startAnim.SetBool("startOpen", false);
            dialogueManager.EndDialogue();
        }
    }
}
