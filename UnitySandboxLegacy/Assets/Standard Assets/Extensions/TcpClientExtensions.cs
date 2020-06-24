using System.Net.Sockets;

public static class TcpClientExtensions {


    // Detect if client disconnected
    //https://social.msdn.microsoft.com/Forums/en-US/c857cad5-2eb6-4b6c-b0b5-7f4ce320c5cd/c-how-to-determine-if-a-tcpclient-has-been-disconnected?forum=netfxnetcom
    public static bool IsDisconnected(this TcpClient tcpClient) {
        if (tcpClient.Client.Poll(0, SelectMode.SelectRead)) {
            byte[] buff = new byte[1];
            if (tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0) {
                return true;
            }
        }

        return false;
    }


}
