using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueDatabase : ScriptableObject
{
    [SerializeField] private List<DialogueData> dialogueDataList = new List<DialogueData>();
    public List<DialogueData> DialogueDataList { get => dialogueDataList; set => dialogueDataList = value; }


    [SerializeField] private Queue<DialogueData> dialogueQueue = new Queue<DialogueData>();
    public Queue<DialogueData> DialogueQueue { get => dialogueQueue; set => dialogueQueue = value; }
}