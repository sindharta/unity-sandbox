using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

abstract class UdpBaseController : MonoBehaviour
{
    UdpReceiverThread m_receiverThread;

//---------------------------------------------------------------------------------------------------------------------

    protected void InitReceiverThread(UdpClient udpClient, IPEndPoint ip, Action<byte[]> dataReceivedCallback)
    {
        m_receiverThread = new UdpReceiverThread(udpClient, ip);
        m_receiverThread.DataReceived = dataReceivedCallback;

        Thread thread = new Thread(new ThreadStart(m_receiverThread.StartReceive));
        thread.Start();
    }

//---------------------------------------------------------------------------------------------------------------------

    void OnDestroy() {
        m_receiverThread.RequestStop();
    }

//---------------------------------------------------------------------------------------------------------------------


}