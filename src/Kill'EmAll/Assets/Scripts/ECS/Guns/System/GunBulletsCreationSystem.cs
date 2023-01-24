using System;
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
                
                /*if (gunAspect.NeedReload && gunAspect.IsReloading)
                {
                    if (!gunAspect.IsReloading)
                    {
                        gunAspect.Ammo = gunAspect.MaxAmmo;
                        
                        gunAspect.IsReloading = false;
                        
                        return;
                    }

                    gunAspect.Timer = gunAspect.ReloadTime;
                    
                    gunAspect.IsReloading = true;
                    
                    return;
                }*/
                
                if (Input.GetButton("Fire1"))
                {
                    if (!gunAspect.CanShoot)
                    {
                        return;
                    }
                    
                    gunAspect.Ammo--;

                    gunAspect.Timer = gunAspect.FireRate;
                        
                    Debug.Log(gunAspect.Ammo);
                }
            }
        }
    }
}