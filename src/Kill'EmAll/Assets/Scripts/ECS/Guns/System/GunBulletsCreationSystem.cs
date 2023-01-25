using ECS.Guns.Component;
using Unity.Burst;
using Unity.Entities;
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
                }
            }
        }

        private static void IsReloaded(GunAspect gunAspect)
        {
            if (gunAspect.NeedReload && gunAspect.IsReloaded)
            {
                gunAspect.IsReloading = false;
                gunAspect.Ammo = gunAspect.MaxAmmo;
            }
        }

        private static void TryReload(GunAspect gunAspect)
        {
            if (gunAspect.NeedReload && !gunAspect.IsReloading)
            {
                gunAspect.IsReloading = true;
                gunAspect.Timer = gunAspect.ReloadTime;
            }
        }
    }
}