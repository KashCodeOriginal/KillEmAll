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
                DependsOn(authoring._gunStaticData);
                
                AddComponent(new Gun()
                {
                    CurrentAmmo = authoring._gunStaticData.GunStatsConfigData.MaxAmmo,
                    GunStatsConfigData = authoring._gunStaticData.GunStatsConfigData,
                    BulletSpawnPoint = GetEntity(authoring._bulletSpawnPoint),
                    EntityView = GetEntity(authoring._entityView),
                    BulletPrefab = GetEntity(authoring._gunStaticData.GunViewConfigData.BulletPrefab)
                });
                
                AddComponentObject(authoring._gunStaticData.GunViewConfigData);

                AddComponent<Timer>();
            }
        }
    }
}