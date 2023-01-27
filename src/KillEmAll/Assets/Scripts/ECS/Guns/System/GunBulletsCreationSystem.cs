﻿using ECS.BulletRaycast.Component;
using ECS.Damage.Component;
using ECS.Guns.Component;
using ECS.Player.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Guns.System
{
    public partial struct GunBulletsCreationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }
        
        public void OnDestroy(ref SystemState state)
        {
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            
            var entityManager = state.WorldUnmanaged.EntityManager;

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
                    
                    gunAspect.Ammo--;
                    gunAspect.Timer = gunAspect.FireRate;

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

                    var bulletEntity = CreateBulletEntity(ecb, gunAspect, newBulletPosition, newBulletRotation);

                    /*ecb.Playback(entityManager);
                    
                    var damage = entityManager.GetComponentData<Bullet>(bulletEntity).Damage;*/
                    
                    foreach (var (ray, localToWorld) in SystemAPI.Query<RayCast, LocalToWorld>())
                    {
                        CastRay(localToWorld, ray, physicsWorldSingleton, ecb);
                    }
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

        private static void CastRay(LocalToWorld localToWorld, RayCast ray, PhysicsWorldSingleton physicsWorldSingleton, EntityCommandBuffer ecb)
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

                ecb.AddComponent(hitEntity, new MakeDamage()
                {
                    Value = 10
                });
            }

            //Debug.DrawRay(rayCastInput.Start, rayCastInput.End - rayCastInput.Start, Color.red);
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