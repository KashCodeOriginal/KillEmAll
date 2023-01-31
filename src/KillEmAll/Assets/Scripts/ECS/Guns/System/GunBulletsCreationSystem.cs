using ECS.BulletRaycast.Component;
using ECS.Bullets.Component;
using ECS.Damage.Component;
using ECS.Guns.Component;
using ECS.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Guns.System
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
            
            var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

            var ecb =
                SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (gunAspect, rayCast, ltw) in SystemAPI.Query<GunAspect, RayCast, LocalToWorld>())
            {
                gunAspect.Timer -= deltaTime;
                
                TryReload(gunAspect);

                IsReloaded(gunAspect);
                
                if (gunAspect.IsShooting)
                {
                    gunAspect.Ammo--;
                    gunAspect.Timer = gunAspect.FireRate;
                

                    float3 newBulletPosition = float3.zero;
                    quaternion newBulletRotation = quaternion.identity;

                
                    /*foreach (var localToWorld in SystemAPI.Query<LocalToWorld>().WithAll<BulletSpawnPointTag>())
                    {
                        newBulletPosition = localToWorld.Position;
                    }
                
                    foreach (var localToWorld in SystemAPI.Query<LocalToWorld>().WithAll<PlayerViewTag>())
                    {
                        newBulletRotation = localToWorld.Rotation;
                    }*/

                    var bulletEntity = CreateBulletEntity(ecb, gunAspect, newBulletPosition, newBulletRotation);

                    var damage = SystemAPI.GetComponentLookup<Bullet>(true).GetRefRO(gunAspect.BulletEntity).ValueRO.Damage;

                    CastRay(ltw, rayCast, physicsWorldSingleton, ecb, damage);
                }
            }
        }

        private static Entity CreateBulletEntity(EntityCommandBuffer ecb, GunAspect gunAspect, float3 newBulletPosition,
            quaternion newBulletRotation)
        {
            var bulletEntity = ecb.Instantiate(gunAspect.BulletEntity);

            ecb.SetComponent(bulletEntity, new LocalTransform()
            {
                Position = newBulletPosition,
                Rotation = newBulletRotation,
                Scale = 1f
            });
            
            return bulletEntity;
        }

        private static void CastRay(LocalToWorld localToWorld, RayCast ray, PhysicsWorldSingleton physicsWorldSingleton,
            EntityCommandBuffer ecb, float damage)
        {
            var rayCastInput = new RaycastInput()
            {
                Start = localToWorld.Position,
                End = localToWorld.Position + math.forward(localToWorld.Rotation) * ray.Length,

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