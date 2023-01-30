using ECS.Enemy.Shoot.Component;
using ECS.Movement.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Movement.System
{
    [BurstCompile]
    public partial struct DirectionMoveSystem : ISystem
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
            var deltaTime = SystemAPI.Time.DeltaTime;

            var ecb =
                SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(); 
            
            new DirectMoveJob(deltaTime, ecb).ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct DirectMoveJob : IJobEntity
    {
        private readonly float _deltaTime;
        private EntityCommandBuffer.ParallelWriter _ecb;

        public DirectMoveJob(float deltaTime, EntityCommandBuffer.ParallelWriter ecb) : this()
        {
            _deltaTime = deltaTime;
            _ecb = ecb;
        }

        [BurstCompile]
        private void Execute(EnemyShootAspect shootAspect, DirectMoveAspect directMoveAspect, [EntityIndexInQuery] int sortKey)
        {
            if (directMoveAspect.IsTargetReached)
            {
                shootAspect.IsShooting = true;
                return;
            }

            shootAspect.IsShooting = false;
            
            directMoveAspect.TransformAspect.LocalPosition +=
                directMoveAspect.Direction * directMoveAspect.Speed * _deltaTime;

            directMoveAspect.TransformAspect.LocalRotation =
                quaternion.LookRotation(directMoveAspect.Direction, math.up());
            
        }
    }
}