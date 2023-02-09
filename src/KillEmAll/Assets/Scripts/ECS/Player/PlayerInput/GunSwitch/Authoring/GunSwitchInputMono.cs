using ECS.Player.PlayerInput.GunSwitch.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Player.PlayerInput.GunSwitch.Authoring
{
    public class GunSwitchInputMono : MonoBehaviour
    {
        private class GunSwitchInputBaker : Baker<GunSwitchInputMono>
        {
            public override void Bake(GunSwitchInputMono authoring)
            {
                AddComponent<GunSwitchInput>();
            }
        }
    }
}