using ECS.Damage.Component;
using ECS.Healths.Component;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using GunBulletsCreationSystem = ECS.Guns.Shoot.System.GunBulletsCreationSystem;

namespace ECS.Damage.System
{
    [BurstCompile]
    [UpdateAfter(typeof(GunBulletsCreationSystem))]
    public partial struct MakeDamageSystem : ISystem
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
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var makeDamage in SystemAPI.Query<MakeDamage>())
            {
                var healthAspect = SystemAPI.GetAspectRW<HealthAspect>(makeDamage.Target);
                
                healthAspect.Health -= makeDamage.Value;
                
                ecb.DestroyEntity(makeDamage.Self);
                
                ecb.AddComponent(makeDamage.Target, new DamagedTag());
            }
        }
    }
}