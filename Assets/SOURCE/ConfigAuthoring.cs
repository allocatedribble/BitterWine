using Unity.Entities;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour
{
    public GameObject SpherePrefab;
    public int SphereCount;

}

public class Baker : Baker<ConfigAuthoring>
{
    public override void Bake(ConfigAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Config
        {
            CubePrefab = GetEntity(authoring.SpherePrefab, TransformUsageFlags.Dynamic),
            CubeCount = authoring.SphereCount,
        });
    }
}