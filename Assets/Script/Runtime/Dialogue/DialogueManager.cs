using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DialogueManager : MonoBehaviour
{
    [Inject] private DialogueDatabase _dialogueDB;
    [SerializeField] private LanguageState _languageState;

    void Awake()
    {
        _dialogueDB.DialogueQueue = new Queue<DialogueData>(_dialogueDB.DialogueDataList);
    }

    public List<DialogueData> GetNextDialogue(int time)
    {
        List<DialogueData> dialogueListTemp = new List<DialogueData>();

        int max = _dialogueDB.DialogueQueue.Count;
        for (int i = 0; i < max; i++)
        {
            if (_dialogueDB.DialogueQueue.First().Time == time)
                dialogueListTemp.Add(_dialogueDB.DialogueQueue.Dequeue());
            else
                break;
        }

        return dialogueListTemp;
    }

    public void PrintDialogue(DialogueData data)
    {
        Debug.Log("<color=blue>" + data.Text[(int)_languageState] + "</color>");
    }
}
