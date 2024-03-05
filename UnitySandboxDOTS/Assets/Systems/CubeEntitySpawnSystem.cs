using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Random = System.Random;

public partial struct CubeEntitySpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<CubeEntitySpawner>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        //Only run the system once
        state.Enabled = false;

        //spawn the prefab 
        Entity prefab = SystemAPI.GetSingleton<CubeEntitySpawner>().CubePrefab;
        NativeArray<Entity> instances = state.EntityManager.Instantiate(prefab, 10, Allocator.Temp);
        
        // randomly set the positions of the new cubes
        // (we'll use a fixed seed, 123, but if you want different randomness 
        // for each run, you can instead use the elapsed time value as the seed)
        Unity.Mathematics.Random random = new Unity.Mathematics.Random(123);
        foreach (Entity entity in instances) {
            RefRW<LocalTransform> transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            transform.ValueRW.Position = random.NextFloat3(new float3(10, 0, 10));
        }        
    }
}