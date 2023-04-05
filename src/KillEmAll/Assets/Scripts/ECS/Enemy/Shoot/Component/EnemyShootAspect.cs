using ECS.Guns.Shoot.Component;
using Unity.Entities;

namespace ECS.Enemy.Shoot.Component
{
    public readonly partial struct EnemyShootAspect : IAspect
    {
        private readonly RefRW<EnemyShoot> _enemyShoot;
        private readonly RefRO<Gun> _gun;

        public bool IsShooting
        {
            get => _enemyShoot.ValueRO.IsShooting;
            set => _enemyShoot.ValueRW.IsShooting = value;
        }
        
        public float ShootRange => _gun.ValueRO.GunConfigData.ShootRange;
    }
}