    using Unity.Entities;
    using UnityEngine;

    public class CameraAuthoring : MonoBehaviour
    {
        public GameObject camera;
        public class Baker : Baker<CameraAuthoring>
        {
            public override void Bake(CameraAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CameraControl
                {
                    Position = authoring.transform.position,
                    Rotation = authoring.transform.rotation
                });
            }
        }
    }