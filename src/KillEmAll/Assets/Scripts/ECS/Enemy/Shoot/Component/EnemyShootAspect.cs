using Unity.Entities;

namespace ECS.Enemy.Shoot.Component
{
    public readonly partial struct EnemyShootAspect : IAspect
    {
        private readonly RefRW<EnemyShoot> _enemyShoot;
        
        public bool IsShooting
        {
            get => _enemyShoot.ValueRO.IsShooting;
            set => _enemyShoot.ValueRW.IsShooting = value;
        }
    }
}