using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogDatabase : ScriptableObject
{
    [SerializeField, ReadOnly] private List<DialogData> dialogueDataList = new List<DialogData>();
    public List<DialogData> DialogueDataList { get => dialogueDataList; set => dialogueDataList = value; }


    [SerializeField] private Queue<DialogData> dialogueQueue = new Queue<DialogData>();
    public Queue<DialogData> DialogueQueue { get => dialogueQueue; set => dialogueQueue = value; }
}