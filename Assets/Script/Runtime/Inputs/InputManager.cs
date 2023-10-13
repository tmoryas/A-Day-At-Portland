using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{

    [SerializeField] private InputMap _currentMap;
    private Dictionary<KeyCode, int> portToCharaID = new Dictionary<KeyCode, int>();
    private List<int> portToCharaIDs;
    [SerializeField] private int _maxPorts = 4;

    public List<int> PortToCharaIDs => portToCharaIDs;

    private void Start()
    {
        int i = 1;
        foreach (KeyCode k in _currentMap.PortKeys)
        {
            portToCharaID.Add(k, i++);
        }
    }

    private void Update()
    {
        portToCharaIDs = GetActiveChara();
        //foreach (int i in portToCharaIDs) UnityEngine.Debug.Log(i);
    }

    private List<int> GetActiveChara()
    {
        List <int> tempList = new List<int>();

        foreach (KeyCode k in _currentMap.PortKeys)
        {
            if (Input.GetKey(k))
            {
                if (tempList.Count > _maxPorts) return tempList;
                tempList.Add(portToCharaID[k]);
            }
        }

        return tempList;
    }
}
