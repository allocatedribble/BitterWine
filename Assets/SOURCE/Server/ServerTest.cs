using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial struct ServerTest : ISystem
{
    private ComponentLookup<NetworkId> networkIdFromEntity;
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkId>();
        networkIdFromEntity = state.GetComponentLookup<NetworkId>(true);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        networkIdFromEntity.Update(ref state);
        foreach (var (rpc, request, entity) in SystemAPI.Query<RefRW<ClientRPCTest>, RefRW<ReceiveRpcCommandRequest>>().WithEntityAccess())
        {
           ecb.AddComponent<NetworkStreamInGame>(request.ValueRO.SourceConnection); 
            var networkId = networkIdFromEntity[request.ValueRO.SourceConnection];
            Debug.Log($"Server received: {rpc.ValueRO.Message} {request.ValueRO.SourceConnection.ToFixedString()} : {networkId.Value}");
            
            ecb.DestroyEntity(entity);
        }
        
        ecb.Playback(state.EntityManager);
    }
}
