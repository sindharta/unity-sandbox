using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

//Server
class UdpServerController : UdpBaseController {
    private IPEndPoint m_serverIP;

    UdpClient m_server;

//---------------------------------------------------------------------------------------------------------------------

    void Start() {
        m_server = new UdpClient(Constants.NETWORK_UDP_SERVER_PORT);
        IPEndPoint ip = new IPEndPoint(IPAddress.Any, Constants.NETWORK_UDP_CLIENT_PORT);

        InitReceiverThread(m_server, ip, OnDataReceived);
    }

//---------------------------------------------------------------------------------------------------------------------

    void OnDataReceived(byte[] data) {
        string message = Encoding.UTF8.GetString(data);

        Debug.Log(message);
    }

//---------------------------------------------------------------------------------------------------------------------

    public void Send(string message, IPEndPoint endpoint) {
        byte[] data= Encoding.ASCII.GetBytes(message);
        m_server.Send(data, data.Length, endpoint);
    }
}