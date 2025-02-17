using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

public struct Config : IComponentData
{
    public Entity CubePrefab;
    public int CubeCount;
}

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial struct CubeSpawningSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        Debug.Log("on_create");
        state.RequireForUpdate<Config>();
        // state.RequireForUpdate<PhysicsStep>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<Config>();

        var rand = new Random(132131541);

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        for (int i = 0; i < config.CubeCount; i++)
        {
            var entity = ecb.Instantiate(config.CubePrefab);
            ecb.SetComponent(entity, new LocalTransform
            {
                Position = new float3(i - 500, 100, 0),
                Rotation = quaternion.EulerXYZ(new float3(rand.NextFloat(0, 360), rand.NextFloat(0, 360),
                    rand.NextFloat(0, 360))),
                Scale = 1
            });
            
            ecb.AddComponent(entity, new PhysicsVelocity
            {
                Angular = float3.zero,
                Linear = float3.zero,
            });
        }
        
        ecb.Playback(state.EntityManager);

        // }



        state.Enabled = false;
    }
}