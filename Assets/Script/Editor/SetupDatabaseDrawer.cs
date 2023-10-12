using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TeamOne_SimpleJSON;
using UnityEditor;
using UnityEngine;
using static Codice.Client.Common.Connection.AskCredentialsToUser;

[CanEditMultipleObjects]
[CustomEditor(typeof(GetGoogleSheet))]
public class SetupDatabaseDrawer : Editor
{
    private GetGoogleSheet myObject => target as GetGoogleSheet;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("-------Dialogue Tool-------");
        EditorGUILayout.HelpBox("Just press the button for MAJ all the dialogue", MessageType.Info);
        EditorGUILayout.EndVertical();

        serializedObject.Update();

        if (GUILayout.Button("GET GOOGLE SHEET"))
        {
            myObject.GetDataSheet();
        }

        if (myObject.JsonFile != null)
        {
            EditorGUILayout.HelpBox(myObject.JsonFile["values"].Count + " line detected ", MessageType.None);

            if (GUILayout.Button("New Dialogue Databse"))
            {
                DialogueDatabase asset = ScriptableObject.CreateInstance<DialogueDatabase>();
                string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Scriptable/DialogueDatabase/" + System.DateTime.Now.ToString("dd-MM") + "_Database.asset");

                for (int i = 1; i < myObject.JsonFile["values"].Count; i++)
                {
                    string key = myObject.JsonFile["values"][i][0];
                    string[] splitKey = key.Split('_');
                    List<string> allDialogueTranslation = new List<string>();

                    for (int j = 4; j < myObject.JsonFile["values"][0].Count; j++)
                    {
                        allDialogueTranslation.Add(myObject.JsonFile["values"][i][j]);
                    }

                    asset.DialogueDataList.Add(new DialogueData(Int32.Parse(splitKey[0]), splitKey[1], Int32.Parse(splitKey[2]), allDialogueTranslation));   
                }

                asset.DialogueDataList = asset.DialogueDataList.OrderBy(d => d.Time).ToList();

                AssetDatabase.CreateAsset(asset, name);
                AssetDatabase.SaveAssets();

                Debug.Log("New Database created at Assets/Scriptable/DialogueDatabase");
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
