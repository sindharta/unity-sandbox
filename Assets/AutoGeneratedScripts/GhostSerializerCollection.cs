using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Networking.Transport;
using Unity.NetCode;

public struct UnitySandboxGhostSerializerCollection : IGhostSerializerCollection
{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public string[] CreateSerializerNameList()
    {
        var arr = new string[]
        {
            "CubeGhostGhostSerializer",
        };
        return arr;
    }

    public int Length => 1;
#endif
    public static int FindGhostType<T>()
        where T : struct, ISnapshotData<T>
    {
        if (typeof(T) == typeof(CubeGhostSnapshotData))
            return 0;
        return -1;
    }

    public void BeginSerialize(ComponentSystemBase system)
    {
        m_CubeGhostGhostSerializer.BeginSerialize(system);
    }

    public int CalculateImportance(int serializer, ArchetypeChunk chunk)
    {
        switch (serializer)
        {
            case 0:
                return m_CubeGhostGhostSerializer.CalculateImportance(chunk);
        }

        throw new ArgumentException("Invalid serializer type");
    }

    public int GetSnapshotSize(int serializer)
    {
        switch (serializer)
        {
            case 0:
                return m_CubeGhostGhostSerializer.SnapshotSize;
        }

        throw new ArgumentException("Invalid serializer type");
    }

    public int Serialize(ref DataStreamWriter dataStream, SerializeData data)
    {
        switch (data.ghostType)
        {
            case 0:
            {
                return GhostSendSystem<UnitySandboxGhostSerializerCollection>.InvokeSerialize<CubeGhostGhostSerializer, CubeGhostSnapshotData>(m_CubeGhostGhostSerializer, ref dataStream, data);
            }
            default:
                throw new ArgumentException("Invalid serializer type");
        }
    }
    private CubeGhostGhostSerializer m_CubeGhostGhostSerializer;
}

public struct EnableUnitySandboxGhostSendSystemComponent : IComponentData
{}
public class UnitySandboxGhostSendSystem : GhostSendSystem<UnitySandboxGhostSerializerCollection>
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<EnableUnitySandboxGhostSendSystemComponent>();
    }

    public override bool IsEnabled()
    {
        return HasSingleton<EnableUnitySandboxGhostSendSystemComponent>();
    }
}
