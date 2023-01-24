using ECS.Player.Component;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Player.System
{
    /*public partial struct PlayerSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();

            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var playerSpawnAspect in SystemAPI.Query<PlayerSpawnAspect>())
            {
                var playerEntity = ecb.Instantiate(playerSpawnAspect.PlayerEntityPrefab);
                
                var playerControlEntity = ecb.Instantiate(playerSpawnAspect.PlayerControlEntityPrefab);

                ecb.SetComponent(playerEntity, new LocalTransform()
                {
                    Position = new float3(0,0,0),
                    Scale = 1f
                });
                
                ecb.SetComponent(playerEntity, new PlayerTag());


                /*var firstPersonPlayerControl = state.EntityManager.GetComponentData<FirstPersonPlayer>(playerControlEntity);

                firstPersonPlayerControl.ControlledCharacter = playerEntity;#1#
            }

            state.Enabled = false;
        }
    }*/
}