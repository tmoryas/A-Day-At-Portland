using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private DialogManager _dialogueManager;
    [Inject] private IInputManager _inputManager;

    private float _timer;
    [SerializeField] private int _delay;
    private int _minSlot = 0;
    private int _hourSlot = 0;

    private List<int> _inputs = new List<int>();

    private void Start()
    {
        _dialogueManager.GetNextDialog(_hourSlot + _minSlot);
    }

    void Update()
    {
        _timer += Time.deltaTime;

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

        if (!_inputs.All(_inputManager.PortToCharaIDs.Contains))
        {
            _inputs = _inputManager.PortToCharaIDs;
            _dialogueManager.UpdateDialog(_inputs);
        }
    }
}
