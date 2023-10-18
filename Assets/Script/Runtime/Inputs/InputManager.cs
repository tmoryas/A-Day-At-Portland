using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour, IInputManager
{
    [Inject] private BaliseGetter _baliseGetter;
    [Inject] private DialogManager _dialogManager;
    [SerializeField] private InputMap _currentMap;
    private Dictionary<KeyCode, int> portToCharaID = new Dictionary<KeyCode, int>();
    private List<int> portToCharaIDs;
    [SerializeField] private int _maxPorts = 4;

    [SerializeField] private float bannerMoveDistance = 350f;
    [SerializeField] private float bannerMoveSpeed = 0.15f;

    [SerializeField] private SerializedDictionary<KeyCode, CluesDictionaries.FamiliesEnum> inputToClue;
    private CluesDictionaries.FamiliesEnum selectedClue;
    private CluesDictionaries.FamiliesEnum previousClue;

    public InputMap CurrentMap => _currentMap;
    public List<int> PortToCharaIDs => portToCharaIDs;


    private void Start()
    {
        int i = 1;
        foreach (KeyCode k in _currentMap.PortKeys)
        {
            portToCharaID.Add(k, i++);
        }

        selectedClue = CluesDictionaries.FamiliesEnum.Time;
        previousClue = selectedClue;
    }

    private void Update()
    {
        portToCharaIDs = GetActiveChara();
        selectedClue = ClueTypeSelection();
        if (previousClue != selectedClue && selectedClue != CluesDictionaries.FamiliesEnum.Rien)
        {
            _baliseGetter.CluesDictionaries.BannerDico[previousClue].transform.
                DOMoveX(_baliseGetter.CluesDictionaries.BannerDico[previousClue].transform.position.x + bannerMoveDistance, 0.15f).SetEase(Ease.OutCubic);
            _baliseGetter.CluesDictionaries.BannerDico[selectedClue].transform.
                DOMoveX(_baliseGetter.CluesDictionaries.BannerDico[selectedClue].transform.position.x - bannerMoveDistance, 0.15f).SetEase(Ease.OutCubic);
            previousClue = selectedClue;
        }
        if (Input.GetKeyDown(_currentMap.LeverKey)) _baliseGetter.SaveClue(selectedClue);
    }

    private List<int> GetActiveChara()
    {
        List <int> tempList = new List<int>();

        foreach (KeyCode k in _currentMap.PortKeys)
        {
            if (Input.GetKey(k))
            {
                if (tempList.Count > _maxPorts) return tempList;
                tempList.Add(portToCharaID[k]);
            }
        }

        return tempList;
    }

    private CluesDictionaries.FamiliesEnum ClueTypeSelection()
    {
        foreach (KeyCode k in _currentMap.ClueKeys)
        {
            if (Input.GetKey(k))
            {
                return inputToClue[k];
            }
        }
        return CluesDictionaries.FamiliesEnum.Rien;
    }

    //private string ClueSave(string currentClue)
    //{
    //    //if (selectedClue == CluesDictionaries.CluesEnum.Rien) return currentClue;
    //    if (selectedClue != _baliseGetter.GetClue(_dialogManager.ActualDialog)) return currentClue;

    //}
}
