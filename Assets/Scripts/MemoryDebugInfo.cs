using UnityEngine;
using System.Collections;
using System.Text;

/*
Taken and refined from:
From: http://wiki.unity3d.com/index.php/AllocationStats
1. "Currently allocated" Shows the total memory allocated according to the GC.
2. "Peak allocated" Shows the max memory allocated during the application run with the amount of memory which was 
    allocated when GC was called last time inside parentheses.
3. "Allocation rate" Shows how fast the application is allocating memory in mb per eh, 0.3 seconds. 
4. "Collection frequency" Shows how far apart the GC collections are spaced in seconds.
5. "Last collect delta" Shows how high the framerate was when GC was last called, 
    GC calls usually make the framerate drop.
*/

[ExecuteInEditMode()]
public class MemoryDebugInfo: MonoBehaviour {

    [SerializeField]
    bool m_show = true;
    [SerializeField]
    bool m_showFPS = false;
    [SerializeField]
    float m_updateFrequency = 0.3f;

    [SerializeField]
    float m_targetMaxMemory; //bytes
    [SerializeField]
    Texture m_warningTexture;

    //Profiler features (Unity Pro feature)
    [SerializeField]
    bool m_useProfiler = false;
    [SerializeField]
    int m_targetMaxMeshMemory;
    [SerializeField]
    int m_targetMaxTextureMemory;
    [SerializeField]
    int m_targetMaxAudioMemory;
    [SerializeField]
    int m_targetMaxAnimationMemory;
    [SerializeField]
    int m_targetMaxMaterialMemory;


//---------------------------------------------------------------------------------------------------------------------

    private float m_lastUpdateTime = float.MinValue;

    private float m_lastCollectTime = 0;
    private float m_lastcollectNum = 0;
    private float m_delta = 0;
    private float m_lastDeltaTime = 0;
    private int m_allocRate = 0;
    private int m_allocMem = 0;
    private int m_collectAlloc = 0;
    private int m_peakAlloc = 0;

//---------------------------------------------------------------------------------------------------------------------


    void Awake() {
        useGUILayout = false;
    }

//---------------------------------------------------------------------------------------------------------------------

    void Update() {
        if (!m_show) {
            return;
        }



        //We want to know the exact frame rate when the garbage collection happened, so need to do this every frame
        int collCount = System.GC.CollectionCount(0);

        //[TODO-Sin:2014-11-19]  until this part
        if (m_lastcollectNum != collCount) {

            m_lastcollectNum = collCount;


            m_delta = Time.realtimeSinceStartup - m_lastCollectTime;

            m_lastCollectTime = Time.realtimeSinceStartup;

            m_lastDeltaTime = Time.deltaTime;

            m_collectAlloc = m_allocMem;
        }


        if (Time.realtimeSinceStartup - m_lastUpdateTime > m_updateFrequency) {
            int prev_alloc_memory = m_allocMem;

            m_allocMem = (int)System.GC.GetTotalMemory(false);
            m_peakAlloc = Mathf.Max(m_allocMem, m_peakAlloc);

            int diff = m_allocMem - prev_alloc_memory;
            m_allocRate = diff;

            m_lastUpdateTime = Time.realtimeSinceStartup;
        }

    }

//---------------------------------------------------------------------------------------------------------------------
    void OnGUI() {
        if (!m_show) {
            return;
        }
        StringBuilder text = new StringBuilder();

        text.Append("Currently allocated			");
        text.Append((m_allocMem / 1000000F).ToString("0"));
        text.Append("mb\n");

        text.Append("Peak allocated				");
        text.Append((m_peakAlloc / 1000000F).ToString("0"));
        text.Append("mb (last	collect ");
        text.Append((m_collectAlloc / 1000000F).ToString("0"));
        text.Append(" mb)\n");


        text.Append("Allocation rate				");
        text.Append((m_allocRate / 1000000F).ToString("0.0"));
        text.Append("mb\n");

        text.Append("Collection frequency		");
        text.Append(m_delta.ToString("0.00"));
        text.Append("s\n");

        text.Append("Last collect m_delta			");
        text.Append(m_lastDeltaTime.ToString("0.000"));
        text.Append("s (");
        text.Append((1F / m_lastDeltaTime).ToString("0.0"));

        text.Append(" fps)");

        if (m_showFPS) {
            text.Append("\n" + (1F / Time.deltaTime).ToString("0.0") + " fps");
        }

        GUI.Box(new Rect(5, 5, 310, 80 + (m_showFPS ? 16 : 0)), "");
        GUI.Label(new Rect(10, 5, 1000, 200), text.ToString());
    }

//---------------------------------------------------------------------------------------------------------------------
 
}