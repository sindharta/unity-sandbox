using Unity.Entities;
using UnityEngine;

public class TankSceneConfigAuthoring : MonoBehaviour
{
    public GameObject TankPrefab;
    public GameObject CannonBallPrefab;
    public int TankCount;

    class Baker : Baker<TankSceneConfigAuthoring>
    {
        public override void Bake(TankSceneConfigAuthoring authoring) {
            // The config entity itself doesn’t need transform components,
            // so we use TransformUsageFlags.None
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);
            AddComponent(entity, new TankSceneConfig {
                // Bake the prefab into entities. GetEntity will return the 
                // root entity of the prefab hierarchy.
                TankPrefab = GetEntity(authoring.TankPrefab, TransformUsageFlags.Dynamic),
                CannonBallPrefab = GetEntity(authoring.CannonBallPrefab, TransformUsageFlags.Dynamic),
                TankCount = authoring.TankCount,
            });
        }
    }
}
public struct TankSceneConfig : IComponentData {
    public Entity TankPrefab;
    public Entity CannonBallPrefab;
    public int TankCount;
}