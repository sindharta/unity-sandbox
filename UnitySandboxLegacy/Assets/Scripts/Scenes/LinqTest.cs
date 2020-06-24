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

    //
    public static int IntFirstOrDefault(this List<int> List, int element)  {
        var enumerator = List.GetEnumerator();
        while (enumerator.MoveNext()) {
            if (enumerator.Current == element) {
                return enumerator.Current;
            }
        }
        return default(int);
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

    bool IsElementEqual(int element) {
        return element == m_searchElement;
    }
        
	// Update is called once per frames
	void Update () {
        
        for (int i=0;i<m_loop;++i) {
            UnityEngine.Profiling.Profiler.BeginSample("Simple"); 
            var enumerator = m_list.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (enumerator.Current  == m_searchElement) {
                    break;
                }
            }
            UnityEngine.Profiling.Profiler.EndSample();
            
            UnityEngine.Profiling.Profiler.BeginSample("LinqConstant");
            m_list.FirstOrDefault((e) => { return e == 3; } );
            UnityEngine.Profiling.Profiler.EndSample();
            
            UnityEngine.Profiling.Profiler.BeginSample("LinqParameter"); 
            m_list.FirstOrDefault((e) => { return e == m_searchElement; } );
            UnityEngine.Profiling.Profiler.EndSample();

            UnityEngine.Profiling.Profiler.BeginSample("LinqWithPredefinedFunc");
            m_list.FirstOrDefault( IsElementEqual );
            UnityEngine.Profiling.Profiler.EndSample();

            UnityEngine.Profiling.Profiler.BeginSample("LambdaParameter");
            m_list.MyFirstOrDefault((e) => { return e == m_searchElement; } );
            UnityEngine.Profiling.Profiler.EndSample();
            
            UnityEngine.Profiling.Profiler.BeginSample("LambdaWithPredefinedFunc");
            m_list.MyFirstOrDefault(IsElementEqual);
            UnityEngine.Profiling.Profiler.EndSample();

            UnityEngine.Profiling.Profiler.BeginSample("LambdaSimple");
            int _x = 1;
            System.Action x = () => { _x = m_searchElement; }; x();
            UnityEngine.Profiling.Profiler.EndSample(); 

            UnityEngine.Profiling.Profiler.BeginSample("CustomFunc");
            m_list.IntFirstOrDefault(m_searchElement);
            UnityEngine.Profiling.Profiler.EndSample();
        }
        
	}
    
}
