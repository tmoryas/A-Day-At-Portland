using AYellowpaper.SerializedCollections;
using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour, IInputManager
{
    [Inject] private BaliseGetter _baliseGetter;
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


    [Header("Rotary Encoder")]
    private bool _unlockChangeTime;
    private int _actualTurn = -1;
    private int _lastStep = 0;
    private bool _clockwise = false;
    private Action<bool> _OnTurnDoneEvent;
    public bool UnlockChangeTime {set => _unlockChangeTime = value; }
    public Action<bool> OnTurnDoneEvent { get => _OnTurnDoneEvent; set => _OnTurnDoneEvent = value; }

    private void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;

        int i = 1;
        foreach (KeyCode k in _currentMap.PortKeys)
        {
            portToCharaID.Add(k, i++);
        }

        selectedClue = CluesDictionaries.FamiliesEnum.Time;
        previousClue = selectedClue;
    }

    void DataReceived(string data, UduinoDevice board)
    {
        if (_unlockChangeTime)
        {
            int step = Int32.Parse(data);

            if(step % 4 == 0)
            {
                if (step > _lastStep && _clockwise)
                {
                    //Debug.Log("ANTI HORAIRE");
                    _clockwise = false;
                    _actualTurn = 0;
                }
                else if(step < _lastStep && !_clockwise)
                {
                    //Debug.Log("HORAIRE");
                    _clockwise = true;
                    _actualTurn = 0;
                }

                _lastStep = step;
               _actualTurn++;
            }

            if(Math.Abs(_actualTurn) >= 20)
            {
                _actualTurn = 0;
                //Send Sens
                _OnTurnDoneEvent.Invoke(_clockwise);
            }
        }
    }

    private void Update()
    {
        portToCharaIDs = GetActiveChara();
        selectedClue = ClueTypeSelection();
        if (previousClue != selectedClue && selectedClue != CluesDictionaries.FamiliesEnum.Rien)
        {
            _baliseGetter.CluesDictionaries.BannerDico[previousClue].transform.
                DOMoveX(_baliseGetter.CluesDictionaries.BannerDico[previousClue].transform.position.x + bannerMoveDistance, bannerMoveSpeed).SetEase(Ease.OutCubic);
            _baliseGetter.CluesDictionaries.BannerDico[selectedClue].transform.
                DOMoveX(_baliseGetter.CluesDictionaries.BannerDico[selectedClue].transform.position.x - bannerMoveDistance, bannerMoveSpeed).SetEase(Ease.OutCubic);
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
