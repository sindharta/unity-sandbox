using System.Net.Sockets;
using System.Net;
using System;

public class TcpListenerThread {

    TcpListener m_tcpListener = null;

    bool m_stopRequested = false;

    public Action<TcpClient> ClientAccepted = null;


//---------------------------------------------------------------------------------------------------------------------
    public TcpListenerThread(IPAddress ipAddress, int port) {
        m_tcpListener = new TcpListener(ipAddress, port);
    }

//---------------------------------------------------------------------------------------------------------------------

    public void StartListen() {
        m_tcpListener.Start();
        while (!m_stopRequested) {
            TcpClient client = m_tcpListener.AcceptTcpClient();
            if (null!=ClientAccepted) {
                ClientAccepted(client);
            }

        }
    }

//---------------------------------------------------------------------------------------------------------------------

    public void RequestStop() {
        m_tcpListener.Stop();
        m_stopRequested = true;
    }


}

