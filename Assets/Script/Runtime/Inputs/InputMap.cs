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

    //[SerializeField] int/float wheelValue;

    //TEMP 
    [SerializeField] KeyCode backwardKey;
    [SerializeField] KeyCode forwardKey;
    public KeyCode BackwardKey => backwardKey;
    public KeyCode ForwardKey => forwardKey;
}
