using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesDictionaries : MonoBehaviour
{
    public enum CluesEnum
    {
        Date,
        Heure,
        Lieu,
        Metier
    }

    //dico couleur

    private Dictionary<CluesEnum, string> clueColors = new Dictionary<CluesEnum, string>()
    {
        { CluesEnum.Date, "<color=blue>" },
        { CluesEnum.Heure, "<color=blue>" },
        { CluesEnum.Lieu, "<color=red>" },
        { CluesEnum.Metier, "<color=green>" }

    };

    [SerializeField] private SerializedDictionary<string, string> clueColorsString = new SerializedDictionary<string, string>()
    {
        { "<Date>", "<color=blue>" },
        { "<Heure>", "<color=blue>" },
        { "<Lieu>", "<color=red>" },
        { "<Metier>", "<color=green>" }

    };

    [SerializeField] private SerializedDictionary<string, string> clueFinal;

    public Dictionary<CluesEnum, string> ClueColors { get => clueColors; }
    public Dictionary<string, string> ClueColorsString { get => clueColorsString; }
    public Dictionary<string, string> ClueFinal { get => clueFinal; }
}
