using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

// This attribute puts this system before TransformSystemGroup in the update order.
// ShootingSystem only sets the local transforms of the cannonballs, but the transform systems
// in TransformSystemGroup will set their world transforms (LocalToWorld).
// If the ShootingSystem were to update after TransformSystemGroup in the frame, the cannonballs
// it spawned would render at the origin for a single frame.
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct ShootingSystem : ISystem
{
    private float timer;
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        // Only shoot in frames where timer has expired
        timer -= SystemAPI.Time.DeltaTime;
        if (timer > 0) {
            return; 
        } 
        timer = 0.3f;   // reset timer

        TankSceneConfig config = SystemAPI.GetSingleton<TankSceneConfig>();
        
        LocalTransform ballTransform = state.EntityManager.GetComponentData<LocalTransform>(config.CannonBallPrefab);
        
        // For each turret of every tank, spawn a cannonball and set its initial velocity
        foreach (var (tank, transform, color) in SystemAPI.Query<RefRO<Tank>, RefRO<LocalToWorld>, RefRO<URPMaterialPropertyBaseColor>>()) {
            Entity cannonBallEntity = state.EntityManager.Instantiate(config.CannonBallPrefab);
            
            // Set color of the cannonball to match the tank that shot it.
            state.EntityManager.SetComponentData(cannonBallEntity, color.ValueRO);
            
            // We need the transform of the cannon in world space, so we get its LocalToWorld instead of LocalTransform.
            var cannonTransform = state.EntityManager.GetComponentData<LocalToWorld>(tank.ValueRO.Cannon);
            ballTransform.Position =  cannonTransform.Position;
            
            // Set position of the new cannonball to match the spawn point
            state.EntityManager.SetComponentData(cannonBallEntity, ballTransform);

            // Set velocity of the cannonball to shoot out of the cannon.
            state.EntityManager.SetComponentData(cannonBallEntity, new CannonBall {
                Velocity = math.normalize(cannonTransform.Up) * 12.0f
            });
        }
    }
}