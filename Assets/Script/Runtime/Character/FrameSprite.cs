using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FrameSprite
{
    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _character;
    [SerializeField] private Sprite _frame;
    [SerializeField] private Sprite _frame_shadow;

    [SerializeField] private Sprite _lightEffect;

    public Sprite Background => _background;
    public Sprite Character => _character;
    public Sprite Frame => _frame;
    public Sprite Frame_shadow => _frame_shadow;
    public Sprite LightEffect => _lightEffect;
}
