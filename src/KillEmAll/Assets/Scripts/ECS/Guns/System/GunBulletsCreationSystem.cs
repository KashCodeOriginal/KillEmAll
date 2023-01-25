using ECS.Guns.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Guns.System
{
    [BurstCompile]
    public partial struct GunBulletsCreationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var gunAspect in SystemAPI.Query<GunAspect>())
            {
                gunAspect.Timer -= deltaTime;
                
                TryReload(gunAspect);
                
                IsReloaded(gunAspect);

                if (Input.GetButton("Fire1"))
                {
                    if (!gunAspect.CanShoot)
                    {
                        return;
                    }

                    var bulletEntity = ecb.Instantiate(gunAspect.BulletEntity);
                    
                    float3 newBulletPosition = float3.zero;
                    quaternion newBulletRotation = quaternion.identity;
                    
                    foreach (var localToWorld in SystemAPI.Query<LocalToWorld>().WithAll<BulletSpawnPointTag>())
                    {
                        newBulletPosition = localToWorld.Position;
                    }

                    foreach (var localToWorld in SystemAPI.Query<LocalToWorld>().WithAll<PlayerViewTag>())
                    {
                        newBulletRotation = localToWorld.Rotation;
                    }

                    ecb.SetComponent(bulletEntity, new LocalTransform()
                    {
                        Position = newBulletPosition,
                        Rotation = newBulletRotation,
                        Scale = 1f
                    });
                    
                    gunAspect.Ammo--;
                    gunAspect.Timer = gunAspect.FireRate;
                }
            }
        }

        private static void IsReloaded(GunAspect gunAspect)
        {
            if (!gunAspect.NeedReload || !gunAspect.IsReloaded)
            {
                return;
            }
               
            gunAspect.IsReloading = false;
            gunAspect.Ammo = gunAspect.MaxAmmo;
        }

        private static void TryReload(GunAspect gunAspect)
        {
            if (!gunAspect.NeedReload || gunAspect.IsReloading)
            {
                return;
            }
            
            gunAspect.IsReloading = true;
            gunAspect.Timer = gunAspect.ReloadTime;
        }
    }
}