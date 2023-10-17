using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterObj : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _character;
    [SerializeField] private Image _frame;

    [SerializeField] private Image _lightEffect;

    public Image Background => _background;
    public Image Character => _character;
    public Image Frame => _frame; 
    public Image LightEffect => _lightEffect;
}
