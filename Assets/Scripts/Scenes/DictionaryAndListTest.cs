#pragma warning disable 0219 // variable assigned but not used.
using System.Collections.Generic;
using GameData;
using UnityEngine;

public class DictionaryAndListTest : MonoBehaviour {

    
    List<HeroData> m_dataList;
    Dictionary<uint, HeroData> m_dataUintDictionary;
    Dictionary<string, HeroData> m_dataStringDictionary;
    
    const uint NUM_DATA = 10000;
    
    [SerializeField]
    int m_numSearchesPerUpdate = 1;
    
    
	// Use this for initialization
	void Start () {
        int num_data = (int)(NUM_DATA);
        m_dataList = new List<HeroData>(num_data);
        m_dataUintDictionary = new Dictionary<uint, HeroData>(num_data);
        m_dataStringDictionary = new Dictionary<string, HeroData>(num_data);
        
        
        for (uint i=0;i<num_data;++i) {
            HeroData new_data = CreateRandomHeroData(i);
            m_dataList.Add(new_data);
            m_dataUintDictionary[i] = new_data;
            m_dataStringDictionary[i.ToString()] = new_data;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        
        for (int i=0;i<m_numSearchesPerUpdate;++i) {
            uint id = (uint) Random.Range (0,(int)NUM_DATA);
            UnityEngine.Profiling.Profiler.BeginSample("List");
            HeroData listData = GetDataFromList(id);
            UnityEngine.Profiling.Profiler.EndSample();
            
            UnityEngine.Profiling.Profiler.BeginSample("Int Dictionary");
            HeroData intDicData = m_dataUintDictionary[id];
            UnityEngine.Profiling.Profiler.EndSample();

            string idStr = id.ToString();
            UnityEngine.Profiling.Profiler.BeginSample("String Dictionary");
            HeroData stringDicData = m_dataStringDictionary[idStr];
            UnityEngine.Profiling.Profiler.EndSample();
        }
        
	
	}
    
    void OnGUI() {
        
        GUI.Label(new Rect(10, 10, 100, 100), string.Format("Num Data: {0}",m_numSearchesPerUpdate));
        if (GUI.Button(new Rect(110, 10, 100, 100),"-")) {
            m_numSearchesPerUpdate = Mathf.Max(1,m_numSearchesPerUpdate-1);
        }
        if (GUI.Button(new Rect(210, 10, 100, 100),"+")) {
            m_numSearchesPerUpdate = Mathf.Min(m_numSearchesPerUpdate+1,int.MaxValue);           
        }
        
    }
    
    HeroData CreateRandomHeroData(uint id) {
        HeroData new_data = new HeroData();
        new_data.ID = id;
        new_data.HP = (uint)Random.Range(100,1000);
        new_data.MP = (uint)Random.Range(100,1000);
        new_data.Name = string.Format("{0}_{1}",new_data.HP, new_data.MP);
        return new_data;        
    }
    
    HeroData GetDataFromList(uint id) {
        HeroData ret = null;
        int i = 0;
        int data_count = m_dataList.Count;
        while (null==ret && i < data_count) {
            if (m_dataList[i].ID == id) {
                ret = m_dataList[i];
            }
            ++i;
        }
        return ret;
    }
    
    
}
