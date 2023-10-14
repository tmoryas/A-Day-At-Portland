using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaliseEnum : MonoBehaviour
{
    public enum Clues
    {
        Date,
        Heure,
        Lieu,
        Metier
    }

    //dico couleur

    private static Dictionary<Clues, string> clueColors = new Dictionary<Clues, string>()
    {
        { Clues.Date, "<color=blue>" },
        { Clues.Heure, "<color=blue>" },
        { Clues.Lieu, "<color=red>" },
        { Clues.Metier, "<color=green>" }

    };

    private static Dictionary<string, string> clueColorsString = new Dictionary<string, string>()
    {
        { "<Date>", "<color=blue>" },
        { "<Heure>", "<color=blue>" },
        { "<Lieu>", "<color=red>" },
        { "<Metier>", "<color=green>" }

    };

    public static Dictionary<Clues, string> ClueColors { get => clueColors; }
    public static Dictionary<string, string> ClueColorsString { get => clueColorsString; }
}
