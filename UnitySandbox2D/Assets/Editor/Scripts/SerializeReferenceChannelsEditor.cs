using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

[CustomEditor(typeof(SerializeReferenceChannels))]
public class SerializeReferenceChannelsEditor : Editor {
    // Unity injects "m_Script" into every MonoBehaviour's serialized data. There is no C# member
    // behind it, so there is nothing for nameof to point at.
    const string k_ScriptPropertyPath = "m_Script";

    SerializedProperty m_Script;
    SerializedProperty m_Channels;

    void OnEnable() {
        m_Script = serializedObject.FindProperty(k_ScriptPropertyPath);
        m_Channels = serializedObject.FindProperty(SerializeReferenceChannels.kChannelsPropertyPath);
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.PropertyField(m_Script);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Channels", EditorStyles.boldLabel);

        if (m_Channels.arraySize == 0)
            EditorGUILayout.HelpBox("No channels yet. Use Add Channel below.", MessageType.Info);

        int removeIndex = -1;
        for (int i = 0; i < m_Channels.arraySize; i++) {
            if (DrawChannel(m_Channels.GetArrayElementAtIndex(i), i))
                removeIndex = i;
        }

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();
        using (new EditorGUILayout.HorizontalScope()) {
            if (GUILayout.Button("Add Channel"))
                AddChannel();

            using (new EditorGUI.DisabledScope(m_Channels.arraySize == 0)) {
                if (GUILayout.Button("Clear"))
                    ClearChannels();
            }
        }

        if (removeIndex >= 0)
            RemoveChannel(removeIndex);
    }

    /// <summary>Draws one channel. Returns true when the user asked to remove it.</summary>
    bool DrawChannel(SerializedProperty element, int index) {
        bool remove = false;

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
            // A null reference has no RefId, so the animation system cannot see it at all. The
            // Inspector's own list "+" produces exactly this, which is why Add Channel exists.
            if (element.managedReferenceId == ManagedReferenceUtility.RefIdNull) {
                using (new EditorGUILayout.HorizontalScope()) {
                    EditorGUILayout.LabelField($"[{index}] null reference - not animatable");
                    remove = GUILayout.Button("Remove", GUILayout.Width(70f));
                }

                return remove;
            }

            SerializedProperty name = element.FindPropertyRelative(nameof(FloatChannelRef.channelName));
            SerializedProperty value = element.FindPropertyRelative(nameof(FloatChannelRef.value));

            using (new EditorGUILayout.HorizontalScope()) {
                EditorGUILayout.PropertyField(name, GUIContent.none);
                remove = GUILayout.Button("Remove", GUILayout.Width(70f));
            }

            EditorGUILayout.PropertyField(value);

            // The attribute string the Animation window binds to. Selectable so it can be copied.
            EditorGUILayout.LabelField("Binding", EditorStyles.miniBoldLabel);
            EditorGUILayout.SelectableLabel($"managedReferences[{element.managedReferenceId}].value",
                EditorStyles.miniLabel, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        }

        return remove;
    }

    void AddChannel() {
        SerializeReferenceChannels component = (SerializeReferenceChannels)target;
        component.AddChannel($"Channel{component.channelCount}");
        ApplyStructuralChange(component);
    }

    void ClearChannels() {
        SerializeReferenceChannels component = (SerializeReferenceChannels)target;
        component.ClearChannels();
        ApplyStructuralChange(component);
    }

    void RemoveChannel(int index) {
        SerializeReferenceChannels component = (SerializeReferenceChannels)target;
        component.RemoveChannel(index);
        ApplyStructuralChange(component);
    }

    // The list is mutated on the target directly rather than through SerializedProperty, because
    // that is what actually gets RefIds assigned. Update() then forces the serialize round-trip
    // and refreshes the SerializedObject we are drawing from.
    void ApplyStructuralChange(SerializeReferenceChannels component) {
        EditorUtility.SetDirty(component);

        if (!EditorApplication.isPlaying && component.gameObject.scene.IsValid())
            EditorSceneManager.MarkSceneDirty(component.gameObject.scene);

        serializedObject.Update();

        // The channel count changed after layout was computed, so abandon this GUI pass rather
        // than letting the layout and repaint events disagree.
        EditorGUIUtility.ExitGUI();
    }
}