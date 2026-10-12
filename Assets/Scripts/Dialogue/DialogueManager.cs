using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Animator boxAnim;
    [SerializeField] private Animator startAnim;
    [SerializeField] private TMPro.TMP_Text dialogueText;
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private ActivatorQuest activatorQuest;
    [SerializeField] private QuestBlocker questBlocker;
    [SerializeField] private QuestChecker questChecker;
    [SerializeField] private DialogueAnimator dialogueAnimator;
    [SerializeField] private TaskCheckerSO taskCheckerSO;

    public event EventHandler questIsStarted;
    private Queue<string> _sentences;
    private bool _endQuest;
    private bool _questStarted;

    private void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);

        nameText.text = dialogue.name;
        _sentences.Clear();

        if (activatorQuest.questStarted)
        { 
            if (!questChecker.questIsCompleted)
                dialogue.sentences = questBlocker.sentenceQuest;
            else
                dialogue.sentences = questBlocker.finalSentenceQuest;
        }

        foreach (string sentence in dialogue.sentences)
            _sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0)
        {
            bool shouldStartQuest = !activatorQuest.questStarted && !_endQuest && !_questStarted;
            EndDialogue();
            if(shouldStartQuest)
            {
                questIsStarted?.Invoke(this, System.EventArgs.Empty);
                _questStarted = true;
            }
            return;
        }
        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
        if (!_endQuest)
            _questStarted = false;

        if (questChecker.questIsCompleted)
            EndQuest();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        boxAnim.SetBool("boxOpen", false);
        if (_endQuest)
        {
            questChecker.enabled = false;
            activatorQuest.ActivateQuest(false);
            dialogueAnimator.DisableTalk();
            _endQuest = false;
        }
    }

    public void EndQuest()
    {
        Debug.Log("Quest ended");
        taskCheckerSO.questCompleted = false;
        questChecker.questIsCompleted = false;
        _endQuest = true;
    }
}

