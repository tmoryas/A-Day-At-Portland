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
    public GameObject GoodEndingGO { get => _goodEndingGO; set => _goodEndingGO = value; }
    public GameObject MidEndingGO { get => _midEndingGO; set => _midEndingGO = value; }
    public GameObject BadEndingGO { get => _badEndingGO; set => _badEndingGO = value; }

    [SerializeField] private GameObject _goodEndingGO;
    [SerializeField] private GameObject _midEndingGO;
    [SerializeField] private GameObject _badEndingGO;

    public void CheckWin()
    {
        if (CompareDico(_baliseGetter.CluesDictionaries.SavedClues, _baliseGetter.CluesDictionaries.ClueGoodEnding))
        {
            goodEnding = true;
            //good ending
            Debug.Log("good :)");
            _goodEndingGO.SetActive(true);
        }
        else if (CompareDico(_baliseGetter.CluesDictionaries.SavedClues, _baliseGetter.CluesDictionaries.ClueBadEnding))
        {
            //bad ending
            Debug.Log("meh :|");
            _midEndingGO.SetActive(true);
        }
        else
        {
            //very bad ending
            Debug.Log("cringe :(");
            _badEndingGO.SetActive(true);
        }
    }

    private bool CompareDico(Dictionary<CluesDictionaries.CluesEnum, string> savedClues, Dictionary<CluesDictionaries.CluesEnum, string> cluesEnding)
    {
        foreach (var clue in savedClues.Keys)
        {
            if (savedClues[clue].ToUpper().Replace(" ", "") != cluesEnding[clue].ToUpper().Replace(" ", "")) return false;
        }
        return true;
    }

}
