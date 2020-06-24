using UnityEngine;
using System.Collections.Generic;
using System;

public class SerializationCallbackTest : MonoBehaviour, ISerializationCallbackReceiver {
    public List<int> m_keys = new List<int>();
    public List<string> m_values = new List<string>();
    
    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<int,string>  m_dictionary = new Dictionary<int,string>();
    
//-------------------------------------------------------------------------------------------------
    void Awake() {
        //Initial Data
        m_dictionary.Add(0, "Data0");
        m_dictionary.Add(1, "Data1");
        m_dictionary.Add(2, "Data2");
        m_dictionary.Add(3, "Data3");           
    }
   
//-------------------------------------------------------------------------------------------------
    //
    public void OnBeforeSerialize() {
        m_keys.Clear();
        m_values.Clear();
        var enumerator = m_dictionary.GetEnumerator();
        while (enumerator.MoveNext()) {
            var cur_data = enumerator.Current;
            m_keys.Add(cur_data.Key);
            m_values.Add(cur_data.Value);
        }
    }

//-------------------------------------------------------------------------------------------------

    //
    public void OnAfterDeserialize() {
        m_dictionary.Clear();
        int count = Math.Min(m_keys.Count,m_values.Count);
        for (int i=0; i< count; ++i) {
            m_dictionary.Add(m_keys[i],m_values[i]);
        }
    }
    
}