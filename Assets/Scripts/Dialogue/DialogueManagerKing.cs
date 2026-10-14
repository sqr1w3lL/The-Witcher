using System;
using System.Collections;
using UnityEngine;

public class DialogueManagerKing : DialogueManagerBase
{
    [SerializeField] private GameObject _winGame;
    [SerializeField] private WinMenu _winMenu;

    private bool _gameIsWon;

    public override void EndDialogue()
    {
        base.EndDialogue();
        if (_gameIsWon)
        {
            StartCoroutine(EndGame());
        }
    }
    protected override void OnSentencesFinished()
    {
        _gameIsWon = true;
        EndDialogue();
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(.2f);
        _winGame.SetActive(true);
        _winMenu.WinGame();
    }
}