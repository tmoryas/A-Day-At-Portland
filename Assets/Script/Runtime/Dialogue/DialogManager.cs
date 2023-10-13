using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DialogManager : MonoBehaviour
{
    [Inject] private DialogDatabase _dialogDB;
    [SerializeField] private LanguageState _languageState;

    private List<DialogData> _actualDialog = new List<DialogData>();
    private Dictionary<int, bool> _characterTalkingDic = new Dictionary<int, bool>(); 


    void Awake()
    {
        _dialogDB.DialogueQueue = new Queue<DialogData>(_dialogDB.DialogueDataList);
        
        for (int i = 1; i < 11; i++)
            _characterTalkingDic.Add(i, false);
    }

    public void GetNextDialog(int time)
    {
        _actualDialog = new List<DialogData>();

        int max = _dialogDB.DialogueQueue.Count;
        for (int i = 0; i < max; i++)
        {
            if (_dialogDB.DialogueQueue.First().Time == time)
                _actualDialog.Add(_dialogDB.DialogueQueue.Dequeue());
            else
                break;
        }
    }

    public void UpdateDialog(List<int> inputPressed, bool forcedMaj = false)
    {
        for(int i = 1; i < _characterTalkingDic.Values.Count + 1; i++)
        {
            if ((_characterTalkingDic[i] && !inputPressed.Contains(i) && !_actualDialog.Any(d => d.CharacterId == i)) 
                || (_characterTalkingDic[i] && inputPressed.Contains(i) && !_actualDialog.Any(d => d.CharacterId == i))
                || (_characterTalkingDic[i] && !inputPressed.Contains(i) && _actualDialog.Any(d => d.CharacterId == i)))
            {
                // Close Bubble
            }
            else if(!_characterTalkingDic[i] && inputPressed.Contains(i) && _actualDialog.Any(d => d.CharacterId == i))
            {
                // Open Bubble
            }
            else if(_characterTalkingDic[i] && inputPressed.Contains(i) && _actualDialog.Any(d => d.CharacterId == i) && forcedMaj)
            {
                // MAJ Text
            }

            //NOTHING
        }
    }
}
