#pragma warning disable 0219 // variable assigned but not used.

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions {
    //
    public static T MyFirstOrDefault<T>(this List<T> List, System.Func<T,bool> func) {
        var enumerator = List.GetEnumerator();
        while (enumerator.MoveNext()) {
            if (func(enumerator.Current)) {
                return enumerator.Current;
            }
        }                
        return default(T);
    }
    
}

public class LinqTest : MonoBehaviour {
    
    List<int> m_list;

    [SerializeField]
    int m_loop = 100;
        
    [SerializeField]
    int m_searchElement = 3;
    
  
        
    void Awake() {
        m_list = new List<int>() { 1,2,3,4,5,6,7,8,9,10};
    }
        
	// Update is called once per frames
	void Update () {
        
        for (int i=0;i<m_loop;++i) {
            Profiler.BeginSample("Simple"); 
            var enumerator = m_list.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (enumerator.Current  == m_searchElement) {
                    break;
                }
            }
            Profiler.EndSample();
            
            Profiler.BeginSample("LinqConstant");
            m_list.FirstOrDefault((e) => { return e == 3; } );
            Profiler.EndSample();
            
            Profiler.BeginSample("LinqParameter"); 
            m_list.FirstOrDefault((e) => { return e == m_searchElement; } );
            Profiler.EndSample();

            Profiler.BeginSample("LambdaCallFunc"); 
            m_list.MyFirstOrDefault((e) => { return e == m_searchElement; } );
            Profiler.EndSample();
            
            Profiler.BeginSample("LambdaParameter");
            int _x = 1;
            System.Action x = () => { _x = m_searchElement; }; x();
            Profiler.EndSample(); 
        }
        
	}
    
}
