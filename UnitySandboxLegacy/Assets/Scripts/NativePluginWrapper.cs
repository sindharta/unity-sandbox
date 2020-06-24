using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class NativePluginWrapper  : MonoBehaviour{

    
//---------------------------------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start() {
        m_dataCallbackDelegate = new DataCallback(DataFunc);
        IntPtr dataFP = Marshal.GetFunctionPointerForDelegate(m_dataCallbackDelegate );
        SetDataCallback(dataFP);

        m_logCallbackDelegate = new LogCallback(LogFunc);
        IntPtr logFP = Marshal.GetFunctionPointerForDelegate(m_logCallbackDelegate );
        SetLogCallback(logFP);

    }

//---------------------------------------------------------------------------------------------------------------------

    void Update() {
        Execute();
    }

//---------------------------------------------------------------------------------------------------------------------

    static void DataFunc( int[] data) {
        int CHANGED_BY_MANAGED_CODE = 1000;
        if (CHANGED_BY_MANAGED_CODE == data[0]) {
            Debug.LogWarning("Data passed from native pluging was changed by managed code");
        }

        //StringBuilder sb = new StringBuilder();
        //for (int i=0;i<DATA_LENGTH;++i) {
        //    sb.Append(data[i].ToString());
        //    sb.Append(",");
        //}
        //Debug.Log(sb.ToString());

        data[0] = CHANGED_BY_MANAGED_CODE; 
    }

//---------------------------------------------------------------------------------------------------------------------

    static void LogFunc(string log) {
        Debug.Log(log);
    }

//---------------------------------------------------------------------------------------------------------------------
    const int DATA_LENGTH = 10;

    DataCallback m_dataCallbackDelegate = null;
    LogCallback  m_logCallbackDelegate = null;

//---------------------------------------------------------------------------------------------------------------------

    public delegate void LogCallback([In][MarshalAs(UnmanagedType.LPStr)] string log);
    public delegate void DataCallback([MarshalAs(UnmanagedType.LPArray, SizeConst=DATA_LENGTH)]  int[] log);

    [DllImport("NativePlugin.dll", CallingConvention=CallingConvention.StdCall)]
    public static extern void SetLogCallback(IntPtr fp);

    [DllImport("NativePlugin.dll", CallingConvention=CallingConvention.StdCall)]
    public static extern void SetDataCallback(IntPtr fp);

    [DllImport("NativePlugin.dll", CallingConvention=CallingConvention.StdCall)]
    public static extern void Execute();


}
