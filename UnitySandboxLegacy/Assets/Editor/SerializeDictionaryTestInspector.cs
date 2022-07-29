using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SerializeDictionaryTest))]
public class SerializeDictionaryTestEditor : UnityEditor.Editor 
{
	public override void OnInspectorGUI()
	{
		SerializeDictionaryTest curTarget = (SerializeDictionaryTest)target;
        Dictionary<int,int> data = curTarget.GetData();
        
        EditorGUILayout.LabelField("DataCount", data.Count.ToString());
		Dictionary<int, int>.Enumerator enumerator = data.GetEnumerator();
        ++EditorGUI.indentLevel;
        
        //header
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Keys");
        EditorGUILayout.LabelField("Values");
        EditorGUILayout.EndHorizontal();
        
		while (enumerator.MoveNext()) {
            KeyValuePair<int, int> curData = enumerator.Current;
            EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(curData.Key.ToString());
            EditorGUILayout.LabelField(curData.Value.ToString());
            EditorGUILayout.EndHorizontal();
		}
        --EditorGUI.indentLevel;		
	}
}