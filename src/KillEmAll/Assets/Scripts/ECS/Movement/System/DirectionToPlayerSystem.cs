using ECS.Movement.Component;
using ECS.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Movement.System
{
    public partial struct DirectionToPlayerSystem : ISystem
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
            foreach (var directMoveAspect in SystemAPI.Query<DirectMoveAspect>())
            {
                foreach (var playerTransform in SystemAPI.Query<TransformAspect>().WithAll<PlayerTag>())
                {
                    var vectorToPlayer =
                        playerTransform.LocalPosition - directMoveAspect.TransformAspect.LocalPosition;

                    var direction = math.normalizesafe(vectorToPlayer);

                    direction.y = 0f;
                    
                    directMoveAspect.DistanceFromDirection = math.length(vectorToPlayer);

                    directMoveAspect.Direction = direction;
                }
            }
        }
    }
}