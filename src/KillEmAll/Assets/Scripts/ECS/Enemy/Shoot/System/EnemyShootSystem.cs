using ECS.Enemy.Shoot.Component;
using ECS.Guns.Component;
using Unity.Entities;

namespace ECS.Enemy.Shoot.System
{
    public partial struct EnemyShootSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (gunAspect, enemyShootAspect) in SystemAPI.Query<GunAspect, EnemyShootAspect>())
            {
                gunAspect.IsShooting = enemyShootAspect.IsShooting;
            }
        }
    }
}