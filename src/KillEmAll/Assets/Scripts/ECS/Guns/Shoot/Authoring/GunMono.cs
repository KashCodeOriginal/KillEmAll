using ECS.Environment.Component;
using ECS.Guns.Shoot.Component;
using Other.Data.Static;
using Unity.Entities;
using UnityEngine;

namespace ECS.Guns.Shoot.Authoring
{
    public class GunMono : MonoBehaviour
    {
        [SerializeField] private GunStaticData _gunStaticData;
        
        [SerializeField] private GameObject _bulletSpawnPoint;
        [SerializeField] private GameObject _entityView;

        private class GunBaker : Baker<GunMono>
        {
            public override void Bake(GunMono authoring)
            {
                AddComponent(new Gun()
                {
                    CurrentAmmo = authoring._gunStaticData.MaxAmmo,
                    MaxAmmo = authoring._gunStaticData.MaxAmmo,
                    Damage = authoring._gunStaticData.Damage,
                    ReloadTime = authoring._gunStaticData.ReloadTime,
                    FireRate = authoring._gunStaticData.FireRate,
                    BulletEntity = GetEntity(authoring._gunStaticData.BulletPrefab),
                    BulletSpawnPoint = GetEntity(authoring._bulletSpawnPoint),
                    EntityView = GetEntity(authoring._entityView)
                });
                
                AddComponent<Timer>();
            }
        }
    }
}