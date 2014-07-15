#pragma warning disable 0219 // variable assigned but not used.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class StringTest : MonoBehaviour {
	
	const uint MAX_DATA_COUNT = 100;
    const uint NUM_ITERATION = 1000;

	float[] m_concatStringTime;
	float[] m_stringFormatTime;
	float[] m_stringBuilderTime;

    uint[] m_concatStringMemory;
    uint[] m_stringFormatMemory;
    uint[] m_stringBuilderMemory;

	string[] m_data;

	#region Unity functions
	void Awake() {
		m_concatStringTime = new float[MAX_DATA_COUNT];
		m_stringFormatTime = new float[MAX_DATA_COUNT];
		m_stringBuilderTime = new float[MAX_DATA_COUNT];

        m_concatStringMemory = new uint[MAX_DATA_COUNT];
        m_stringFormatMemory = new uint[MAX_DATA_COUNT];
        m_stringBuilderMemory = new uint[MAX_DATA_COUNT];

		//prepare the data, use 1 digit of integers to compare more easily
		m_data = new string[MAX_DATA_COUNT];
		for (uint i=0;i<MAX_DATA_COUNT;++i) {
			int random_number = UnityEngine.Random.Range(0,10);
			m_data[i] = random_number.ToString();
		}
	}

    void Start() {
		StartCoroutine(DoTest());
	}
	#endregion

	IEnumerator DoTest() {
		System.Text.StringBuilder format = new System.Text.StringBuilder("");	

		for (uint i=0;i<MAX_DATA_COUNT;++i) {
			Debug.Log (string.Format ("Test for {0} data ", i ));

            //concat
            ProfileTest( () => {
                ConcatString (i);
            }, out m_concatStringTime[i], out m_concatStringMemory[i]);

			//string format
			format.Append('{');
			format.Append(i);
			format.Append('}');
			string cur_format = format.ToString();

            ProfileTest( () => {
                StringFormat(cur_format, null);
            }, out m_stringFormatTime[i], out m_stringFormatMemory[i]);


			//string builder
            ProfileTest( () => {
                StringBuilder(i);
            }, out m_stringBuilderTime[i], out m_stringBuilderMemory[i]);
            
			yield return null;
		}

        //outputting results
        const string filename = "Output/StringTestResult.txt";
        using (StreamWriter file = new StreamWriter(filename)) {
            
            file.WriteLine("TIME");
            file.WriteLine("Concat, StringFormat, StringBuilder");
            for (uint i=0;i<MAX_DATA_COUNT;++i) {
                string line = string.Format("{0}, {1}, {2}",
                                            m_concatStringTime[i],
                                            m_stringFormatTime[i],
                                            m_stringBuilderTime[i]
                                            );
                file.WriteLine(line);
            }

            file.WriteLine("MEMORY");
            file.WriteLine("Concat, StringFormat, StringBuilder");
            for (uint i=0;i<MAX_DATA_COUNT;++i) {
                string line = string.Format("{0}, {1}, {2}",
                                            m_concatStringMemory[i],
                                            m_stringFormatMemory[i],
                                            m_stringBuilderMemory[i]
                                            );
                file.WriteLine(line);
            }

            file.Close();
        }
        Debug.Log ("StringTest results written in " + filename);

        yield break;
	}

    void ProfileTest(System.Action testFunc, out float timeResult, 
                     out uint memResult) 
    {
        GC.Collect();
        float time_start = Time.realtimeSinceStartup;
        uint mem_start = Profiler.GetMonoUsedSize();
        testFunc ();
        float time_end = Time.realtimeSinceStartup;
        uint mem_end  = Profiler.GetMonoUsedSize();
        timeResult = time_end - time_start;
        memResult = 0;
        if (mem_end > mem_start) { //validity check
            memResult  = mem_end - mem_start;
        }
    }


	void ConcatString(uint numData) {
        for (uint j=0;j<NUM_ITERATION;++j) {
            string result = "";
    		for (int i =0;i<numData;++i) {
                result += (m_data[i]);
    		}
        }
	}
	
	void StringBuilder(uint numData) {		
        for (uint j=0;j<NUM_ITERATION;++j) {
            StringBuilder result = new StringBuilder("");
    		for (int i =0;i<numData;++i) {
	    		result.Append(m_data[i]);
		    }
        }
	}

	void StringFormat(string format, params string[] strParams) {
        for (uint j=0;j<NUM_ITERATION;++j) {
		    string result = string.Format(format,m_data);
        }
	}

}
