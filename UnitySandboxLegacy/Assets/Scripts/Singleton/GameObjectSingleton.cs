using UnityEngine;
using System.Collections;

public class GameObjectSingleton : MonoBehaviour {

    private static GameObjectSingleton m_instance;
    uint m_uintData = 0;    
    int  m_intData  = 0;

    public static GameObjectSingleton GetInstance() {
        if(null == m_instance) {
            m_instance = GameObject.FindFirstObjectOfType<GameObjectSingleton>();          

            //Create automatically
            if (null==m_instance) {
                GameObject gameObj = new GameObject();
                gameObj.name = "GameObjectSingleton";
                m_instance = gameObj.AddComponent<GameObjectSingleton>();

            }
            Object.DontDestroyOnLoad(m_instance.gameObject);
        }
        
        return m_instance;

    }

    void Awake() {
        if(null==m_instance) {
            m_instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Update() {
        ++m_intData;
        ++m_uintData;
    }

    public uint GetUintData() {
        return m_uintData;
    }

    public int GetIntData() {
        return m_intData;
    }
}
