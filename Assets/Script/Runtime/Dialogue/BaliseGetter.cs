using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaliseGetter : MonoBehaviour
{
    [SerializeField] private List<string> _clueType = new List<string>();
    [SerializeField] private List<List<string>> _clueFamilies = new List<List<string>>();
    [SerializeField] private string dialogz;
    private string endBalise = "</>";

    private void Start()
    {
        List<(string, string)> u = new List<(string, string)>();
        u = GetClue(dialogz);
        foreach ((string, string) c in u) if(c.Item2 != null) Debug.Log(c);
    }

    private List<(string, string)> GetClue(string stc)
    {
        List<(string, string)> result = new List<(string, string)>();
        string sentence = stc;

        if (!sentence.Contains(endBalise)) return result;

        foreach (string clue in _clueType)
        {
            if (sentence.Contains(clue))
            {
                result.Add((clue, sentence.Substring(sentence.IndexOf(clue) + clue.Length, sentence.IndexOf(endBalise) - sentence.IndexOf(clue) - clue.Length).Trim()));
                sentence = sentence.Substring(sentence.IndexOf(endBalise) + endBalise.Length);
            }
        }
        return result;
    }

}
