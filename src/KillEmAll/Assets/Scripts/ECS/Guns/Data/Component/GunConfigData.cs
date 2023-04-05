using System;
using Other.Data;
using Unity.Entities;

namespace ECS.Guns.Data.Component
{
    [Serializable]
    public struct GunConfigData : IComponentData
    {
        public GunType GunType;
        public GunID GunID;
        public int Damage;
        public int MaxAmmo;
        public float ReloadTime;
        public float FireRate;
        public float ShootRange;
    }
}