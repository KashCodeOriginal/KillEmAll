using ECS.Player.PlayerInput.Shoot.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Player.PlayerInput.Shoot.Authoring
{
    public class PlayerShootInputTagMono : MonoBehaviour
    {
        private class PlayerShootInputTagBaker : Baker<PlayerShootInputTagMono>
        {
            public override void Bake(PlayerShootInputTagMono authoring)
            {
                AddComponent<PlayerShootInputTag>();
            }
        }
    }
}