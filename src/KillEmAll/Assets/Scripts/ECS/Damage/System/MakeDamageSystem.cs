using ECS.Damage.Component;
using ECS.Guns.System;
using ECS.Healths.Component;
using Unity.Burst;
using Unity.Entities;

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
            
            foreach (var (healthAspect, makeDamage) in SystemAPI.Query<HealthAspect, MakeDamage>())
            {
                healthAspect.Health -= makeDamage.Value;
                
                ecb.RemoveComponent<MakeDamage>(healthAspect.Self);
                ecb.AddComponent(healthAspect.Self,new DamagedTag());
            }
        }
    }
}