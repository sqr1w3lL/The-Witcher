using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DialogueManagerKing : MonoBehaviour
{
    [SerializeField] private GameObject _winGame;
    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private TMPro.TMP_Text _dialogueText;
    [SerializeField] private TMPro.TMP_Text _nameText;
    [SerializeField] private Animator _boxAnim;
    [SerializeField] private Animator _startAnim;

    private Queue<string> _sentences;

    private void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _boxAnim.SetBool("boxOpen", true);
        _startAnim.SetBool("startOpen", false);

        _nameText.text = dialogue.name;
        _sentences.Clear();

        foreach (string sentence in dialogue.sentences)
            _sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        _boxAnim.SetBool("boxOpen", false);
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(.2f);
        _winGame.SetActive(true);
        _winMenu.WinGame();
    }
}

