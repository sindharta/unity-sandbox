using UnityEngine;
using System.Collections;

public class GUILogManager : MonoBehaviour {
    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        m_logQueue.Enqueue("[" + type + "] : " + logString);
        if (type == LogType.Exception)
            m_logQueue.Enqueue(stackTrace);
        while (m_logQueue.Count > MAX_LOG_SIZE)
            m_logQueue.Dequeue();
    }

    void OnGUI() {
        GUILayout.BeginArea(new Rect(Screen.width - (Screen.width * 0.5f), 0, Screen.height * 0.5f, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", m_logQueue.ToArray()));
        GUILayout.EndArea();
    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    const    uint  MAX_LOG_SIZE = 40; // number of messages to keep
    readonly Queue m_logQueue   = new Queue();
    
}