using ECS.Environment.Component;
using ECS.Guns.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Guns.Authoring
{
    public class GunMono : MonoBehaviour
    {
        [SerializeField] private int _ammo;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _fireRate;
        [SerializeField] private GameObject _bulletPrefab;
        
        [SerializeField] private GameObject _bulletSpawnPoint;
        [SerializeField] private GameObject _entityView;

        private class GunBaker : Baker<GunMono>
        {
            public override void Bake(GunMono authoring)
            {
                AddComponent(new Gun()
                {
                    Ammo = authoring._ammo,
                    MaxAmmo = authoring._maxAmmo,
                    ReloadTime = authoring._reloadTime,
                    FireRate = authoring._fireRate,
                    BulletEntity = GetEntity(authoring._bulletPrefab),
                    BulletSpawnPoint = GetEntity(authoring._bulletSpawnPoint),
                    EntityView = GetEntity(authoring._entityView)
                });
                
                AddComponent<Timer>();
            }
        }
    }
}