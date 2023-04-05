using ECS.BulletRaycast.Component;
using ECS.Damage.Component;
using ECS.Guns.Data.Component;
using ECS.Guns.Shoot.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace ECS.Guns.Shoot.System
{
    [BurstCompile]
    public partial struct GunBulletsCreationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }
        
        public void OnDestroy(ref SystemState state)
        {
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            var physicsWorldSingleton = GetSingleton<PhysicsWorldSingleton>();

            var ecb =
                GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (gunAspect, rayCast) in Query<GunAspect, RayCast>())
            {
                gunAspect.Timer -= deltaTime;
                
                TryReload(gunAspect);

                IsReloaded(gunAspect);
                
                if (gunAspect.IsShooting)
                {
                    gunAspect.Ammo--;
                    gunAspect.Timer = gunAspect.FireRate;

                    var newBulletPosition = GetComponent<LocalToWorld>(gunAspect.BulletSpawnPoint).Position;
                    var newBulletRotation = GetComponent<LocalToWorld>(gunAspect.EntityView).Rotation;

                    var damage = gunAspect.Damage;
                    
                    CreateBulletEntity(ecb, gunAspect, newBulletPosition, newBulletRotation);

                    CastRay(newBulletPosition, newBulletRotation, rayCast, physicsWorldSingleton, ecb, damage);
                }
            }
        }

        private static void CreateBulletEntity(EntityCommandBuffer ecb, GunAspect gunAspect, float3 newBulletPosition,
            quaternion newBulletRotation)
        {
            var bulletEntity = ecb.Instantiate(gunAspect.BulletPrefab);

            ecb.SetComponent(bulletEntity, new LocalTransform()
            {
                Position = newBulletPosition,
                Rotation = newBulletRotation,
                Scale = 1f
            });
        }

        private static void CastRay(float3 localToWorld, quaternion rotation, RayCast ray, PhysicsWorldSingleton physicsWorldSingleton,
            EntityCommandBuffer ecb, float damage)
        {
            var rayCastInput = new RaycastInput()
            {
                Start = localToWorld,
                End = localToWorld + math.forward(rotation) * ray.Length,

                Filter = new CollisionFilter()
                {
                    CollidesWith = ray.CollidesWith,
                    BelongsTo = ray.BelongsTo,
                    GroupIndex = ray.GroupIndex
                }
            };

            if (physicsWorldSingleton.CastRay(rayCastInput, out var hit))
            {
                var hitEntity = hit.Entity;

                var damageEvent = ecb.CreateEntity();

                ecb.AddComponent(damageEvent, new MakeDamage()
                {
                    Self = damageEvent,
                    Value = damage,
                    Target = hitEntity
                });
            }

            Debug.DrawRay(rayCastInput.Start, rayCastInput.End - rayCastInput.Start, Color.red);
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