using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System;
using System.Net;

public class TcpServerController : MonoBehaviour {

    const int MAX_CONNECTIONS = 32;

    TcpListenerThread m_listenerThread = null;
    LinkedList<TcpClient> m_tcpClients = new LinkedList<TcpClient>();
    List<TcpClient> m_disconnectedClients = new List<TcpClient>();
    byte[] m_receiveBuffer = null;
    string m_clientMessage;

//---------------------------------------------------------------------------------------------------------------------

    // Use this for initialization
    void Start () {
        m_listenerThread = new TcpListenerThread(IPAddress.Loopback, Constants.NETWORK_PORT);
        m_listenerThread.ClientAccepted = OnClientAccepted;

        Thread thread = new Thread(new ThreadStart(m_listenerThread.StartListen));
        thread.Start();
    }

//---------------------------------------------------------------------------------------------------------------------
    void Update() {
        var enumerator = m_tcpClients.GetEnumerator();
        while (enumerator.MoveNext()) {
            TcpClient curTcpClient = enumerator.Current;

            //is the client still connected
            if (curTcpClient.IsDisconnected()) {
                m_disconnectedClients.Add(curTcpClient);
                continue;
            }

            NetworkStream stream = curTcpClient.GetStream();
            if (stream.CanRead && stream.DataAvailable ) {

                Array.Clear(m_receiveBuffer, 0, m_receiveBuffer.Length);
                stream.Read(m_receiveBuffer, 0, (int) curTcpClient.ReceiveBufferSize);
                m_clientMessage = Encoding.UTF8.GetString(m_receiveBuffer);

                Debug.Log("The client says this: " + m_clientMessage);

            }
        }

        //Process the disconnected clients
        var disconnectedEnumerator = m_disconnectedClients.GetEnumerator();
        while (disconnectedEnumerator.MoveNext()) {
            Debug.Log("Server disconnected a client");
            m_tcpClients.Remove(disconnectedEnumerator.Current);
        }
        m_disconnectedClients.Clear();
    }


//---------------------------------------------------------------------------------------------------------------------

    void OnDestroy() {
        m_listenerThread.RequestStop();
    }

//---------------------------------------------------------------------------------------------------------------------

    void OnClientAccepted(TcpClient client) {
        Debug.Log("Server says: Client connected to server");

        //error check
        if (!client.GetStream().CanRead) {
            Debug.LogError("Can't read from the connected client");
            return;
        }

        if (m_receiveBuffer == null || m_receiveBuffer.Length < client.ReceiveBufferSize) {
            m_receiveBuffer = new byte[client.ReceiveBufferSize];
        }

        m_tcpClients.AddLast(client);
    }

//---------------------------------------------------------------------------------------------------------------------

    public string GetClientMessage() {
        return m_clientMessage;
    }


}
