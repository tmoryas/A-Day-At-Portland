using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogueData
{
    private int _characterId;
    private string _emotionSprite;
    private int _time;
    private List<string> _text;

    public int CharacterId => _characterId;
    public string EmotionSprite => _emotionSprite;
    public int Time => _time;
    public List<string> Text => _text;

    public DialogueData(int characterId, string emotionSprite, int time, List<string> text)
    {
        _characterId = characterId;
        _emotionSprite = emotionSprite;
        _time = time;
        _text = text;
    }
}
