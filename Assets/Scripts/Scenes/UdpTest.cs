using UnityEngine;
using System.Collections;
using System.Net;

public class UdpTest : MonoBehaviour {

    [SerializeField]
    UdpServerController m_udpServer;

    [SerializeField]
    UdpClientController m_udpClient;

//---------------------------------------------------------------------------------------------------------------------


    void OnGUI() {
        GUI.Label(new Rect(10, 10, 150, 30), "Message: ");

        string send_control_name = "SendToServer";

        GUI.SetNextControlName(send_control_name);
        if (GUI.Button(new Rect(115, 30, 100, 30), "Server to client")) {
            m_udpServer.Send("To client", new IPEndPoint(IPAddress.Loopback, Constants.NETWORK_UDP_CLIENT_PORT));
        }

        if (GUI.Button(new Rect(115, 70, 100, 30), "Client to server")) {
            m_udpClient.Send("To server");
        }


    }

}
