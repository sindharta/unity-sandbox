using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

public partial struct CubeRotationSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        
        float deltaTime = SystemAPI.Time.DeltaTime;

        // This foreach loops through all entities that have LocalTransform and RotationSpeed components. 
        // You need to modify the LocalTransform, so it is wrapped in RefRW (read-write). 
        // You only need to read the RotationSpeed, so it is wrapped in RefRO (read only). 
        foreach ((RefRW<LocalTransform> transform, RefRO<RotationSpeed> rotationSpeed) in
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
        {
            // Rotate the transform around the Y axis. 
            float radians = rotationSpeed.ValueRO.RadiansPerSecond * deltaTime;
            transform.ValueRW = transform.ValueRW.RotateY(radians);
        }
        
    }
}