using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{

    [SerializeField] private InputMap currentMap;
    private Dictionary<KeyCode, int> portToCharaID;
    private List<int> portToCharaIDs;

    public List<int> PortToCharaIDs => portToCharaIDs;

    private void Start()
    {
        int i = 0;
        foreach (KeyCode k in currentMap.PortKeys)
        {
            portToCharaID.Add(k, i++);
        }
    }

    private void Update()
    {
        portToCharaIDs = GetActiveChara();
    }

    private List<int> GetActiveChara()
    {
        List <int> tempList = new List<int>();

        foreach (KeyCode k in currentMap.PortKeys)
        {
            if (Input.GetKey(k)) tempList.Add(portToCharaID[k]);
        }

        return tempList;
    }
}
