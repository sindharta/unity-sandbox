using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SerializeDictionaryTest))]
public class SerializeDictionaryTestEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		SerializeDictionaryTest cur_target = (SerializeDictionaryTest)target;
        Dictionary<int,int> data = cur_target.GetData();
        
        EditorGUILayout.LabelField("DataCount", data.Count.ToString());
		var enumerator = cur_target.GetData().GetEnumerator();
        ++EditorGUI.indentLevel;
        
        //header
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Keys");
        EditorGUILayout.LabelField("Values");
        EditorGUILayout.EndHorizontal();
        
		while (enumerator.MoveNext()) {
            var cur_data = enumerator.Current;            
            EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(cur_data.Key.ToString());
            EditorGUILayout.LabelField(cur_data.Value.ToString());
            EditorGUILayout.EndHorizontal();
		}
        --EditorGUI.indentLevel;		
	}
}