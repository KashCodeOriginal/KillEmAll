using Unity.Entities;

namespace ECS.Enemy.Shoot.Component
{
    public struct EnemyShoot : IComponentData
    {
        public bool IsShooting;
    }
}