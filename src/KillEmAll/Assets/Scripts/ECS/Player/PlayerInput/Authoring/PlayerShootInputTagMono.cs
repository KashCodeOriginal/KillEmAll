using ECS.Player.PlayerInput.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Player.PlayerInput.Authoring
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