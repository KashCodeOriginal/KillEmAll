using ECS.Player.PlayerInput.Shoot.Component;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using GunAspect = ECS.Guns.Shoot.Component.GunAspect;

namespace ECS.Player.PlayerInput.Shoot.System
{
    [BurstCompile]
    public partial struct PlayerShootInputSystem : ISystem
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
            foreach (var gunAspect in SystemAPI.Query<GunAspect>().WithAll<PlayerShootInputTag>())
            {
                gunAspect.IsShooting = Input.GetButton("Fire1");
            }
        }
    }
}