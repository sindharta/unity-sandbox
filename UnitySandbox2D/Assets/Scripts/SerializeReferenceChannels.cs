using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>One channel. A plain managed class, so each instance gets its own RefId.</summary>
[Serializable]
public class FloatChannelRef {
    public string channelName;   // your key; a string, so not animatable itself
    public float value;          // the animatable float
}

/// <summary>
/// Dynamic channel count via [SerializeReference]: the animation system enumerates the object's
/// managed-reference registry, so every element in this list contributes its own animatable
/// property, exposed as "managedReferences[refId].value".
/// </summary>
[ExecuteAlways]
public class SerializeReferenceChannels : MonoBehaviour {
    [SerializeReference] List<FloatChannelRef> m_Channels = new List<FloatChannelRef>();

    /// <summary>Raised after the animation system writes any channel.</summary>
    public event Action Applied;

    public int channelCount => m_Channels.Count;

    public float this[string name] {
        get {
            FloatChannelRef channel = Find(name);
            return channel != null ? channel.value : 0f;
        }
        set {
            FloatChannelRef channel = Find(name);
            if (channel != null)
                channel.value = value;
        }
    }

    public bool TryGetValue(string name, out float value) {
        FloatChannelRef channel = Find(name);
        if (channel != null) {
            value = channel.value;
            return true;
        }

        value = 0f;
        return false;
    }

    FloatChannelRef Find(string name) {
        for (int i = 0; i < m_Channels.Count; i++) {
            if (m_Channels[i] != null && m_Channels[i].channelName == name)
                return m_Channels[i];
        }

        return null;
    }

    void OnDidApplyAnimationProperties() {
        Applied?.Invoke();
    }

#if UNITY_EDITOR
    // Two-phase: apply the size change first so the slot exists in the backend, then re-acquire
    // the element and assign the value.
    [ContextMenu("Add Channel (SerializedProperty)")]
    void AddChannelViaSerializedProperty() {
        SerializedObject so = new SerializedObject(this);
        SerializedProperty list = so.FindProperty("m_Channels");

        int index = list.arraySize;
        list.arraySize = index + 1;
        so.ApplyModifiedProperties();

        so.Update();
        list = so.FindProperty("m_Channels");
        SerializedProperty element = list.GetArrayElementAtIndex(index);
        element.managedReferenceValue = new FloatChannelRef { channelName = $"Channel{index}", value = 0f };
        so.ApplyModifiedProperties();

        ReportLast("SerializedProperty");
    }

    // Plain list mutation, letting ordinary serialization register the reference.
    [ContextMenu("Add Channel (Direct)")]
    void AddChannelDirect() {
        m_Channels.Add(new FloatChannelRef { channelName = $"Channel{m_Channels.Count}", value = 0f });
        EditorUtility.SetDirty(this);

        ReportLast("Direct");
    }

    [ContextMenu("Clear Channels")]
    void ClearChannels() {
        m_Channels.Clear();
        EditorUtility.SetDirty(this);

        SerializedObject so = new SerializedObject(this);
        so.FindProperty("m_Channels").arraySize = 0;
        so.ApplyModifiedProperties();

        Debug.Log("Cleared.", this);
    }

    // Reads back through a fresh SerializedObject, which forces a serialize round-trip and is
    // where RefIds get assigned.
    void ReportLast(string how) {
        SerializedObject so = new SerializedObject(this);
        SerializedProperty list = so.FindProperty("m_Channels");
        if (list.arraySize == 0) {
            Debug.Log($"{how}: list is empty.", this);
            return;
        }

        SerializedProperty element = list.GetArrayElementAtIndex(list.arraySize - 1);
        Debug.Log($"{how}: refId={element.managedReferenceId}" +
            $" type='{element.managedReferenceFullTypename}'", this);
    }
#endif
}