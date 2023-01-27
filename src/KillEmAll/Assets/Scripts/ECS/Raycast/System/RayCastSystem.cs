using ECS.Guns.Component;
using ECS.Raycast.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Raycast.System
{
    [BurstCompile]
    public partial struct RayCastSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            
            foreach (var (ray, localToWorld) in SystemAPI.Query<RayCast, LocalToWorld>())
            {
                var rayCastInput = new RaycastInput()
                {
                    Start = localToWorld.Position,
                    End = localToWorld.Position + math.forward(localToWorld.Rotation) * ray.Length,

                    Filter = new CollisionFilter()
                    {
                        CollidesWith = ray.CollidesWith,
                        BelongsTo = ray.BelongsTo,
                        GroupIndex = ray.GroupIndex
                    }
                };

                if (physicsWorldSingleton.CastRay(rayCastInput, out var hit))
                {
                    Debug.Log("true");
                }
                
                Debug.DrawRay(rayCastInput.Start, rayCastInput.End - rayCastInput.Start, Color.red);
            }
        }
    }
}