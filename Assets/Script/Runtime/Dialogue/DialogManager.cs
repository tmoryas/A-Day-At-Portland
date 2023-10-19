using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DialogManager : MonoBehaviour
{
    [Inject] private DialogDatabase _dialogDB;
    [Inject] private BaliseGetter _baliseGetter;
    [SerializeField] private LanguageState _languageState;

    [Header("Bubble OBJ")]
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject _topRight, _topLeft, _bottomRight, _bottomLeft;

    [Header("Sprite")]
    [SerializeField] private SerializedDictionary<int, CharacterObj> _characterSprite;
    [SerializeField] private FrameSprite _spriteEteint;
    [SerializeField] private FrameSprite _spriteAllumer;

    [Header("Information")]
    [SerializeField] private SerializedDictionary<int, RectTransform> _characterTransform; // -> Instantier les bulles a cette endroit
    [SerializeField] private SerializedDictionary<int, CharacterPlaceEnum> _characterPlace; // -> Set a la main la position des cadres pour les bulles
    private Dictionary<int, (BubbleProxy, BubblePosState)> _actualBubbleObjPos = new Dictionary<int, (BubbleProxy, BubblePosState)>(); // -> Pour stocker la bulle instantier et la place qu'elle utilise
    private Dictionary<BubblePosState, bool> _spaceUse = new Dictionary<BubblePosState, bool>() 
    {
        { BubblePosState.TOP, false },
        { BubblePosState.MIDDLE, false },
        { BubblePosState.BOTTOM, false }
    }; // -> Pour savoir quel espace du "plateau" est utiliser

    [Header("Dialogue actuel")]
    private Dictionary<int, DialogData> _actualDialog = new Dictionary<int, DialogData>();
    private Dictionary<int, bool> _characterTalkingDic = new Dictionary<int, bool>();

    public Dictionary<int, DialogData> ActualDialog { get => _actualDialog; }

    void Awake()
    {
        ResetDialogData();
        
        for (int i = 1; i < 11; i++)
            _characterTalkingDic.Add(i, false);
    }

    public void ResetDialogData()
    {
        _dialogDB.DialogQueue = new Queue<DialogData>(_dialogDB.DialogueDataList);
        for(int i = 1; i < 11; i++)
            _characterSprite[i].LightEffect.sprite = _spriteEteint.LightEffect;
    }

    public void ForwardDialogData(int time)
    {
        _dialogDB.DialogQueue = new Queue<DialogData>(_dialogDB.DialogQueue.Where(dialog => dialog.Time >= time).OrderBy(d => d.Time).ToList());
        for (int i = 1; i < 11; i++)
            _characterSprite[i].LightEffect.sprite = _spriteEteint.LightEffect;
    }

    public void GetNextDialog(int time)
    {
        foreach (DialogData data in _actualDialog.Values)
            _characterSprite[data.CharacterId].LightEffect.sprite = _spriteEteint.LightEffect;

        _actualDialog = new Dictionary<int, DialogData>();

        int max = _dialogDB.DialogQueue.Count;
        for (int i = 0; i < max; i++)
        {
            if (_dialogDB.DialogQueue.First().Time == time)
            {
                DialogData data = _dialogDB.DialogQueue.Dequeue();
                _actualDialog.Add(data.CharacterId, data);

                _characterSprite[data.CharacterId].LightEffect.sprite = _spriteAllumer.LightEffect;
            }
            else
                break;
        }
    }

    public void UpdateDialog(List<int> inputPressed, bool forcedMaj = false)
    {
        for(int i = 1; i < _characterTalkingDic.Values.Count + 1; i++)
        {
            if ((_characterTalkingDic[i] && !inputPressed.Contains(i) && !_actualDialog.ContainsKey(i)) 
                || (_characterTalkingDic[i] && inputPressed.Contains(i) && !_actualDialog.ContainsKey(i))
                || (_characterTalkingDic[i] && !inputPressed.Contains(i) && _actualDialog.ContainsKey(i)))
            {
                // Close Bubble
                //Debug.Log("Close");

                if (_actualBubbleObjPos[i].Item1 != null)
                {
                    _baliseGetter.DeleteAvailableClue(i);
                    Destroy(_actualBubbleObjPos[i].Item1.BubbleRoot);

                    _spaceUse[_actualBubbleObjPos[i].Item2] = false;
                    _actualBubbleObjPos.Remove(i);

                    _characterSprite[i].Background.sprite = _spriteEteint.Background;
                    _characterSprite[i].Frame.sprite = _spriteEteint.Frame;

                    _characterTalkingDic[i] = false;
                }
            }
            else if(!_characterTalkingDic[i] && inputPressed.Contains(i) && _actualDialog.ContainsKey(i))
            {
                // Open Bubble
                //Debug.Log("Open");

                (GameObject, BubblePosState) bubbleData = GetRightBubble(i);
                
                _spaceUse[bubbleData.Item2] = true;

                BubbleProxy bubble = Instantiate(bubbleData.Item1, _characterTransform[i].position, Quaternion.identity, canvas).GetComponent<BubbleProxy>();
                _actualBubbleObjPos.Add(i, (bubble, bubbleData.Item2));
                
                bubble.TextComponent.text = _baliseGetter.CleanSentence(i, _actualDialog[i].Text[(int)_languageState]);

                _characterSprite[i].Background.sprite = _spriteAllumer.Background;
                _characterSprite[i].Frame.sprite = _spriteAllumer.Frame;

                _characterTalkingDic[i] = true;
            }
            else if(_characterTalkingDic[i] && inputPressed.Contains(i) && _actualDialog.ContainsKey(i) && forcedMaj)
            {
                // MAJ Text
                //Debug.Log("MAJ");

                _actualBubbleObjPos[i].Item1.TextComponent.text = _baliseGetter.CleanSentence(i, _actualDialog[i].Text[(int)_languageState]);
            }

            //NOTHING
        }
    }

    private (GameObject, BubblePosState) GetRightBubble(int id)
    {
        CharacterPlaceEnum charaPlace = _characterPlace[id];

        switch (charaPlace)
        {
            case CharacterPlaceEnum.TOP_LEFT:

                if (!_spaceUse[BubblePosState.TOP])
                    return (_topRight, BubblePosState.TOP);
                else 
                    return (_bottomRight, BubblePosState.MIDDLE);

            case CharacterPlaceEnum.TOP_RIGHT:

                if (!_spaceUse[BubblePosState.TOP])
                    return (_topLeft, BubblePosState.TOP);
                else
                    return (_bottomLeft, BubblePosState.MIDDLE);

            case CharacterPlaceEnum.BOTTOM_LEFT:

                if (!_spaceUse[BubblePosState.MIDDLE])
                    return (_topRight, BubblePosState.MIDDLE);
                else
                    return (_bottomRight, BubblePosState.BOTTOM);

            case CharacterPlaceEnum.BOTTOM_RIGHT:

                if (!_spaceUse[BubblePosState.MIDDLE])
                    return (_topLeft, BubblePosState.MIDDLE);
                else
                    return (_bottomLeft, BubblePosState.BOTTOM);

            default:
                return default;
        }
    }
}
