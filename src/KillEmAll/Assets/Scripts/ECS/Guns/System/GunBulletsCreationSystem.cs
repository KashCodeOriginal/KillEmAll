using ECS.BulletRaycast.Component;
using ECS.Bullets.Component;
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
                    
                    foreach (var (ray, localToWorld) in SystemAPI.Query<RayCast, LocalToWorld>())
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
                        }
                        
                        Debug.DrawRay(rayCastInput.Start, rayCastInput.End - rayCastInput.Start, Color.red);
                    }

                    var damage = entityManager.GetComponentData<Bullet>(bulletEntity).Damage;
                        
                    Debug.Log(damage);
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