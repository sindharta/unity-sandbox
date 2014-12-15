using System;
using System.Net.Sockets;
using UnityEngine;
using System.Text;

public class TcpClientController : MonoBehaviour {

    const float TIMEOUT = 5; //5 seconds.

    NetworkPeerType m_networkStatus = NetworkPeerType.Disconnected;
    TcpClient m_tcpClient = null;
    float m_beginConnectTime = 0;

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


//---------------------------------------------------------------------------------------------------------------------

    public void SendToServer(string message) {
        NetworkStream stream = m_tcpClient.GetStream();
        Byte[] send_bytes = Encoding.UTF8.GetBytes(message);
        stream.Write(send_bytes, 0, send_bytes.Length);
    }
//---------------------------------------------------------------------------------------------------------------------

    static void ConnectCallback(IAsyncResult result)
    {
        Debug.Log("Client says: Client connected to server");

        TcpClientController client_controller = (TcpClientController)result.AsyncState;
        client_controller.m_networkStatus = NetworkPeerType.Client;
        client_controller.m_tcpClient.EndConnect(result);

        client_controller.SendToServer("Are you there, server ?");
    }


//---------------------------------------------------------------------------------------------------------------------
}