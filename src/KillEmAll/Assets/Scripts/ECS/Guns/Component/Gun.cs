using Unity.Entities;

namespace ECS.Guns.Component
{
    public struct Gun : IComponentData
    {
        public int Ammo;
        public int MaxAmmo;
        
        public float ReloadTime;
        
        public float FireRate;

        public bool IsReloading;
        
        public bool IsShooting;

        public Entity BulletEntity;
    }
}