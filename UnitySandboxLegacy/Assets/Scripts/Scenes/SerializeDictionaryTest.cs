using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SerializeDictionaryTest : MonoBehaviour {

	const string DATA_FILENAME = "DataFile.bytes";
	
	public Dictionary<int,int> m_data = new Dictionary<int, int>();
	
//----------------------------------------------------------------------------------------------------------------------            
    public Dictionary<int,int> GetData() {
        return m_data;
    }
    

//----------------------------------------------------------------------------------------------------------------------			

	// Use this for initialization
	void Start () {
	
		Load ();
		
		int num_data = 100;
		bool error = (m_data.Count!=num_data); //if the length doesn't match, then error
		if (!error) {
			//check if there is any data mismatch
			int i=0;
			while (!error && i<num_data) {
				if (m_data[i]!=GetValue(i)) {
					error = true;
				}
				++i;
			}
		}

		if (error) {
			m_data.Clear();
			for (int i=0;i<num_data;++i) {
				m_data[i] = GetValue(i);
			}
			Save ();			
			Debug.Log (string.Format("Data mismatch detected. File \"{0}\" overwritten",DATA_FILENAME));
		} else {
			Debug.Log ("Data check successful");
		}
	}

//----------------------------------------------------------------------------------------------------------------------			
	
	//Load data
	void Load() {
    
        if (!File.Exists(DATA_FILENAME))
            return;
	
        FileStream fs = new FileStream(DATA_FILENAME, FileMode.Open);
		try {
			BinaryFormatter formatter = new BinaryFormatter();		
			m_data = (Dictionary<int,int>) formatter.Deserialize(fs);
		}
		catch (SerializationException e) {
			Debug.LogError(string.Format("Failed to deserialize. Reason: {0}", e.Message));
		}
		fs.Close();
	}

//----------------------------------------------------------------------------------------------------------------------			
	
	//Save
	void Save() {
		FileStream fs = new FileStream(DATA_FILENAME, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		try {
			formatter.Serialize(fs, m_data);
		}
		catch (SerializationException e) {
			Debug.Log ("Failed to serialize. Reason: " + e.Message);
		}
		fs.Close();		
	}

//----------------------------------------------------------------------------------------------------------------------			
	//	
	int GetValue(int index) {
		return index * 10;
	}	
}
