//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO.Ports;

//public class Seria : MonoBehaviour
//{
    
//    public static Seria Instance;
//    SerialPort Serial;
//    public bool ManuelPort = false;
//    [SerializeField] private string _portName = "COM3";
//    [SerializeField] private float _timeRead = 0.4f;
//    bool connected = false;
//    bool tryConnection = false;

    
//    public List<connect> listconnect = new List<connect>();


//    void Start () {
//        StartCoroutine(Connect());
//        InvokeRepeating("CheckValue", 0.0f, _timeRead);
//	}

//    // Update is called once per frame
//    void Update() {
//        if (Serial!= null && !Serial.IsOpen)
//        {
//            if(!tryConnection) StartCoroutine(Connect());
//        }
//        if(!connected) return;
        
//    }

//    void CheckValue(){
//        try
//        {
//            List<connect> listconnectTemp = new List<connect>();
//            string[] val = Serial.ReadLine().Split('/');
//            for (int i = 0; i < val.Length; i++)
//            {
//                if(i == val.Length -1) break;
//                Debug.Log("i " + i + "  val " + val[i]);
//                string[] valConnect = val[i].Split('!');
//                connect con = new connect();
//                con.x = valConnect[0];
//                con.y = valConnect[1];
//                listconnectTemp.Add(con);
//            }
//            listconnect = listconnectTemp;    
        
//        }
//        catch (System.Exception)
//        {
//            Debug.LogWarning("error read port");
//        }        
//    }

//    IEnumerator Connect()
//    {
//        tryConnection = true;
//        yield return new WaitForSeconds(1);
//        if (ManuelPort)
//        {
//            Serial = new SerialPort(PortName, 9600);
//            Serial.ReadTimeout = 1;
//            Serial.Open();
//            if (Serial.IsOpen)
//            {
//                Debug.Log("Serial port open");
//                connected = true;
//            }
//            else
//            {
//                Debug.LogWarning("Serial port not open");
//            }
//            tryConnection = false;
//            yield break;
//        }
//        string[] ports = SerialPort.GetPortNames();
//        foreach (string port in ports)
//        {
//            Debug.Log(port);
//        }
//        // open the port
//        if (ports.Length == 0)
//        {
//            Debug.LogWarning("No ports");
//            yield break;
//        }
//        Serial = new SerialPort(ports[0], 9600);
//        Serial.ReadTimeout = 1;
//        Serial.Open();
//        if (Serial.IsOpen)
//        {
//            Debug.Log("Serial port open");
//            connected = true;
//        }
//        else
//        {
//            Debug.LogWarning("Serial port not open");
//        }
//        tryConnection = false;
        
//    }
//    private void Awake()
//    {
//        Instance = this;
//        DontDestroyOnLoad(this.gameObject);
//    }
//}

//[System.Serializable]
//   public class connect{
//       public string x;
//       public string y;

//    }
