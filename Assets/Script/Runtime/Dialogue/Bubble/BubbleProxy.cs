using TMPro;
using UnityEngine;

public class BubbleProxy : MonoBehaviour
{
    [SerializeField] private GameObject _bubbleRoot;
    [SerializeField] private TextMeshProUGUI _textComponent;
    public GameObject BubbleRoot => _bubbleRoot;
    public TextMeshProUGUI TextComponent => _textComponent;
}
