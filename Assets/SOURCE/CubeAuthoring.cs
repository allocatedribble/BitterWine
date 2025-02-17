using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public class CubeAuthoring :MonoBehaviour
{

    public class Baker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,new  PhysicsVelocity
            {
                
            });
            
            AddComponent(entity, new PhysicsMass
            {
            });
        }
    }
    
}
