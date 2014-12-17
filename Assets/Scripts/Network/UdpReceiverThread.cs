using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Net;

public class UdpReceiverThread {
    UdpClient m_client = null;
    IPEndPoint m_sourceIp;
    bool m_stopRequested = false;

    public Action<byte[]> DataReceived = null;

//---------------------------------------------------------------------------------------------------------------------

    public UdpReceiverThread(UdpClient client, IPEndPoint sourceIp) {
        m_client = client;
    }

//---------------------------------------------------------------------------------------------------------------------

    public void StartReceive() {
        while (!m_stopRequested) {
            byte[] data = m_client.Receive(ref m_sourceIp);
            if (null != DataReceived) {
                DataReceived(data);
            }
        }
    }

//---------------------------------------------------------------------------------------------------------------------

    public void RequestStop() {
        m_client.Close();
        m_stopRequested = true;
    }
}