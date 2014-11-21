using UnityEngine;
using System.Text;

/*
Taken and refined from:
From: http://wiki.unity3d.com/index.php/AllocationStats
1. "Currently allocated" Shows the total memory allocated according to the GC.
2. "Peak allocated" Shows the max memory allocated during the application run with the amount of memory which was 
    allocated when GC was called last time inside parentheses.
3. "Allocation rate" Shows how fast the application is allocating memory in mb per eh, 0.3 seconds. 
4. "Collection frequency" Shows how far apart the GC collections are spaced in seconds.
5. "Delta time dur. collection" Shows how high the framerate was when GC was last called, 
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
    float m_maxMem; //Mega Bytes
    [SerializeField]
    Texture m_warningTexture;

    //Profiler features (Unity Pro feature)
    [SerializeField]
    bool m_useProfiler = false;
    [SerializeField]
    int m_maxMeshMem;  //Mega Bytes
    [SerializeField]
    int m_maxTexMem;//Mega Bytes
    [SerializeField]
    int m_maxAudioMem;//Mega Bytes
    [SerializeField]
    int m_maxAnimMem;//Mega Bytes
    [SerializeField]
    int m_maxMatMem;//Mega Bytes


//---------------------------------------------------------------------------------------------------------------------

    float m_lastUpdateTime = float.MinValue;

    //GC
    float m_lastCollectTime = 0;
    int[] m_lastCollectCount;
    float m_collectionFrequency = 0;

    float m_lastDeltaTime         = 0;
    int m_allocRate               = 0;
    int m_allocMem                = 0;
    int m_allocMemAfterCollection = 0;
    int m_peakAlloc               = 0;

    //Profiler
    int m_allocMeshMem      = 0;
    int m_allocTexMem       = 0;
    int m_allocAudioMem     = 0;
    int m_allocAnimMem      = 0;
    int m_allocMaterialMem  = 0;

    const float TO_BYTE = 1024f * 1024f;

//---------------------------------------------------------------------------------------------------------------------


    void Awake() {
        useGUILayout = false;
        m_lastCollectCount = new int[System.GC.MaxGeneration];
        for (int i=0; i < System.GC.MaxGeneration; ++i) {
            m_lastCollectCount[i] = 0;
        }
    }


//---------------------------------------------------------------------------------------------------------------------

    void Update() {
        if (!m_show) {
            return;
        }

        //We want to know the exact frame rate when the garbage collection happened, so need to do this every frame
        bool updated = UpdateGCCollectionCount();
        if (updated) {
            m_collectionFrequency = Time.realtimeSinceStartup - m_lastCollectTime;
            m_lastCollectTime = Time.realtimeSinceStartup;
            m_lastDeltaTime = Time.deltaTime;
            m_allocMemAfterCollection = m_allocMem;
        }

        if (Time.realtimeSinceStartup - m_lastUpdateTime <= m_updateFrequency) 
            return;

        //Start updating info
        int prev_alloc_memory = m_allocMem;

        m_allocMem = (int)System.GC.GetTotalMemory(false);
        m_peakAlloc = Mathf.Max(m_allocMem, m_peakAlloc);

        int diff = m_allocMem - prev_alloc_memory;
        m_allocRate = diff;

        m_lastUpdateTime = Time.realtimeSinceStartup;


#if ENABLE_PROFILER
        if (!m_useProfiler)
            return;
        m_allocTexMem = GetRuntimeMemorySize(typeof(Texture));
        m_allocMeshMem = GetRuntimeMemorySize(typeof(Mesh));
        m_allocAudioMem = GetRuntimeMemorySize(typeof(AudioClip));
        m_allocAnimMem = GetRuntimeMemorySize(typeof(Animation));
        m_allocMaterialMem = GetRuntimeMemorySize(typeof(Material));
#endif
    }

//---------------------------------------------------------------------------------------------------------------------

    bool IsMemoryOverused() {
        if (m_allocMem > m_maxMem * TO_BYTE)
            return true;

#if ENABLE_PROFILER
        if (!m_useProfiler)
            return false;

        if (m_allocMeshMem > m_maxMeshMem * TO_BYTE)
            return true;
        if (m_allocTexMem > m_maxTexMem * TO_BYTE)
            return true;
        if (m_allocMaterialMem > m_maxMatMem * TO_BYTE)
            return true;
        if (m_allocAudioMem > m_maxAudioMem * TO_BYTE)
            return true;
        if (m_allocAnimMem > m_maxAnimMem * TO_BYTE)
            return true;
#endif

        return false;
    }

//---------------------------------------------------------------------------------------------------------------------

    bool UpdateGCCollectionCount() {

        bool updated = false;
        // Display collection counts.
        for (int i = 0; i < System.GC.MaxGeneration; ++i) {
            int count = System.GC.CollectionCount(i);
            if (count!= m_lastCollectCount[i]) {
                updated = true;
                m_lastCollectCount[i] = count;
            }
        }

        return updated;
    }

//---------------------------------------------------------------------------------------------------------------------
    void OnGUI() {

        if (!m_show) {
            return;
        }

        if (IsMemoryOverused()) {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_warningTexture);
        }

        StringBuilder text = new StringBuilder();
        text.AppendStrings("Currently allocated			", ToMB(m_allocMem), "MB. Max: ", m_maxMem.ToString(), "MB\n");
        text.AppendStrings("Peak allocated:				", ToMB(m_peakAlloc), "MB ( Allocated ");
        text.AppendStrings("after collection: ", ToMB(m_allocMemAfterCollection), " MB)\n");
        text.AppendStrings("Allocation rate				", ToMB(m_allocRate), "MB\n");
        text.AppendStrings("Collection frequency		", m_collectionFrequency.ToString("0.00"), "s\n");                           
        text.AppendStrings("Delta time dur. collection  ", m_lastDeltaTime.ToString("0.000"));
        text.AppendStrings("s (", (1F / m_lastDeltaTime).ToString("0.0"), " fps)\n");

        if (m_showFPS) {
            text.AppendStrings((1F / Time.deltaTime).ToString("0.0") , " fps\n");
        }

        //profiler
        if (m_useProfiler) 
        {
            text.AppendStrings("Mesh:		", ToMB(m_allocMeshMem), "MB. Max: ", m_maxMeshMem.ToString(),"MB\n");
            text.AppendStrings("Texture		", ToMB(m_allocTexMem), "MB. Max: ", m_maxTexMem.ToString(), "MB\n");
            text.AppendStrings("AudioClip:	", ToMB(m_allocAudioMem), "MB. Max: ", m_maxAudioMem.ToString(), "MB\n");
            text.AppendStrings("Animation:	", ToMB(m_allocAnimMem), "MB. Max: ", m_maxAnimMem.ToString(), "MB\n");
            text.AppendStrings("Material:	", ToMB(m_allocMaterialMem), "MB. Max: ", m_maxMatMem.ToString(), "MB\n");
        }

        GUI.Box(new Rect(5, 5, 500, 160 + (m_showFPS ? 16 : 0)), "");
        GUI.Label(new Rect(10, 5, 1000, 200), text.ToString());
    }

//---------------------------------------------------------------------------------------------------------------------

    static string ToMB(int bytes) {
        return (bytes / TO_BYTE).ToString("0");
    }

//---------------------------------------------------------------------------------------------------------------------

    static int GetRuntimeMemorySize(System.Type type) {
        int ret = 0;
        Object[] objects = Resources.FindObjectsOfTypeAll(type);
        for (int i=0; i < objects.Length; ++i) {
            ret += Profiler.GetRuntimeMemorySize(objects[i]);
        }
        return ret;
    }
//---------------------------------------------------------------------------------------------------------------------
 
}