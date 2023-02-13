using ECS.Enemy.Shoot.Component;
using ECS.Movement.Component;
using ECS.Movement.System;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

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

        public void OnUpdate(ref SystemState state)
        {
            foreach (var directMoveAspect in SystemAPI.Query<DirectMoveAspect>())
            {
                var childFromEntity = SystemAPI.GetBuffer<LinkedEntityGroup>(directMoveAspect.Self);

                var shootAspect = SystemAPI.GetAspectRW<EnemyShootAspect>(childFromEntity[2].Value);

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