using ECS.Player.PlayerInput.GunSwitch.Component;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace ECS.Player.PlayerInput.GunSwitch.System
{
    [BurstCompile]
    public partial struct GunSwitchInputSystem : ISystem
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
            foreach (var gunSwitchInput in SystemAPI.Query<RefRW<GunSwitchInput>>())
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    gunSwitchInput.ValueRW.IsPrimaryGunSelected = true;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    gunSwitchInput.ValueRW.IsSecondaryGunSelected = true;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    gunSwitchInput.ValueRW.IsMeleeSelected = true;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    gunSwitchInput.ValueRW.IsGrenadeSelected = true;
                }
                else
                {
                    gunSwitchInput.ValueRW.IsPrimaryGunSelected = false;
                    gunSwitchInput.ValueRW.IsSecondaryGunSelected = false;
                    gunSwitchInput.ValueRW.IsMeleeSelected = false;
                    gunSwitchInput.ValueRW.IsGrenadeSelected = false;
                }
            }
        }
    }
}