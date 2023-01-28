using Unity.Entities;
using UnityEngine;

namespace ECS.Player
{
    public class PlayerTagMono : MonoBehaviour
    {
        private class PlayerTagBaker : Baker<PlayerTagMono>
        {
            public override void Bake(PlayerTagMono authoring)
            {
                AddComponent(new PlayerTag());
            }
        }
    }
}