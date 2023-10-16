using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaliseGetter : MonoBehaviour
{
    public TextMeshProUGUI txt1,txt2,txt3,txt4;

    [SerializeField] private CluesDictionaries _cluesDictionaries;
    [SerializeField] private List<string> _clueType = new List<string>();
    [SerializeField] private List<List<string>> _clueFamilies = new List<List<string>>();
    [SerializeField] private string dialogz;
    private string endBalise = "</>";
    private string colorEndBalise = "</color>";

    private Dictionary<int, List<CluesDictionaries.CluesEnum>> charaIdToCluesEnum = new Dictionary<int, List<CluesDictionaries.CluesEnum>>();

    public CluesDictionaries CluesDictionaries { get => _cluesDictionaries; set => _cluesDictionaries = value; }

    private void Start()
    {
        List<(string, string)> u = new List<(string, string)>();
        u = GetClue(dialogz);
        foreach ((string, string) c in u) if(c.Item2 != null) Debug.Log(c);
        //ebug.Log(CleanSentence(dialogz));
    }

    public List<(string, string)> GetClue(string stc)
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

    public string CleanSentence (int charaId, string stc)
    {
        GetAvailableClues(charaId, stc);
        string sentence = stc;

        sentence = sentence.Replace(endBalise, colorEndBalise);

        foreach (string clue in _clueType)
        {
            if (sentence.Contains(clue))
            {
                //check color/clue
                sentence = sentence.Replace(clue, CluesDictionaries.ClueColorsString[clue].ToString());
            }
        }

        return sentence;
    }

    private void GetAvailableClues (int charaId, string stc)
    {
        List<(string, string)> baliseList = GetClue(stc);
        if (baliseList.Count == 0)return;
        List<CluesDictionaries.CluesEnum> cluesEnumList = new List<CluesDictionaries.CluesEnum> ();
        foreach (var s in baliseList)
        {
            cluesEnumList.Add(_cluesDictionaries.ClueEnumName[s.Item1]);
            _cluesDictionaries.AvailableClues[_cluesDictionaries.ClueEnumName[s.Item1]] = s.Item2;
        }
        charaIdToCluesEnum.Add(charaId, cluesEnumList);
    }

    public void DeleteAvailableClue (int charaId)
    {
        if (!charaIdToCluesEnum.ContainsKey(charaId)) return;

        foreach(var c in charaIdToCluesEnum[charaId])
        {
            _cluesDictionaries.AvailableClues[c] = "";
        }
        charaIdToCluesEnum.Remove(charaId);
    }

    public void SaveClue(CluesDictionaries.FamiliesEnum family)
    {
        switch (family)
        {
            case CluesDictionaries.FamiliesEnum.Time:
                if (_cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Date] != "")
                {
                    _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Date] = _cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Date];
                    txt1.text = _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Date];
                }
                else if (_cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Heure] != "")
                {
                    _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Heure] = _cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Heure];
                    txt2.text = _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Heure];
                }

                break;
            case CluesDictionaries.FamiliesEnum.Place:
                if (_cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Lieu] != "")
                {
                    _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Lieu] = _cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Lieu];
                    txt3.text = _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Lieu];
                }
                break;
            case CluesDictionaries.FamiliesEnum.Job:
                if (_cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Metier] != "")
                {
                    _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Metier] = _cluesDictionaries.AvailableClues[CluesDictionaries.CluesEnum.Metier];
                    txt4.text = _cluesDictionaries.SavedClues[CluesDictionaries.CluesEnum.Metier];
                }
                break;
        }
    }

}
