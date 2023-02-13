using UnityEngine;
using Unity.Entities;
using ECS.Movement.Component;

namespace ECS.Movement.Authoring
{
    public class DirectMoveMono : MonoBehaviour
    {
        private class DirectMoveBaker : Baker<DirectMoveMono>
        {
            public override void Bake(DirectMoveMono authoring)
            {
                AddComponent<DirectMove>();
            }
        }
    }
}