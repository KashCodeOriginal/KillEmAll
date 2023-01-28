using ECS.Guns.Component;
using ECS.Player.PlayerInput.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Player.PlayerInput.System
{
    public partial struct PlayerShootInputSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var gunAspect in SystemAPI.Query<GunAspect>().WithAll<PlayerShootInputTag>())
            {
                gunAspect.IsShooting = Input.GetButton("Fire1");
            }
        }
    }
}