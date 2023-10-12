using TeamOne_SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class GetGoogleSheet : MonoBehaviour
{
    [Header("Google Sheet")]
    JSONNode jsonFile = null;
    public JSONNode JsonFile => jsonFile;

    #region Google Sheet

    public void GetDataSheet()
    {
        StartCoroutine(ObtainSheetData());
    }

    private IEnumerator ObtainSheetData()
    {
        string link = "https://sheets.googleapis.com/v4/spreadsheets/17aJxl5jKQF-8P1Xp0wEo1qM_4FFoFtYfB29pZe5eQ-s/values/Feuille%201?key=AIzaSyCgG7bKF2jrnQ-_uTtjUUInm5r1Q7H0nKY";
        UnityWebRequest www = UnityWebRequest.Get(link);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError || www.timeout > 2)
        {
            Debug.Log("Error" + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            jsonFile = JSON.Parse(json);
        }
    }

    #endregion
}

//JSON.Parse(o["values"].Count) pour le nombre de ligne
//JSON.Parse(o["values"][0].Count) pour le nombre de colonne
