using ECS.Guns.Shoot.Authoring;
using ECS.Guns.Shoot.Component;
using ECS.Player.PlayerInput.GunSwitch.Component;
using Unity.Burst;
using Unity.Entities;

namespace ECS.Guns.Switch.System
{
    [BurstCompile]
    public partial struct GunSwitchSystem : ISystem
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
            foreach (var (gunSwitchInput, gunAspect) in SystemAPI.Query<GunSwitchInput, GunAspect>())
            {
                if (gunSwitchInput.IsPrimaryGunSelected)
                {
                    
                }
                
                
            }
        }
    }
}