using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompile]
public struct FindNearestJobParallel : IJobParallelFor
{
    [ReadOnly] public NativeArray<float3> TargetPositions;
    [ReadOnly] public NativeArray<float3> SeekerPositions;

    public NativeArray<float3> NearestTargetPositions;

    // An IJobParallelFor's Execute() method takes an index parameter and 
    // is called once for each index, from 0 up to the index count:
    public void Execute(int index)
    {
        float3 seekerPos = SeekerPositions[index];
        float nearestDistSq = float.MaxValue;
        for (int i = 0; i < TargetPositions.Length; i++)
        {
            float3 targetPos = TargetPositions[i];
            float distSq = math.distancesq(seekerPos, targetPos);
            if (distSq < nearestDistSq)
            {
                nearestDistSq = distSq;
                NearestTargetPositions[index] = targetPos;
            }
        }
    }
}