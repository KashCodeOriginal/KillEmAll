using ECS.Damage.Component;
using ECS.Healths.Component;
using Unity.Burst;
using Unity.Entities;

namespace ECS.Damage.System
{
    [BurstCompile]
    public partial struct DamagedSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (healthAspect, damagedTag) in SystemAPI.Query<HealthAspect, DamagedTag>())
            {
                if (healthAspect.Health <= 0)
                {
                    ecb.DestroyEntity(healthAspect.Self);
                }
            }
        }
    }
}