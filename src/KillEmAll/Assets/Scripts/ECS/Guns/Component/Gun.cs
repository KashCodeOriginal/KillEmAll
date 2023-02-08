using Unity.Entities;

namespace ECS.Guns.Component
{
    public struct Gun : IComponentData
    {
        public int CurrentAmmo;
        public int MaxAmmo;
        public float Damage;

        public float ReloadTime;

        public float FireRate;

        public bool IsReloading;

        public bool IsShooting;

        public Entity BulletEntity;

        public Entity BulletSpawnPoint;
        public Entity EntityView;
    }
}