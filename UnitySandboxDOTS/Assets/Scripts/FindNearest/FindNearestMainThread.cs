using UnityEngine;

public class FindNearestMainThread : MonoBehaviour
{
    public void Update()
    {
        // Find the nearest Target.
        // When comparing distances, it's cheaper to compare
        // the squares of the distances because doing so
        // avoids computing square roots.
        int seekerCount = Spawner.SeekerTransforms.Length;
        int targetCount = Spawner.TargetTransforms.Length;
        for (int i = 0; i < seekerCount; ++i) {
            Transform seekerTransform = Spawner.SeekerTransforms[i];
            Vector3 seekerPos = seekerTransform.localPosition;
            Vector3 nearestTargetPos = default;
            float nearestDistSq = float.MaxValue;

            
            for (int j = 0; j < targetCount; ++j) {
                Transform targetTransform = Spawner.TargetTransforms[j];
                Vector3 offset = targetTransform.localPosition - seekerPos;
                float distSq = offset.sqrMagnitude;

                if (distSq < nearestDistSq)
                {
                    nearestDistSq = distSq;
                    nearestTargetPos = targetTransform.localPosition;
                }
            }
 
            Debug.DrawLine(seekerPos, nearestTargetPos);
        }        
    }
}