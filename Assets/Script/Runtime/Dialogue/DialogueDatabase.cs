using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : ScriptableObject
{
    private List<DialogueData> dialogueDataList = new List<DialogueData>();

    public List<DialogueData> DialogueDataList { get => dialogueDataList; set => dialogueDataList = value; }
}
