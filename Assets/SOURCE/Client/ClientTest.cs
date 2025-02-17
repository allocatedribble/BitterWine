using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;
using UnityEngine.Localization.Pseudo;

public struct ClientRPCTest : IRpcCommand
{
    public FixedString32Bytes Message;
}

[BurstCompile]
[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial struct ClientTest : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkId>();
    }

    public void OnDestroy(ref SystemState state)
    {
        Debug.Log("ClientTest destroyed");
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        

        foreach (var (id, networkIdEntity) in SystemAPI.Query<RefRO<NetworkId>>().WithNone<NetworkStreamInGame>().WithEntityAccess())
        {
            var rpc_entity = ecb.CreateEntity(); 
            ecb.AddComponent(rpc_entity, new ClientRPCTest
            {
                Message = "Hello World!",
            });
        
            ecb.AddComponent(rpc_entity, new SendRpcCommandRequest
            {
                TargetConnection = networkIdEntity
            });
        }
        
        Debug.Log("hm?");
        
        ecb.Playback(state.EntityManager);

        state.Enabled = false;
    }
    
}
