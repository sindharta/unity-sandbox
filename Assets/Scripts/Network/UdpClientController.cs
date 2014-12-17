using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

class UdpClientController : UdpBaseController {
    [SerializeField]
    string m_serverIP;

    UdpClient m_client;

//---------------------------------------------------------------------------------------------------------------------

    void Start() {
        m_client = new UdpClient(Constants.NETWORK_UDP_CLIENT_PORT);
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(m_serverIP), Constants.NETWORK_UDP_SERVER_PORT); //server port
        m_client.Connect(ep);

        InitReceiverThread(m_client, ep, OnDataReceived);
    }

//---------------------------------------------------------------------------------------------------------------------

    void OnDataReceived(byte[] data) {
        string message = Encoding.UTF8.GetString(data);

        Debug.Log(message);
    }

//---------------------------------------------------------------------------------------------------------------------

    public void Send(string message) {
        byte[] data= Encoding.ASCII.GetBytes(message);
        m_client.Send(data, data.Length);
    }
}