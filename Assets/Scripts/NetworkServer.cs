using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class NetworkServer {

    TcpListener m_tcpServer = null;

    bool m_stopRequested = false;

    public Action<TcpClient> ClientAccepted = null;


//---------------------------------------------------------------------------------------------------------------------
    public void StartServer() {
        IPAddress local_addr = IPAddress.Parse("127.0.0.1");
        m_tcpServer = new TcpListener(local_addr, Constants.NETWORK_PORT);
        m_tcpServer.Start();
    }

//---------------------------------------------------------------------------------------------------------------------

    public void StartListen() {
        while (!m_stopRequested) {
            TcpClient client = m_tcpServer.AcceptTcpClient();
            if (null!=ClientAccepted) {
                ClientAccepted(client);
            }

        }
    }

//---------------------------------------------------------------------------------------------------------------------

    public void RequestStop() {
        m_tcpServer.Stop();
        m_stopRequested = true;
    }


}

