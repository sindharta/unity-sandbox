using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Networking.Transport;
using Unity.NetCode;

public struct UnitySandboxGhostDeserializerCollection : IGhostDeserializerCollection
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
    public void Initialize(World world)
    {
        var curCubeGhostGhostSpawnSystem = world.GetOrCreateSystem<CubeGhostGhostSpawnSystem>();
        m_CubeGhostSnapshotDataNewGhostIds = curCubeGhostGhostSpawnSystem.NewGhostIds;
        m_CubeGhostSnapshotDataNewGhosts = curCubeGhostGhostSpawnSystem.NewGhosts;
        curCubeGhostGhostSpawnSystem.GhostType = 0;
    }

    public void BeginDeserialize(JobComponentSystem system)
    {
        m_CubeGhostSnapshotDataFromEntity = system.GetBufferFromEntity<CubeGhostSnapshotData>();
    }
    public bool Deserialize(int serializer, Entity entity, uint snapshot, uint baseline, uint baseline2, uint baseline3,
        ref DataStreamReader reader, NetworkCompressionModel compressionModel)
    {
        switch (serializer)
        {
            case 0:
                return GhostReceiveSystem<UnitySandboxGhostDeserializerCollection>.InvokeDeserialize(m_CubeGhostSnapshotDataFromEntity, entity, snapshot, baseline, baseline2,
                baseline3, ref reader, compressionModel);
            default:
                throw new ArgumentException("Invalid serializer type");
        }
    }
    public void Spawn(int serializer, int ghostId, uint snapshot, ref DataStreamReader reader,
        NetworkCompressionModel compressionModel)
    {
        switch (serializer)
        {
            case 0:
                m_CubeGhostSnapshotDataNewGhostIds.Add(ghostId);
                m_CubeGhostSnapshotDataNewGhosts.Add(GhostReceiveSystem<UnitySandboxGhostDeserializerCollection>.InvokeSpawn<CubeGhostSnapshotData>(snapshot, ref reader, compressionModel));
                break;
            default:
                throw new ArgumentException("Invalid serializer type");
        }
    }

    private BufferFromEntity<CubeGhostSnapshotData> m_CubeGhostSnapshotDataFromEntity;
    private NativeList<int> m_CubeGhostSnapshotDataNewGhostIds;
    private NativeList<CubeGhostSnapshotData> m_CubeGhostSnapshotDataNewGhosts;
}
public struct EnableUnitySandboxGhostReceiveSystemComponent : IComponentData
{}
public class UnitySandboxGhostReceiveSystem : GhostReceiveSystem<UnitySandboxGhostDeserializerCollection>
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<EnableUnitySandboxGhostReceiveSystemComponent>();
    }
}
