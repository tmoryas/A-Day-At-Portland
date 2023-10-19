using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Inject] private DialogDatabase _dialogDB;
    [Inject] private DialogManager _dialogueManager;
    [Inject] private IInputManager _inputManager;
    [Inject] private EndingManager _endingManager;

    [SerializeField] private CanvasGroup _rewindEffect;
    [SerializeField] private CanvasGroup _forwardEffect;

    [SerializeField] private RectTransform _watch;
    private float _watchTime;
    private float _maxWatchTime;

    private float _timer;
    [SerializeField] private int _delay;
    private int _minSlot = 0;
    private int _hourSlot = 0;

    private bool switchTime;

    private List<int> _inputs = new List<int>();

    private void Start()
    {
        _rewindEffect.gameObject.SetActive(false);
        _forwardEffect.gameObject.SetActive(false);

        _dialogueManager.GetNextDialog(_hourSlot + _minSlot);

        _maxWatchTime = RealSecondCalcul(_dialogDB.DialogueDataList[_dialogDB.DialogueDataList.Count - 1].Time);
    }

    void Update()
    {
        if (!switchTime)
        {
            _timer += Time.deltaTime;

            if (_watchTime <= _maxWatchTime)
                _watchTime += Time.deltaTime;
            else if (!_endingManager.HasEnded)
            {
                _endingManager.HasEnded = true;
                _endingManager.CheckWin();
            }
            _watch.eulerAngles = new Vector3(0, 0, _watchTime * 180 / _maxWatchTime);
        }

        if(_timer >= _delay)
        {
            _timer = 0;
            _minSlot += 5;
            if(_minSlot >= 60)
            {
                _minSlot = 0;
                _hourSlot += 100;
            }

            _dialogueManager.GetNextDialog(_hourSlot + _minSlot);
            _dialogueManager.UpdateDialog(_inputs, true);
        }

        if (!_inputs.SequenceEqual(_inputManager.PortToCharaIDs))
        {
            _inputs = _inputManager.PortToCharaIDs;
            _dialogueManager.UpdateDialog(_inputs);
            //Debug.Log("input change");
        }

        if (Input.GetKeyDown(_inputManager.CurrentMap.ForwardKey))
        {
            _watchTime = RealSecondCalcul(_hourSlot + 100);
            _dialogueManager.ForwardDialogData(_hourSlot + 100);
            StartCoroutine(SwitchTime(_hourSlot + 100, _forwardEffect));
        }
        else if (Input.GetKeyDown(_inputManager.CurrentMap.BackwardKey) && !_endingManager.GoodEnding)
        {
            _watchTime = 0;
            _dialogueManager.ResetDialogData();
            if (_endingManager.HasEnded) _endingManager.HasEnded = false;
            StartCoroutine(SwitchTime(0, _rewindEffect));
        }
    }

    private IEnumerator SwitchTime(int nextTime, CanvasGroup effect)
    {
        _timer = 0;
        _minSlot = 0;
        _hourSlot = nextTime;

        _dialogueManager.ActualDialog.Clear();
        _dialogueManager.UpdateDialog(_inputs);

        switchTime = true;

        effect.gameObject.SetActive(true);
        effect.DOFade(1, 0.3f);

        _watch.DORotate(new Vector3(0, 0, RealSecondCalcul(_hourSlot + _minSlot) * 180 / _maxWatchTime), 2.8f);

        yield return new WaitForSeconds(2.6f);
        effect.DOFade(0, 0.3f).OnComplete(() => {
            effect.gameObject.SetActive(false);
            _dialogueManager.GetNextDialog(_hourSlot + _minSlot);
            _dialogueManager.UpdateDialog(_inputs);
            switchTime = false;
        });
    }

    private int RealSecondCalcul(int time)
    {
        int hour = time / 100;
        int min = time - 100 * hour;
        int allMin = hour * 60 + min;
        return allMin * _delay / 5;
    }
}
