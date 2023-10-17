using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager
{
    public List<int> PortToCharaIDs { get; }
    public InputMap CurrentMap { get; }
}
