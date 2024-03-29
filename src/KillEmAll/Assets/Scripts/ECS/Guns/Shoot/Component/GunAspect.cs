﻿using ECS.Environment.Component;
using Unity.Entities;

namespace ECS.Guns.Shoot.Component
{
    public readonly partial struct GunAspect : IAspect
    {
        private readonly RefRW<Gun> _gun;
        private readonly RefRW<Timer> _timer;

        public int Ammo
        {
            get => _gun.ValueRO.CurrentAmmo;
            set => _gun.ValueRW.CurrentAmmo = value;
        }

        public float Timer
        {
            get => _timer.ValueRO.Value;
            set => _timer.ValueRW.Value = value;
        }
        
        public Entity BulletPrefab => _gun.ValueRO.BulletPrefab;
        public Entity BulletSpawnPoint => _gun.ValueRO.BulletSpawnPoint;
        public Entity EntityView => _gun.ValueRO.EntityView;

        public int MaxAmmo => _gun.ValueRO.GunConfigData.MaxAmmo;
        
        public float ReloadTime => _gun.ValueRO.GunConfigData.ReloadTime;
        
        public float FireRate => _gun.ValueRO.GunConfigData.FireRate;

        public float Damage => _gun.ValueRO.GunConfigData.Damage;
        public float ShootRange => _gun.ValueRO.GunConfigData.ShootRange;

        public bool IsShooting
        {
            get => _gun.ValueRO.IsShooting && Timer < 0;
            set => _gun.ValueRW.IsShooting = value;
        }

        public bool IsReloading
        {
            get => _gun.ValueRO.IsReloading;
            set => _gun.ValueRW.IsReloading = value;
        }

        public bool NeedReload => Ammo <= 0;

        public bool IsReloaded => Timer <= 0;
    }
}