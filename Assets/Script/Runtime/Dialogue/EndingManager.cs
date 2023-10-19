using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EndingManager : MonoBehaviour
{

    [Inject] private BaliseGetter _baliseGetter;

    private bool hasEnded;
    private bool goodEnding;

    public bool HasEnded { get => hasEnded; set => hasEnded = value; }
    public bool GoodEnding { get => goodEnding; set => goodEnding = value; }

    public void CheckWin()
    {
        if (_baliseGetter.CluesDictionaries.SavedClues == _baliseGetter.CluesDictionaries.ClueGoodEnding)
        {
            goodEnding = true;
            //good ending
        }
        else if (_baliseGetter.CluesDictionaries.SavedClues == _baliseGetter.CluesDictionaries.ClueBadEnding)
        {
            //bad ending
        }
        else
        {
            //very bad ending
        }
    }

}
