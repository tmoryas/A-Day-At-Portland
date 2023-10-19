using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesDictionaries : MonoBehaviour
{
    public enum CluesEnum
    {
        Date,
        Lieu,
        Metier,
        Heure,
        Rien
    }

    public enum FamiliesEnum
    {
        Time,
        Place,
        Job,
        Rien
    }


    private Dictionary<string, CluesEnum> clueEnumName = new Dictionary<string, CluesEnum>()
    {
        { "<Date>", CluesEnum.Date },
        { "<Heure>", CluesEnum.Heure },
        { "<Lieu>", CluesEnum.Lieu },
        { "<Metier>", CluesEnum.Metier }

    };

    [SerializeField] private SerializedDictionary<string, string> clueColorsString = new SerializedDictionary<string, string>()
    {
        { "<Date>", "<color=#008ca3>" },
        { "<Heure>", "<color=#008ca3>" },
        { "<Lieu>", "<color=#a21700>" },
        { "<Metier>", "<color=#4eb303>" }

    };

    [SerializeField] private SerializedDictionary<CluesEnum, string> clueGoodEnding;
    [SerializeField] private SerializedDictionary<CluesEnum, string> clueBadEnding;

    private SerializedDictionary<CluesEnum, string> availableClues = new SerializedDictionary<CluesEnum, string>()
    {
        { CluesEnum.Date, ""},
        { CluesEnum.Lieu, ""},
        { CluesEnum.Metier, ""},
        { CluesEnum.Heure, ""}
    };

    private SerializedDictionary<CluesEnum, string> savedClues = new SerializedDictionary<CluesEnum, string>()
    {
        { CluesEnum.Date, ""},
        { CluesEnum.Lieu, ""},
        { CluesEnum.Metier, ""},
        { CluesEnum.Heure, ""}
    };

    [SerializeField] private SerializedDictionary<FamiliesEnum, GameObject> bannerDico = new SerializedDictionary<FamiliesEnum, GameObject>();




    public Dictionary<CluesEnum, string> AvailableClues { get => availableClues; }
    public Dictionary<CluesEnum, string> SavedClues { get => savedClues; }
    public Dictionary<string, CluesEnum> ClueEnumName { get => clueEnumName; }
    public Dictionary<string, string> ClueColorsString { get => clueColorsString; }
    public Dictionary<CluesEnum, string> ClueGoodEnding { get => clueGoodEnding; }
    public Dictionary<CluesEnum, string> ClueBadEnding { get => clueBadEnding; }
    public Dictionary<FamiliesEnum, GameObject> BannerDico { get => bannerDico; }
}
