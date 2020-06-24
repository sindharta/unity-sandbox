using System;
using System.Net.Sockets;
using UnityEngine;
using System.Text;

public class TcpClientController : MonoBehaviour {

    const float TIMEOUT = 5; //5 seconds.

    NetworkStatus m_networkStatus = NetworkStatus.Disconnected;
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
            case NetworkStatus.Disconnected:  {
                AsyncCallback callback = new AsyncCallback(ConnectCallback);
                m_tcpClient.BeginConnect("127.0.0.1", Constants.NETWORK_TCP_PORT, callback, this);
                m_networkStatus = NetworkStatus.Connecting;
                m_beginConnectTime = Time.realtimeSinceStartup;
                break;
            }
            case NetworkStatus.Connecting: {

                //Untested !!!
                if (Time.realtimeSinceStartup - m_beginConnectTime > TIMEOUT) {
                    m_tcpClient.Close();
                    m_networkStatus = NetworkStatus.Disconnected;
                }

                break;
            }
            case NetworkStatus.StartToConnect: {
                if (m_tcpClient.IsDisconnected()) {
                    m_networkStatus = NetworkStatus.Disconnected;
                }
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
        client_controller.m_networkStatus = NetworkStatus.StartToConnect;
        client_controller.m_tcpClient.EndConnect(result);

        client_controller.SendToServer("Are you there, server ?");
    }


//---------------------------------------------------------------------------------------------------------------------
}