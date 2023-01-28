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
            
            new DirectMoveJob(deltaTime).ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct DirectMoveJob : IJobEntity
    {
        private float _deltaTime;

        public DirectMoveJob(float deltaTime) : this()
        {
            _deltaTime = deltaTime;
        }

        [BurstCompile]
        private void Execute(DirectMoveAspect directMoveAspect)
        {
            if (!directMoveAspect.IsTargetReached)
            {
                directMoveAspect.TransformAspect.LocalPosition +=
                    directMoveAspect.Direction * directMoveAspect.Speed * _deltaTime;

                directMoveAspect.TransformAspect.LocalRotation =
                    quaternion.LookRotation(directMoveAspect.Direction, math.up());
            }
        }
    }
}