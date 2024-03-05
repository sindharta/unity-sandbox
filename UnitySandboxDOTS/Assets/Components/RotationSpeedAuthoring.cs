using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class RotationSpeedAuthoring : MonoBehaviour
{
    public float DegreesPerSecond = 360.0f;
    
    class Baker : Baker<RotationSpeedAuthoring> {
        public override void Bake(RotationSpeedAuthoring authoring) {
            //TransformUsageFlags.Dynamic to specify that the entity needs the standard transform components
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            RotationSpeed rotationSpeed = new RotationSpeed {
                RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
            };

            AddComponent(entity, rotationSpeed);
        }
    }    
}

public struct RotationSpeed : IComponentData {     
    public float RadiansPerSecond;  // how quickly the entity rotates 
}


