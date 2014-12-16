using UnityEngine;

public class TcpTest : MonoBehaviour {

    [SerializeField]
    TcpClientController m_client;

    [SerializeField]
    TcpServerController m_server;

//---------------------------------------------------------------------------------------------------------------------

    string m_message = "";

//---------------------------------------------------------------------------------------------------------------------

    void OnGUI() {
        //Client side
        GUI.Label(new Rect(10, 10, 150, 30), "Message: ");

        string send_control_name = "SendToServer";

        GUI.SetNextControlName(send_control_name);
        m_message = GUI.TextField(new Rect(10, 30, 100, 30), m_message);
        Event e = Event.current;
        if (GUI.Button(new Rect(115, 30, 100, 30), "Send to server")
            || e.IsKeyPressed(KeyCode.Return, send_control_name)) {
            m_client.SendToServer(m_message);
            m_message = "";
        }

        //server side
        GUI.Label(new Rect(10, 100, 300, 100), "Message received in the server: ");
        GUI.Label(new Rect(10, 120, 300, 100), m_server.GetClientMessage());



    }

}
