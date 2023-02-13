using System;
using Other.Data;
using Unity.Entities;

namespace ECS.Guns.Data.Component
{
    [Serializable]
    public struct GunStatsConfigData : IComponentData
    {
        public GunType GunType;
        public int Damage;
        public int MaxAmmo;
        public float ReloadTime;
        public float FireRate;
        public float ShootRange;
    }
}