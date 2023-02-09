using Unity.Entities;

namespace ECS.Player.PlayerInput.GunSwitch.Component
{
    public struct GunSwitchInput : IComponentData
    {
        public bool IsPrimaryGunSelected;
        public bool IsSecondaryGunSelected;
        public bool IsMeleeSelected;
        public bool IsGrenadeSelected;
    }
}