using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class DOTSBallSpawnSystem : SystemBase {
    private struct DOTSBallSpawned : IComponentData {
    }

//----------------------------------------------------------------------------------------------------------------------    
    
    protected override void OnCreate() {
        base.OnCreate();
        m_beginInitializationECBS = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }    
    
//----------------------------------------------------------------------------------------------------------------------    

    protected override void OnUpdate() {
        EntityCommandBuffer cmd = m_beginInitializationECBS.CreateCommandBuffer();
        
        JobHandle jobHandle =  Entities
            .WithNone<DOTSBallSpawned>()
            .ForEach((Entity entity, ref DOTSBallSpawnAuthoringComponent spawnAC) => {
                if (spawnAC.NumBalls <= 0) {
                    cmd.AddComponent<DOTSBallSpawned>(entity);
                    return;
                }
                
                if (Entity.Null == spawnAC.BallPrefab) {
                    cmd.AddComponent<DOTSBallSpawned>(entity);
                    Debug.LogError("Prefab is not set in DOTSBallSpawnAuthoringComponent");
                    return;
                }

                Entity instance = cmd.Instantiate(spawnAC.BallPrefab);
                
                cmd.SetComponent(instance, new Translation() {
                    Value = new float3(spawnAC.NumBalls * 0.1f,0,0)
                });
                spawnAC.NumBalls = spawnAC.NumBalls-1;

            }).Schedule(Dependency);
        
        
        // Make sure that the ECB system knows about our job
        m_beginInitializationECBS.AddJobHandleForProducer(jobHandle);
        
        
    }
//----------------------------------------------------------------------------------------------------------------------
    
    BeginInitializationEntityCommandBufferSystem m_beginInitializationECBS;

}

    