using Unity.Entities;
using Unity.NetCode;

[GenerateAuthoringComponent]
public struct MovableGhost : IComponentData
{
    [GhostDefaultField]
    public int PlayerId;
}