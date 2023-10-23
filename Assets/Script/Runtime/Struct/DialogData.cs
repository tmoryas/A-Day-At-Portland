using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogData
{
    [SerializeField] private int _characterId;
    [SerializeField] private string _emotionSprite;
    [SerializeField] private int _startTime;
    [SerializeField] private int _duration;
    [SerializeField] private int _talkWithId;
    [SerializeField] private List<string> _text;

    public int CharacterId => _characterId;
    public string EmotionSprite => _emotionSprite;
    public int StartTime => _startTime;
    public int Duration => _duration;
    public int TalkWithId => _talkWithId;
    public List<string> Text => _text;

    public DialogData(int characterId, string emotionSprite, int startTime, int duration, int talkWithId, List<string> text)
    {
        _characterId = characterId;
        _emotionSprite = emotionSprite;
        _startTime = startTime;
        _duration = duration;
        _talkWithId = talkWithId;
        _text = text;
    }
}
