using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class FindNearestJobSpawner : MonoBehaviour {

    public void Start() {
        Spawner spawner = Object.FindObjectOfType<Spawner>();
        // We use the Persistent allocator because these arrays must
        // exist for the run of the program.
        TargetPositions = new NativeArray<float3>(spawner.NumTargets, Allocator.Persistent);
        SeekerPositions = new NativeArray<float3>(spawner.NumSeekers, Allocator.Persistent);
        NearestTargetPositions = new NativeArray<float3>(spawner.NumSeekers, Allocator.Persistent);
    }

    // We are responsible for disposing of our allocations
    // when we no longer need them.
    public void OnDestroy() {
        TargetPositions.Dispose();
        SeekerPositions.Dispose();
        NearestTargetPositions.Dispose();
    }

    public void Update() {
        // Copy every target transform to a NativeArray.
        for (int i = 0; i < TargetPositions.Length; i++) {
            // Vector3 is implicitly converted to float3
            TargetPositions[i] = Spawner.TargetTransforms[i].localPosition;
        }

        // Copy every seeker transform to a NativeArray.
        for (int i = 0; i < SeekerPositions.Length; i++) {
            // Vector3 is implicitly converted to float3
            SeekerPositions[i] = Spawner.SeekerTransforms[i].localPosition;
        }

        JobHandle findHandle;

        switch (m_findNearestType) {
            case FindNearestType.SingleThreadBruteForce: {
                // To schedule a job, we first need to create an instance and populate its fields.
                FindNearestJob findJob = new FindNearestJob {
                    TargetPositions = TargetPositions,
                    SeekerPositions = SeekerPositions,
                    NearestTargetPositions = NearestTargetPositions,
                };

                // Schedule() puts the job instance on the job queue.
                findHandle = findJob.Schedule();
                break;
            }
            case FindNearestType.ParallelBruteForce: {
                FindNearestJobParallel findJob = new FindNearestJobParallel() {
                    TargetPositions = TargetPositions,
                    SeekerPositions = SeekerPositions,
                    NearestTargetPositions = NearestTargetPositions,
                };
            
                // This job processes every seeker, so the
                // seeker array length is used as the index count.
                // A batch size of 100 is semi-arbitrarily chosen here 
                // simply because it's not too big but not too small.
                findHandle = findJob.Schedule(SeekerPositions.Length, m_maxParallelJobs);
                break;
            }
            case FindNearestType.ParallelSmart: {
                SortJob<float3, AxisXComparer> sortJob = TargetPositions.SortJob(new AxisXComparer { });
                JobHandle sortHandle = sortJob.Schedule();

                FindNearestSmartJobParallel findJob = new FindNearestSmartJobParallel()
                {
                    TargetPositions = TargetPositions,
                    SeekerPositions = SeekerPositions,
                    NearestTargetPositions = NearestTargetPositions,
                };

                // By passing the sort job handle to Schedule(), the find job will depend
                // upon the sort job, meaning the find job will not start executing until 
                // after the sort job has finished.
                // The find nearest job needs to wait for the sorting, 
                // so it must depend upon the sorting jobs. 
                findHandle = findJob.Schedule(SeekerPositions.Length, m_maxParallelJobs, sortHandle);
                break;
            }
            default: {
                findHandle = new JobHandle();
                break;
            }
        }

        // The Complete method will not return until the job represented by
        // the handle finishes execution. Effectively, the main thread waits
        // here until the job is done.
        findHandle.Complete();

        // Draw a debug line from each seeker to its nearest target.
        for (int i = 0; i < SeekerPositions.Length; i++) {
            // float3 is implicitly converted to Vector3
            Debug.DrawLine(SeekerPositions[i], NearestTargetPositions[i]);
        }
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private FindNearestType m_findNearestType = FindNearestType.ParallelBruteForce;
    
    [Range(1,500)][SerializeField] int m_maxParallelJobs = 100;

    // The size of our arrays does not need to vary, so rather than create
    // new arrays every field, we'll create the arrays in Awake() and store them
    // in these fields.
    NativeArray<float3> TargetPositions;
    NativeArray<float3> SeekerPositions;
    NativeArray<float3> NearestTargetPositions;
    
}