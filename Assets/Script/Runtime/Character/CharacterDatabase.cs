using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Character", fileName ="Character Database")]
public class CharacterDatabase : ScriptableObject
{
    [SerializeField] private SerializedDictionary<int, int> patate;
}

[Serializable]
public struct CharacterSprite
{

}