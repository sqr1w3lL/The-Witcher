using UnityEngine;

public class DialogueAnimatorKing : MonoBehaviour
{
    [SerializeField] private Animator startAnim;
    [SerializeField] private DialogueManagerKing dialogueManagerKing;

    public bool canTalk = true;

    public void DisableTalk()
    {
        canTalk = false;
        startAnim.SetBool("startOpen", false);

        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTalk) return;

        if (collision.transform.TryGetComponent(out Player player))
            startAnim.SetBool("startOpen", true);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            startAnim.SetBool("startOpen", false);
            dialogueManagerKing.EndDialogue();
        }
    }
}
