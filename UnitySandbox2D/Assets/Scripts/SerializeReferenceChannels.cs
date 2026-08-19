using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>One channel. A plain managed class, so each instance gets its own RefId.</summary>
[Serializable]
public class FloatChannelRef {
    public string channelName;   // your key; a string, so not animatable itself
    public float value;          // the animatable float
}

/// <summary>
/// Dynamic channel count via [SerializeReference]: the animation system enumerates the object's
/// managed-reference registry, so every non-null element contributes its own animatable property,
/// exposed as "managedReferences[refId].value".
/// </summary>
[ExecuteAlways]
public class SerializeReferenceChannels : MonoBehaviour {
    [SerializeReference] List<FloatChannelRef> m_Channels = new List<FloatChannelRef>();

    /// <summary>
    /// Serialized property path of the channel list. The field is private, so editor code in
    /// another assembly cannot apply nameof to it directly.
    /// </summary>
    public const string kChannelsPropertyPath = nameof(m_Channels);

    /// <summary>Raised after the animation system writes any channel.</summary>
    public event Action Applied;

    public int channelCount => m_Channels.Count;

    public IReadOnlyList<FloatChannelRef> channels => m_Channels;

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

    /// <summary>
    /// Appends a channel. The caller is responsible for making the object serialize afterwards;
    /// until it does, the new reference has no RefId and no animatable property exists for it.
    /// </summary>
    public FloatChannelRef AddChannel(string name) {
        FloatChannelRef channel = new FloatChannelRef { channelName = name, value = 0f };
        m_Channels.Add(channel);
        return channel;
    }

    public void RemoveChannel(int index) {
        if (index >= 0 && index < m_Channels.Count)
            m_Channels.RemoveAt(index);
    }

    public void ClearChannels() {
        m_Channels.Clear();
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
}