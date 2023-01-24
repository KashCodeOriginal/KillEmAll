using ECS.Movement.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Movement.Authoring
{
    public class MoveSpeedMono : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        
        private class MoveSpeedBaker : Baker<MoveSpeedMono>
        {
            public override void Bake(MoveSpeedMono authoring)
            {
                AddComponent(new Speed
                {
                    MoveSpeed = authoring._moveSpeed
                });
            }
        }
    }
}