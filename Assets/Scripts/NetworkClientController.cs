using System;
using System.Net.Sockets;
using UnityEngine;
using System.Text;

public class NetworkClientController : MonoBehaviour {

    const float TIMEOUT = 5; //5 seconds.

    NetworkPeerType m_networkStatus = NetworkPeerType.Disconnected;
    TcpClient m_tcpClient = null;
    float m_beginConnectTime = 0;
    string m_message="";

//---------------------------------------------------------------------------------------------------------------------

    void Start() {
        m_tcpClient = new TcpClient();
    }

//---------------------------------------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update () {
        switch (m_networkStatus) {
            case NetworkPeerType.Disconnected:  {
                AsyncCallback callback = new AsyncCallback(ConnectCallback);
                m_tcpClient.BeginConnect("127.0.0.1", Constants.NETWORK_PORT, callback, this);
                m_networkStatus = NetworkPeerType.Connecting;
                m_beginConnectTime = Time.realtimeSinceStartup;
                break;
            }
            case NetworkPeerType.Connecting: {

                //Untested !!!
                if (Time.realtimeSinceStartup - m_beginConnectTime > TIMEOUT) {
                    m_tcpClient.Close();
                    m_networkStatus = NetworkPeerType.Disconnected;
                }

                break;
            }
            case NetworkPeerType.Client: {
                if (m_tcpClient.IsDisconnected()) {
                    m_networkStatus = NetworkPeerType.Disconnected;
                }
                break;
            }
            case NetworkPeerType.Server: {
                Debug.LogError("Should not be here because this gameobject is not a server");
                break;
            }
        }

    }

//---------------------------------------------------------------------------------------------------------------------

    void OnDestroy() {
        m_tcpClient.Close();
    }


//---------------------------------------------------------------------------------------------------------------------

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 150, 30), "Message: ");

        string send_control_name = "SendToServer";

        GUI.SetNextControlName(send_control_name);
        m_message = GUI.TextField(new Rect(10, 30, 100, 30), m_message);
        Event e = Event.current;
        if (GUI.Button(new Rect(115, 30, 100, 30), "Send to server") 
            || IsKeyPressed(e, KeyCode.Return, send_control_name)) 
        {
            SendToServer(m_message);
            m_message = "";
        }

    }

//---------------------------------------------------------------------------------------------------------------------
    bool IsKeyPressed(Event e, KeyCode keyCode, string controlName) {
        if (e.isKey && e.keyCode == keyCode && GUI.GetNameOfFocusedControl() == controlName) {
            return true;
        }

        return false;
    }


//---------------------------------------------------------------------------------------------------------------------

    void SendToServer(string message) {
        NetworkStream stream = m_tcpClient.GetStream();
        Byte[] send_bytes = Encoding.UTF8.GetBytes(message);
        stream.Write(send_bytes, 0, send_bytes.Length);
    }
//---------------------------------------------------------------------------------------------------------------------

    static void ConnectCallback(IAsyncResult result)
    {
        Debug.Log("Client says: Client connected to server");

        NetworkClientController client_controller = (NetworkClientController)result.AsyncState;
        client_controller.m_networkStatus = NetworkPeerType.Client;
        client_controller.m_tcpClient.EndConnect(result);

        client_controller.SendToServer("Are you there, server ?");
    }


//---------------------------------------------------------------------------------------------------------------------
}