using UnityEngine;
using System.Collections;


public class SingletonTest : MonoBehaviour {

    private class SimpleGenericSingletonClass : SimpleGenericSingleton<SimpleGenericSingletonClass>{
        uint m_uintData = 0;        
        int  m_intData = 0;
        
        public void Update() {
            ++m_uintData;
            ++m_intData;
        }
        
        public int GetIntData() {
            return m_intData;
        }

        public uint GetUintData() {
            return m_uintData;
        }
    }

    int m_localIntData = 0;
    uint  m_localUintData = 0;

    
    void Awake() {
        Debug.Log("SingletonTest Awake() ");
        LogData();

    }

	// Use this for initialization
	void Start () {
        Debug.Log("SingletonTest Start()");
        LogData();
	}
	
    void LogData() {
        Debug.Log(string.Format("SimpleGenericSingletonClass Data : {0}, {1}", 
                  SimpleGenericSingletonClass.GetInstance().GetIntData(),
                  SimpleGenericSingletonClass.GetInstance().GetUintData()
        ));   
        Debug.Log(string.Format("GameObjectSingleton Data : : {0}, {1}", 
                  GameObjectSingleton.GetInstance().GetIntData(),
                  GameObjectSingleton.GetInstance().GetUintData()
        ));   
    }

	// Update is called once per frame
	void Update () {
        SimpleGenericSingletonClass.GetInstance().Update();
        ++m_localIntData;
        ++m_localUintData;
	}

    void OnGUI() {
        GUI.Label (new Rect (10,0,Screen.width,30), "Try to recompile the script");
        GUI.Box (new Rect (10,30,Screen.width,30), 
                 string.Format("SimpleGenericsSingletonClass Data. Int: {0}. Uint: {1}",
                    SimpleGenericSingletonClass.GetInstance().GetIntData(),
                    SimpleGenericSingletonClass.GetInstance().GetUintData()
                )
        );
        GUI.Box (new Rect (10,60,Screen.width,30), 
                 string.Format("GameObjectSingleton Data. Int: {0}. Uint: {1}",
                 GameObjectSingleton.GetInstance().GetIntData(),
                 GameObjectSingleton.GetInstance().GetUintData()
                )
        );
        GUI.Box (new Rect (10,90,Screen.width,30), 
                 string.Format("Local Data. Int: {0}. Uint: {1}",
                      m_localIntData,
                      m_localUintData
                      )
                 );

    }



}
