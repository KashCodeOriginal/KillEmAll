using ECS.Enemy.Refs.Component;
using ECS.Enemy.Shoot.Component;
using ECS.Movement.Component;
using ECS.Movement.System;
using Unity.Burst;
using Unity.Entities;

namespace ECS.Enemy.Shoot.System
{
    [BurstCompile]
    [UpdateAfter(typeof(DirectionToPlayerSystem))]
    public partial struct CloseToPlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var directMoveAspect in SystemAPI.Query<DirectMoveAspect>())
            {
                var gunComponentLookup = SystemAPI.GetComponentLookup<EnemyChildRefs>();

                var gun = gunComponentLookup.GetRefRO(directMoveAspect.Self).ValueRO.Gun;

                var shootAspect = SystemAPI.GetAspectRW<EnemyShootAspect>(gun);

                directMoveAspect.SafetyDistanceFromDirection = shootAspect.ShootRange;
                
                if (!directMoveAspect.IsTargetReached)
                {
                    shootAspect.IsShooting = false; 
                    return;
                }
                
                shootAspect.IsShooting = true;
            }
        }
    }
    
}