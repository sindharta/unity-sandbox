using UnityEngine;
using System.Collections;

public class SimpleGenericSingleton<T>  where T: class, new() {

    static T m_instance = null;

    public static T GetInstance() {
        if (null==m_instance) {
            m_instance = new T();
        }

        return m_instance;
    }

}
