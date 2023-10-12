using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEditor.VersionControl;

public class GameManager : MonoBehaviour
{
    [Inject] private DialogueDatabase _dialogueDB;

    private float _timer;
    [SerializeField] private int _delay;
    private int _minSlot = 0;
    private int _hourSlot = 0;

    private List<DialogueData> _actualDialogue = new List<DialogueData>();

    private void Start()
    {
        _dialogueDB.DialogueQueue = new Queue<DialogueData>(_dialogueDB.DialogueDataList);
        GetNextDialogue();
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

            GetNextDialogue();
        }
    }

    private void GetNextDialogue()
    {
        _actualDialogue.Clear();

        int max = _dialogueDB.DialogueQueue.Count;
        for (int i = 0; i < max; i++)
        {
            if (_dialogueDB.DialogueQueue.First().Time == _hourSlot + _minSlot)
                _actualDialogue.Add(_dialogueDB.DialogueQueue.Dequeue());
            else
                break;
        }
    }
}
