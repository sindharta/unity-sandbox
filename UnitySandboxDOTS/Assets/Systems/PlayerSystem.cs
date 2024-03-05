using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerSystem : ISystem
{
    // Because OnUpdate accesses a managed object (the camera), we cannot Burst compile 
    // this method, so we don't use the [BurstCompile] attribute here.
    public void OnUpdate(ref SystemState state) {
        float3 movement = new float3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        movement *= SystemAPI.Time.DeltaTime;

        foreach (RefRW<LocalTransform> playerTransform in 
                 SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Player>())
        {
            // move the player tank
            playerTransform.ValueRW.Position += movement;

            // move the camera to follow the player
            Transform cameraTransform = Camera.main.transform;
            Vector3 nextCameraPos = playerTransform.ValueRO.Position;
            nextCameraPos -= 10.0f * (Vector3)playerTransform.ValueRO.Forward();  // move the camera back from the player
            nextCameraPos += new Vector3(0, 5f, 0);  // raise the camera by an offset
            cameraTransform.position = nextCameraPos;
            cameraTransform.LookAt(playerTransform.ValueRO.Position);  // look at the player
        }
    }
}