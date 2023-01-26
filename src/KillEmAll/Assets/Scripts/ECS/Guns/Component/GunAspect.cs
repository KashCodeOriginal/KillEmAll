using ECS.Environment.Component;
using Unity.Entities;

namespace ECS.Guns.Component
{
    public readonly partial struct GunAspect : IAspect
    {
        private readonly RefRW<Gun> _gun;
        
        private readonly RefRW<Timer> _timer;

        public int Ammo
        {
            get => _gun.ValueRO.Ammo;
            set => _gun.ValueRW.Ammo = value;
        }

        public float Timer
        {
            get => _timer.ValueRO.Value;
            set => _timer.ValueRW.Value = value;
        }

        public Entity BulletEntity => _gun.ValueRO.BulletEntity;

        public int MaxAmmo => _gun.ValueRO.MaxAmmo;
        
        public float ReloadTime => _gun.ValueRO.ReloadTime;
        
        public float FireRate => _gun.ValueRO.FireRate;
        
        public bool IsReloading
        {
            get => _gun.ValueRO.IsReloading;
            set => _gun.ValueRW.IsReloading = value;
        }

        public bool NeedReload => Ammo <= 0;

        public bool CanShoot => Timer <= 0;

        public bool IsReloaded => Timer <= 0;
    }
}