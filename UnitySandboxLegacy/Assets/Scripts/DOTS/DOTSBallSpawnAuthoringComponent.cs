using Unity.Entities;

[GenerateAuthoringComponent]
public struct DOTSBallSpawnAuthoringComponent : IComponentData {
    public Entity BallPrefab;
    public int NumBalls;    
}

