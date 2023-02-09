using ECS.Enemy.Shoot.Component;
using ECS.Guns.Shoot.Component;
using Unity.Burst;
using Unity.Entities;

namespace ECS.Enemy.Shoot.System
{
    [BurstCompile]
    public partial struct EnemyShootSystem : ISystem
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
            foreach (var (gunAspect, enemyShootAspect) in SystemAPI.Query<GunAspect, EnemyShootAspect>())
            {
                gunAspect.IsShooting = enemyShootAspect.IsShooting;
            }
        }
    }
}