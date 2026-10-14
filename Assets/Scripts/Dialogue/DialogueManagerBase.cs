using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class DialogueManagerBase : MonoBehaviour, IDialogueManager, IStartDialogue
{
    private static readonly int BoxOpenHash = Animator.StringToHash("boxOpen");
    private static readonly int StartOpenHash = Animator.StringToHash("startOpen");

    [SerializeField, FormerlySerializedAs("_boxAnim")] protected Animator boxAnim;
    [SerializeField, FormerlySerializedAs("_startAnim")] protected Animator startAnim;
    [SerializeField, FormerlySerializedAs("_dialogueText")] protected TMPro.TMP_Text dialogueText;
    [SerializeField, FormerlySerializedAs("_nameText")] protected TMPro.TMP_Text nameText;

    protected readonly Queue<string> sentences = new Queue<string>();

    public void StartDialogue(Dialogue dialogue)
    {
        boxAnim.SetBool(BoxOpenHash, true);
        startAnim.SetBool(StartOpenHash, false);

        nameText.text = dialogue.name;
        sentences.Clear();

        PrepareDialogue(dialogue);

        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            OnSentencesFinished();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        OnSentenceShown();
    }

    public virtual void EndDialogue()
    {
        boxAnim.SetBool(BoxOpenHash, false);
    }

    protected virtual void PrepareDialogue(Dialogue dialogue) { }
    protected virtual void OnSentencesFinished() => EndDialogue();
    protected virtual void OnSentenceShown() { }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}