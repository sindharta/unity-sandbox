using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Collections;
using Unity.NetCode;
using Unity.Transforms;

public struct CubeGhostGhostSerializer : IGhostSerializer<CubeGhostSnapshotData>
{
    private ComponentType componentTypeMovableGhost;
    private ComponentType componentTypeLocalToWorld;
    private ComponentType componentTypeRotation;
    private ComponentType componentTypeTranslation;
    // FIXME: These disable safety since all serializers have an instance of the same type - causing aliasing. Should be fixed in a cleaner way
    [NativeDisableContainerSafetyRestriction][ReadOnly] private ArchetypeChunkComponentType<MovableGhost> ghostMovableGhostType;
    [NativeDisableContainerSafetyRestriction][ReadOnly] private ArchetypeChunkComponentType<Rotation> ghostRotationType;
    [NativeDisableContainerSafetyRestriction][ReadOnly] private ArchetypeChunkComponentType<Translation> ghostTranslationType;


    public int CalculateImportance(ArchetypeChunk chunk)
    {
        return 1;
    }

    public int SnapshotSize => UnsafeUtility.SizeOf<CubeGhostSnapshotData>();
    public void BeginSerialize(ComponentSystemBase system)
    {
        componentTypeMovableGhost = ComponentType.ReadWrite<MovableGhost>();
        componentTypeLocalToWorld = ComponentType.ReadWrite<LocalToWorld>();
        componentTypeRotation = ComponentType.ReadWrite<Rotation>();
        componentTypeTranslation = ComponentType.ReadWrite<Translation>();
        ghostMovableGhostType = system.GetArchetypeChunkComponentType<MovableGhost>(true);
        ghostRotationType = system.GetArchetypeChunkComponentType<Rotation>(true);
        ghostTranslationType = system.GetArchetypeChunkComponentType<Translation>(true);
    }

    public void CopyToSnapshot(ArchetypeChunk chunk, int ent, uint tick, ref CubeGhostSnapshotData snapshot, GhostSerializerState serializerState)
    {
        snapshot.tick = tick;
        var chunkDataMovableGhost = chunk.GetNativeArray(ghostMovableGhostType);
        var chunkDataRotation = chunk.GetNativeArray(ghostRotationType);
        var chunkDataTranslation = chunk.GetNativeArray(ghostTranslationType);
        snapshot.SetMovableGhostPlayerId(chunkDataMovableGhost[ent].PlayerId, serializerState);
        snapshot.SetRotationValue(chunkDataRotation[ent].Value, serializerState);
        snapshot.SetTranslationValue(chunkDataTranslation[ent].Value, serializerState);
    }
}
