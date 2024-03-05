using Unity.Entities;
using UnityEngine;

public class CubeEntitySpawnerAuthoring : MonoBehaviour {
    public GameObject CubePrefab;

    class Baker : Baker<CubeEntitySpawnerAuthoring> {
        public override void Bake(CubeEntitySpawnerAuthoring authoring) {
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);
            CubeEntitySpawner spawner = new CubeEntitySpawner {
                CubePrefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic)
            };
            AddComponent(entity, spawner);
        }
    }
}

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
struct CubeEntitySpawner : IComponentData
{
    public Entity CubePrefab;
}