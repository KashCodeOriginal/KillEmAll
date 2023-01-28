using ECS.Player;
using Unity.Entities;
using UnityEngine;

namespace ECS.Guns.Authoring
{
    public class PlayerViewTagMono : MonoBehaviour
    {
        private class PlayerViewTagBaker : Baker<PlayerViewTagMono>
        {
            public override void Bake(PlayerViewTagMono authoring)
            {
                AddComponent<PlayerViewTag>();
            }
        }
    }
}