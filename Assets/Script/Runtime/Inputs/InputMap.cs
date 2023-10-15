using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Input/Input Map", fileName = "New InputMap")]
public class InputMap : ScriptableObject
{
    [SerializeField] List<KeyCode> portKeys;
    [SerializeField] List<KeyCode> clueKeys;
    [SerializeField] KeyCode leverKey;

    public List<KeyCode> PortKeys { get => portKeys; set => portKeys = value; }
    public List<KeyCode> ClueKeys { get => clueKeys; set => clueKeys = value; }
    public KeyCode LeverKey { get => leverKey; set => leverKey = value; }
    //[SerializeField] int/float wheelValue;
}
