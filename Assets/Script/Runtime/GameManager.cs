using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEditor.VersionControl;

public class GameManager : MonoBehaviour
{
    [Inject] private DialogueManager _dialogueManager;
    [Inject] private IInputManager _inputManager;

    private float _timer;
    [SerializeField] private int _delay;
    private int _minSlot = 0;
    private int _hourSlot = 0;

    private List<DialogueData> _actualDialogue = new List<DialogueData>();

    private void Start()
    {
        _actualDialogue = _dialogueManager.GetNextDialogue(_hourSlot + _minSlot);
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

            _actualDialogue = _dialogueManager.GetNextDialogue(_hourSlot + _minSlot);
        }

        for(int i = 0; i < _actualDialogue.Count; i++)
        {
            if (_inputManager.PortToCharaIDs.Contains(_actualDialogue[i].CharacterId))
            {
                _dialogueManager.PrintDialogue(_actualDialogue[i]);
            }
        }
    }
}
