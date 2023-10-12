using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputMap : ScriptableObject
{
    [SerializeField] List<KeyCode> portKeys;
    [SerializeField] List<KeyCode> clueKeys;
    [SerializeField] KeyCode leverKey;

    public List<KeyCode> PortKeys { get => portKeys; set => portKeys = value; }
    //[SerializeField] int/float wheelValue;
}
