using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogData
{
    [SerializeField] private int _characterId;
    [SerializeField] private string _emotionSprite;
    [SerializeField] private int _time;
    [SerializeField] private List<string> _text;

    public int CharacterId => _characterId;
    public string EmotionSprite => _emotionSprite;
    public int Time => _time;
    public List<string> Text => _text;

    public DialogData(int characterId, string emotionSprite, int time, List<string> text)
    {
        _characterId = characterId;
        _emotionSprite = emotionSprite;
        _time = time;
        _text = text;
    }
}
